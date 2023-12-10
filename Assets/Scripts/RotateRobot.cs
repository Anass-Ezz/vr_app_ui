using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Joints{
    public GameObject joint;
    public Vector3 axis;
}


public class RotateRobot : MonoBehaviour
{
    [SerializeField] private List<Joints> Joints = new List<Joints>();
    // Start is called before the first frame update
    public float value1 = 0.0f;
    public float value2 = 0.0f;
    public float value3 = 0.0f;
    public float value4 = 0.0f;
    public int joint = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentJoint = Joints[joint].joint;
        Vector3 jointAxis = Joints[joint].axis;
        if (jointAxis.x == 1){
            currentJoint.transform.rotation = Quaternion.Euler(value1, currentJoint.transform.rotation.eulerAngles.y, currentJoint.transform.rotation.eulerAngles.z);
            // Quaternion newRotation = Quaternion.Euler(value, currentJoint.transform.rotation.eulerAngles.y, currentJoint.transform.rotation.eulerAngles.z);
            // currentJoint.transform.rotation = newRotation;
        }
        else if (jointAxis.y == 1){
            currentJoint.transform.rotation = Quaternion.Euler(currentJoint.transform.rotation.eulerAngles.x, value1, currentJoint.transform.rotation.eulerAngles.z);
            // Quaternion newRotation = Quaternion.Euler(currentJoint.transform.rotation.eulerAngles.x, value, currentJoint.transform.rotation.eulerAngles.z);
            // currentJoint.transform.rotation = newRotation;
        }
        //


    }
}
