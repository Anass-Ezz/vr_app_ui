using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public ObjectInfoScriptable target;


    public string getName()
    {
        return (target.name);
    }

    public string getDescription()
    {
        return (target.description);
    }
}
