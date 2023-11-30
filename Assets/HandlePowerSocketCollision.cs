using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePowerSocketCollision : MonoBehaviour
{
    public bool isSameTerminal;
    public bool isConnected;
    void Start()
    {
        
    }
    void OnTriggerStay(Collider other){
        if (isConnected){
            if (other.gameObject.tag == gameObject.tag){
                isSameTerminal = true;
            }
            else{
                isSameTerminal = false;
            }
        }
    }
    public void CheckSocket(){
        isConnected = true;
    }
    public void UncheckSocket(){
        isConnected = false;
    }
    void Update()
    {
        
    }
}