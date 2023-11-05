using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EventsHandeler : MonoBehaviour
{


    public InputActionProperty triggerAction;
    [SerializeField] private LayerMask newLayerMask;
    [SerializeField] private Camera camera;
    private GameObject gazedObject;
    private GameObject duplicatedGazedObject;
    private bool hover = false;
    private bool objectGazed = false;
    public Color backgroundColor = Color.black;
    [SerializeField] GameObject descriptionUI;
   
    public void Gaze(){
        objectGazed = true;
        Vector3 camera_position = camera.transform.position;
        Quaternion zeroRotation = Quaternion.identity;
        GameObject duplicatedGazedObject = Instantiate(gazedObject, camera_position + new Vector3(0f, 0f, 2f), zeroRotation);
        descriptionUI.transform.position = camera_position + new Vector3(-1.5f, 0f, 2f);
        // descriptionUI.transform.rotation = camera.transform.rotation;
        Rigidbody rb = duplicatedGazedObject.GetComponent<Rigidbody>();
        Component XR_intr_script = duplicatedGazedObject.GetComponent("XRSimpleInteractable");
        XR_intr_script.GetType().GetProperty("enabled").SetValue(XR_intr_script, false, null);
        rb.useGravity = false;
        rb.isKinematic = true;
        duplicatedGazedObject.layer = LayerMask.NameToLayer("Gaze");
        camera.cullingMask = LayerMask.GetMask("Gaze", "player");
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = backgroundColor;
    }

    public void OnHoverEntered(GameObject gameObject){
        if (!this.hover) this.hover = !this.hover;
        gazedObject = gameObject;
    }
    public void OnHoverExited(GameObject gameObject){
        if (this.hover) this.hover = !this.hover;
        gazedObject = gameObject;
    }
    
    public void DescExitButton(){
        Destroy(gazedObject);
        camera.cullingMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "player");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        float triggerValue = triggerAction.action.ReadValue<float>();    
        if (this.hover && triggerValue > 0 && !objectGazed){
            Gaze();
        }
        Debug.Log("Hover : ");
        Debug.Log(hover);
        Debug.Log("trigger value : ");
        Debug.Log(triggerValue);
    }
}
