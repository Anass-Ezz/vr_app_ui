using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsHandeler : MonoBehaviour
{
    public LayerMask newLayerMask;
    [SerializeField] private Camera camera;
    public void Gaze(GameObject gameObject){
        
        Vector3 camera_position = camera.transform.position;
        Quaternion zeroRotation = Quaternion.identity;
        GameObject duplicatedObject = Instantiate(gameObject, camera_position, zeroRotation);
        Rigidbody rb = duplicatedObject.GetComponent<Rigidbody>();
        Component XR_intr_script = duplicatedObject.GetComponent("XRSimpleInteractable");
        XR_intr_script.GetType().GetProperty("enabled").SetValue(XR_intr_script, false, null);
        rb.useGravity = false;
        rb.isKinematic = true;
        duplicatedObject.layer = LayerMask.NameToLayer("Gaze");
        camera.cullingMask = newLayerMask;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
