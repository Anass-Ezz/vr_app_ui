using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Create New Object Details")]
public class ObjectInfoScriptable : ScriptableObject
{
    public string name;
    public string description;
}

