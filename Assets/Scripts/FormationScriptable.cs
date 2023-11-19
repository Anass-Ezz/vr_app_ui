using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FormationItem {
    public string name;
    public string description;
    public Color color;
    public Sprite img;
    public AudioClip audio;
}

[CreateAssetMenu(fileName = "Object", menuName = "ScriptableObjects/Create New Formation")]
public class FormationScriptable : ScriptableObject
{
    public List<FormationItem> formationList = new List<FormationItem>();

}