using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;



public class EventsHandeler : MonoBehaviour
{

    public InputActionProperty triggerAction;
    public InputActionProperty degaze_button;
    public InputActionProperty rightJoystickX;
    public InputActionProperty rightJoystickY;

    [SerializeField] private LayerMask newLayerMask;
    [SerializeField] private Camera camera;
    private GameObject gazedObject;
    private GameObject duplicatedGazedObject;
    private bool objectHover = false;
    private bool objectGazed = false;
    public Color backgroundColor = Color.black;
    [SerializeField] private GameObject descriptionUI;
    [SerializeField] private GameObject XR_rigMorveManager;
    [SerializeField] private GameObject XR_rigTurnManager;
    [SerializeField] private GameObject LocomotionSys;
    [SerializeField] private Text UIHeaderArea;
    [SerializeField] private Text UITextArea;
    [SerializeField] private GameObject RightTeleportation;
   
    public void Gaze(){
        objectGazed = true;
        duplicatedGazedObject = DuplicateGameObj();
        duplicatedGazedObject.layer = LayerMask.NameToLayer("Gaze");
        camera.cullingMask = LayerMask.GetMask("Gaze", "player");
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = backgroundColor;
        LocomotionSys.SetActive(false);
        descriptionUI.SetActive(true);
        UIHeaderArea.text =  getObjectData().name;
        UITextArea.text =  getObjectData().description;
        Component XR_telepo_script = RightTeleportation.GetComponent("XRRayInteractor");
        XR_telepo_script.GetType().GetProperty("enabled").SetValue(XR_telepo_script, false, null); 
    }
    private ObjectInfoScriptable getObjectData(){
        ObjectData ObjectDataScript = gazedObject.GetComponent<ObjectData>();
        return (ObjectDataScript.target);
    }
  
    private GameObject DuplicateGameObj(){
        Vector3 camera_position = camera.transform.position;
        Quaternion camera_Rotation = camera.transform.rotation;
        GameObject duplicatedGazedObject = Instantiate(gazedObject, camera_position + new Vector3(0f, 0f, 2f), new Quaternion(camera.transform.rotation.x, camera.transform.rotation.y, 0f, camera.transform.rotation.w));
        descriptionUI.transform.position = camera_position + new Vector3(-1.5f, 0f, 2f);
        Rigidbody rb = duplicatedGazedObject.GetComponent<Rigidbody>();
        Component XR_intr_script = duplicatedGazedObject.GetComponent("XRSimpleInteractable");
        XR_intr_script.GetType().GetProperty("enabled").SetValue(XR_intr_script, false, null);
        rb.useGravity = false;
        rb.isKinematic = true;
        return duplicatedGazedObject;
    }
    public void OnObjectHoverEntered(GameObject gameObject){
        if (!objectGazed){
            if (!objectHover) objectHover = !objectHover;
            gazedObject = gameObject;
        }
    }
    public void OnObjectHoverExited(GameObject gameObject){
        if (!objectGazed){
            if (objectHover) objectHover = !objectHover;
            gazedObject = null;
        }
    }
  
    public void Degaze(InputAction.CallbackContext context){
        if (objectGazed){
            camera.cullingMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "player");
            Destroy(duplicatedGazedObject);
            objectGazed = false;
            gazedObject = null;
            duplicatedGazedObject = null;
            camera.clearFlags = CameraClearFlags.Skybox;
            // XR_rigMorveManager.SetActive(true);
            // XR_rigTurnManager.SetActive(true);
            LocomotionSys.SetActive(true);
            descriptionUI.SetActive(false);
            Component XR_telepo_script = RightTeleportation.GetComponent("XRRayInteractor");
            XR_telepo_script.GetType().GetProperty("enabled").SetValue(XR_telepo_script, true, null);  
        }
    }
    private void ZoomGazedObject(float zoomValue){
        Vector3 currentPosition = duplicatedGazedObject.transform.position;
        if (zoomValue > 0){
            Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 0.01f);
            duplicatedGazedObject.transform.position = newPosition;
        }
        else{
            Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 0.01f);
            duplicatedGazedObject.transform.position = newPosition;
        }
    }
    private void RotateGazedObject(float rotValue){
        if (rotValue > 0){
            duplicatedGazedObject.transform.Rotate(Vector3.up*1f);
        }
        else{
            duplicatedGazedObject.transform.Rotate(Vector3.up*(-1f));
        }
    }
    void Start()
    {
        descriptionUI.SetActive(false);  

    }

    void Update(){
        float triggerValue = triggerAction.action.ReadValue<float>();    
        float JstickYValue = rightJoystickY.action.ReadValue<Vector2>()[1];    
        float JstickXValue = rightJoystickX.action.ReadValue<Vector2>()[0];    
        if (objectHover && triggerValue > 0 && !objectGazed){
            Gaze();
        }
        if (objectGazed && Math.Abs(JstickXValue) < Math.Abs(JstickYValue)){
            ZoomGazedObject(JstickYValue);
        }
        else if (objectGazed && Math.Abs(JstickXValue) > Math.Abs(JstickYValue)){
            RotateGazedObject(JstickXValue);
        }
        else if (objectGazed && JstickYValue != 0){
            ZoomGazedObject(JstickYValue);
        }
        else if (objectGazed && JstickXValue != 0){
            RotateGazedObject(JstickXValue);
        }
        degaze_button.action.started += Degaze;
    }
   
}
