using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitManip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ExitUI;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "XR Origin (XR Rig)"){
            ExitUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.name == "XR Origin (XR Rig)"){
            ExitUI.SetActive(false);
        }
    }
    public void  ExitClick(){
        SceneManager.LoadScene("Main VR scean");
    }
    void Update()
    {
        
    }
}
