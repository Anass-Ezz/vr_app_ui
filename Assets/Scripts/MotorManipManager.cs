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
    public GameObject clipBoardStep;
    public bool status;
}

public class MotorManipManager : MonoBehaviour
{

    [SerializeField] private AudioSource source;
    [SerializeField] private List<MotorManipSteps> Steps = new List<MotorManipSteps>();
    [SerializeField] private GameObject Rotor;
    [SerializeField] private GameObject socketL1;
    [SerializeField] private GameObject socketL2;
    [SerializeField] private GameObject socketL3;

    private HandlePowerSocketCollision scriptL1;
    private HandlePowerSocketCollision scriptL2;
    private HandlePowerSocketCollision scriptL3;

    private bool direction;
    private bool connectedFirst = false;

    private MotorManipSteps nextStep;
    [SerializeField] private AudioClip finishigAudio;

    public void ValidStep(int stepId){
        MotorManipSteps currentStep = Steps[stepId];
        currentStep.status = true;
        if (currentStep.clipBoardStep != null){
            currentStep.clipBoardStep.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (currentStep.originalObject != null){
            currentStep.originalObject.SetActive(true);
        }
        if (currentStep.stepObject != null){
            Destroy(currentStep.stepObject);
        }
        if (currentStep.socketObject != null){
            Destroy(currentStep.socketObject);
        }
        if(stepId < Steps.Count-1){
            source.PlayOneShot(Steps[stepId+1].stepAudio);
            nextStep = Steps[stepId+1];
            if (nextStep.stepObject != null){
                StartCoroutine(WaitAndContinue(nextStep));
            }
        }
        else{
            source.PlayOneShot(finishigAudio);
        }
    }

    public void VerifyLate(){
        if(nextStep.repeatAudio != null){
            if (!source.isPlaying){
                source.PlayOneShot(nextStep.repeatAudio);
            }
        }
        if(!Steps[Steps.Count - 1].status){
            Invoke("VerifyLate", 15f);
        }
    }
    IEnumerator WaitAndContinue(MotorManipSteps nextStep)
    {
        while (source.isPlaying)
        {
            yield return null; 
        }
        Component XR_grab_Script = nextStep.stepObject.GetComponent("XRGrabInteractable");
        XR_grab_Script.GetType().GetProperty("enabled").SetValue(XR_grab_Script, true, null);
        Component OutlineScript = nextStep.stepObject.GetComponent("Outline");
        OutlineScript.GetType().GetProperty("enabled").SetValue(OutlineScript, true, null);
    }
    
    private void InitSteps(){
        Component XR_grab_Script;
        foreach(MotorManipSteps step in Steps){
            if (step.stepObject != null){
                XR_grab_Script = step.stepObject.GetComponent("XRGrabInteractable");
                XR_grab_Script.GetType().GetProperty("enabled").SetValue(XR_grab_Script, false, null); 
            }
        }
        ValidStep(0);
    }

   
   
    void Start()
    {
        InitSteps();
        scriptL1 = socketL1.GetComponent<HandlePowerSocketCollision>();
        scriptL2 = socketL2.GetComponent<HandlePowerSocketCollision>();
        scriptL3 = socketL3.GetComponent<HandlePowerSocketCollision>();
        RotateMotor(Vector3.forward);
        Invoke("VerifyLate", 15f);
    }
    void RotateMotor(Vector3 rotationDirection)
    {
        Rotor.transform.Rotate(rotationDirection * 1000f * Time.deltaTime);
    }
    void Update()
    {
        if(Steps[Steps.Count - 2].status){
            if (scriptL1.isConnected && scriptL2.isConnected && scriptL3.isConnected){
                if (!connectedFirst){
                    ValidStep(5);
                    connectedFirst = true;
                }
                direction = (!scriptL1.isSameTerminal || scriptL3.isSameTerminal) && (scriptL1.isSameTerminal || !scriptL2.isSameTerminal)  && (scriptL2.isSameTerminal || !scriptL3.isSameTerminal);
                Debug.Log("Run " + direction);
                RotateMotor((direction) ? Vector3.forward : Vector3.back);
            }
        }
        
    }
}
