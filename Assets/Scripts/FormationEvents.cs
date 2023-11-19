using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormationEvents : MonoBehaviour
{
    [SerializeField] private FormationScriptable formationScriptable;
    [SerializeField] private GameObject FormationDetails;

    public void FormationHoverEntered(GameObject gameObject){
        Vector3 current = gameObject.transform.position;
        gameObject.transform.position = new Vector3(current[0], current[1], current[2] - 0.1f);
        Transform child = gameObject.transform.GetChild(0);
        Vector3 currentChildPos = child.position;
        child.position = new Vector3(currentChildPos[0], currentChildPos[1], currentChildPos[2] - 0.02f);
        child.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void FormationHoverExited(GameObject gameObject){
        Vector3 current = gameObject.transform.position;
        gameObject.transform.position = new Vector3(current[0], current[1], current[2] + 0.1f);
        Transform child = gameObject.transform.GetChild(0);
        Vector3 currentChildPos = child.position;
        child.position = new Vector3(currentChildPos[0], currentChildPos[1], currentChildPos[2] + 0.02f);
        child.localScale = new Vector3(1f, 1f, 1f);
    }
    public void FormationClicked(int id){
        FormationItem formation = formationScriptable.formationList[id];
        Image bgImage = FormationDetails.GetComponent<Image>();
        bgImage.sprite = formation.img;
        FormationDetails.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = formation.name;
        FormationDetails.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = formation.color;
        FormationDetails.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = formation.description;
        FormationDetails.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().color = formation.color;
        // Debug.Log(FormationDetails.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = formation.color);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
