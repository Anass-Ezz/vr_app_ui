using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionTable : MonoBehaviour
{
    [SerializeField] Material collisionMaterial;
    [SerializeField] Material tableMaterial;
    
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Box"){
            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            if (rb.isKinematic){
                Renderer tableRenderer = transform.GetComponent<Renderer>();
                tableRenderer.material = collisionMaterial;
            }

        }
        else if(collision.gameObject.tag == "Robot"){
                Renderer tableRenderer = transform.GetComponent<Renderer>();
                tableRenderer.material = collisionMaterial;
        }
    }
    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Robot"){
                Renderer tableRenderer = transform.GetComponent<Renderer>();
                tableRenderer.material = tableMaterial;
        }
    }

    void Start()
    {
        tableMaterial = transform.GetComponent<Renderer>().material;
    }

    void Update()
    {
        
    }
}
