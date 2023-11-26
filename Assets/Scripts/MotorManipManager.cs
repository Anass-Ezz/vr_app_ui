using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MotorManipSteps {
    public string name;
    public AudioClip stepAudio;
    public AudioClip repeatAudio;
    public GameObject stepObject;
    public GameObject originalObject;
    public GameObject socketObject;
    public bool status;
}

public class MotorManipManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private List<MotorManipSteps> Steps = new List<MotorManipSteps>();
    // [SerializeField] private List<GameObject> DisassembledMotor = new List<GameObject>();

    public void ValidStep(int stepId){
        MotorManipSteps currentStep = Steps[stepId];
        currentStep.status = true;
        currentStep.originalObject.SetActive(true);
        Destroy(currentStep.stepObject);
        Destroy(currentStep.socketObject);
        if(stepId < Steps.Count-1){
            MotorManipSteps nextStep = Steps[stepId+1];
            Component XR_grab_Script = nextStep.stepObject.GetComponent("XRGrabInteractable");
            XR_grab_Script.GetType().GetProperty("enabled").SetValue(XR_grab_Script, true, null);
            source.PlayOneShot(Steps[stepId+1].stepAudio);
        }
        else{
            Debug.Log("Congratulations");
        }

    }
    public void ValidStep(){

    }
    private void InitSteps(){
        Component XR_grab_Script;
        foreach(MotorManipSteps step in Steps){
            XR_grab_Script = step.stepObject.GetComponent("XRGrabInteractable");
            XR_grab_Script.GetType().GetProperty("enabled").SetValue(XR_grab_Script, false, null); 
        }
        XR_grab_Script = Steps[0].stepObject.GetComponent("XRGrabInteractable");
        XR_grab_Script.GetType().GetProperty("enabled").SetValue(XR_grab_Script, true, null); 
        source.PlayOneShot(Steps[0].stepAudio);
    }
    void Start()
    {
        InitSteps();
    }

    void Update()
    {
        
    }
}
