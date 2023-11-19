using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elevatorEvent : MonoBehaviour
{
    public bool elevIsDown = true;
    [SerializeField] private Text UI_tbn_text; 
    [SerializeField] private GameObject envirenment;

    public void MoveElevator(GameObject elevator){
        if (elevIsDown = true){
            Vector3 envCurrPos = envirenment.transform.position;
            envirenment.transform.position = new Vector3(envCurrPos[0], envCurrPos[1], envCurrPos[2] + 8f);
            elevIsDown = false;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
