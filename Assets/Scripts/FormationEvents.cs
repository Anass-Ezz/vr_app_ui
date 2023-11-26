using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FormationEvents : MonoBehaviour
{
    private int SelectedFormationId;
    private bool isReading = false;
    [SerializeField] private FormationScriptable formationScriptable;
    [SerializeField] private GameObject FormationDetails;
    [SerializeField] private GameObject FormationHome;
    [SerializeField] private List<GameObject> FormationList = new List<GameObject>();
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip HoverSFX;
    private List<GameObject> FormationInstList = new List<GameObject>();

    public void FormationHoverEntered(GameObject gameObject){
        source.PlayOneShot(HoverSFX);
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
        SelectedFormationId = id;
        
        FormationItem formation = formationScriptable.formationList[id];
        FormationDetails.SetActive(true);
        FormationHome.SetActive(false);
        Image bgImage = FormationDetails.GetComponent<Image>();
        bgImage.sprite = formation.img;
        FormationDetails.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = formation.name;
        FormationDetails.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = formation.color;
        FormationDetails.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = formation.description;
        FormationDetails.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().color = formation.color;
        FormationDetails.transform.GetChild(2).gameObject.GetComponent<Image>().color = formation.color;
        FormationDetails.transform.GetChild(3).gameObject.GetComponent<Image>().color = formation.color;
        FormationDetails.transform.GetChild(4).gameObject.GetComponent<Image>().color = formation.color;

    }
    public void BackHome(){
        for(int i = 0 ; i<FormationInstList.Count ; i++){
            Destroy(FormationList[i]);
        }
        FormationList.Clear();
        foreach(GameObject form in FormationInstList){
            GameObject instObject = Instantiate(form, FormationHome.transform);
            instObject.SetActive(true);
            FormationList.Add(instObject);
        }
        StopReading();
        FormationDetails.SetActive(false);
        FormationHome.SetActive(true);
        SelectedFormationId = 5;
    }
    
    public void HandleReading(){
        if (SelectedFormationId <= 3){
            if(!isReading){
                FormationItem formation = formationScriptable.formationList[SelectedFormationId];
                source.PlayOneShot(formation.audio);
                isReading = true;
                FormationDetails.transform.GetChild(3).gameObject.SetActive(false);
                FormationDetails.transform.GetChild(4).gameObject.SetActive(true);
            }
            else{
                StopReading();
            }
        }
    }
    public void StopReading(){
        source.Stop();
        FormationDetails.transform.GetChild(3).gameObject.SetActive(true);
        FormationDetails.transform.GetChild(4).gameObject.SetActive(false);
        isReading = false;
    }

    void Start()
    {
        foreach(GameObject form in FormationList){
            GameObject instObject = Instantiate(form, FormationHome.transform);
            instObject.SetActive(false);
            FormationInstList.Add(instObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
