using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleBoxGrab : MonoBehaviour
{
    private bool Collided = false;
    private bool Picked = false;
    [SerializeField] private GameObject boxToPick;
    [SerializeField] private GameObject boxInEffector;
    [SerializeField] private TMP_Text pickText;
    void Start()
    {
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "box to pick")
            Collided = true;
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.name == "box to pick")
            Collided = false;
    }
    public void PickBox(){
        if(Collided){
            if (pickText.text == "PICK"){
                boxToPick.transform.position = boxInEffector.transform.position;
                boxToPick.transform.rotation = boxInEffector.transform.rotation;
                Rigidbody rb = boxToPick.transform.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                boxToPick.transform.SetParent(transform);
                pickText.text = "DROP";
            }
            else{
                // boxToPick.transform.position = boxInEffector.transform.position;
                // boxToPick.transform.rotation = boxInEffector.transform.rotation;
                boxToPick.transform.SetParent(null);
                Rigidbody rb = boxToPick.transform.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                pickText.text = "PICK";
            }
        }
        
    }
    void Update()
    {
        Debug.Log(pickText.text);
    }
}
