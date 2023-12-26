using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Joints{
    public GameObject joint;
    public Vector3 axis;
}


public class RotateRobot : MonoBehaviour
{
    [SerializeField] private List<Joints> Joints = new List<Joints>();
    [SerializeField] private List<Slider> JointSliders = new List<Slider>();

    void Start()
    {
    }

    public void ChangeRot(int id){
        GameObject joint = Joints[id].joint;
        Vector3 axis = Joints[id].axis;
        joint.transform.localEulerAngles = new Vector3(JointSliders[id].value*axis.x, JointSliders[id].value*axis.y, JointSliders[id].value*axis.z);
    }

    void Update()
    {
        
    }
}
