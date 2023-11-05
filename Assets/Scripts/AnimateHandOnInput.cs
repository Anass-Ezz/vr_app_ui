using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimatorAction;
    public InputActionProperty gripAnimatorAction;
    public Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pinchValue = pinchAnimatorAction.action.ReadValue<float>();
        float gripValue = gripAnimatorAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", pinchValue);
        handAnimator.SetFloat("Grip", gripValue);
    }
}
