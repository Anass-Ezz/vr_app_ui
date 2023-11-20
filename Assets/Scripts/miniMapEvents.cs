using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.InputSystem;
using TMPro;

public class miniMapEvents : MonoBehaviour
{
    public InputActionProperty ChooseBlockAction;
    public InputActionProperty LeftGrip;
    [SerializeField] private List<GameObject> ListOfFloor0 = new List<GameObject>();
    [SerializeField] private List<GameObject> ListOfFloor1 = new List<GameObject>();
    [SerializeField] private Material InvisibleMaterial;
    [SerializeField] private Material OrangeMaterial; 
    [SerializeField] private Material RoofMaterial; 
    [SerializeField] private GameObject miniMap;
    [SerializeField] private TMP_Text FloorText;
    [SerializeField] private GameObject TeleportationPoints;
    [SerializeField] private GameObject Character;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip MiniMapExpandSFX;
    [SerializeField] private AudioClip HoverSFX;
    private bool work = false;
    private GameObject SelectedBlock;
    private int selectedFloor = 0;
    private bool miniMapExpanded = false;

    public void testFunc(){
    }
    private void WaitAndWork(){
        work = true;
    }
    public void expandBlock(GameObject block){
        if(work && SelectedBlock == null){
            source.PlayOneShot(HoverSFX);
            SelectedBlock = block;
            block.transform.Translate(new Vector3(0f, 0.01f, 0f));
            Transform SecondFloor = block.transform.GetChild(1);
            // SecondFloor.Translate(new Vector3(0f, 0f, 0.01f));
            GameObject floor = block.transform.GetChild(selectedFloor).gameObject;
            Component OutlineScript = floor.GetComponent("Outline");
            OutlineScript.GetType().GetProperty("enabled").SetValue(OutlineScript, true, null); 
        }
    }
    public void colapsBlock(GameObject block){
        if(work && SelectedBlock != null){
            SelectedBlock.transform.Translate(new Vector3(0f, -0.01f, 0f));
            Transform SecondFloor = SelectedBlock.transform.GetChild(1);
            // SecondFloor.Translate(new Vector3(0f, 0f, -0.01f));   
            GameObject floor = SelectedBlock.transform.GetChild(selectedFloor).gameObject;
            Component OutlineScript = floor.GetComponent("Outline");
            OutlineScript.GetType().GetProperty("enabled").SetValue(OutlineScript, false, null); 
            SelectedBlock = null;     
        }
    }
    void ExpandMiniMap(){
        if(!miniMapExpanded){
            source.PlayOneShot(MiniMapExpandSFX);
            GameObject[] NoRayCastObj = GameObject.FindGameObjectsWithTag("MiniMapNoRayCast");
            foreach (GameObject obj in NoRayCastObj)
            {
                Component RayInterScript = obj.GetComponent("XRSimpleInteractable");
                RayInterScript.GetType().GetProperty("enabled").SetValue(RayInterScript, true, null); 
            }
            miniMap.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            miniMapExpanded = true;
        }

    }
    void ColapsMiniMap(){
        if(miniMapExpanded){
            GameObject[] NoRayCastObj = GameObject.FindGameObjectsWithTag("MiniMapNoRayCast");
            foreach (GameObject obj in NoRayCastObj)
            {
                Component RayInterScript = obj.GetComponent("XRSimpleInteractable");
                RayInterScript.GetType().GetProperty("enabled").SetValue(RayInterScript, false, null); 
            }
            miniMap.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            miniMapExpanded = false;
        }
    }

    public void ChangeFloor(){
        
        if(selectedFloor == 0){
            selectedFloor = 1;
            FloorText.text = "Floor 1";
            foreach(GameObject floor in ListOfFloor1){
                Renderer rend = floor.GetComponent<Renderer>();
                Material[] materialsArray = new Material[] { OrangeMaterial, RoofMaterial };
                rend.materials = materialsArray;
            }
            foreach(GameObject floor in ListOfFloor0){
                Renderer rend = floor.GetComponent<Renderer>();

                rend.material = InvisibleMaterial;
            }

        }
        else if(selectedFloor == 1){
            selectedFloor = 0;
            FloorText.text = "Floor 0";
            foreach(GameObject floor in ListOfFloor0){
                Renderer rend = floor.GetComponent<Renderer>();
                rend.material = OrangeMaterial;
            }
            foreach(GameObject floor in ListOfFloor1){
                Renderer rend = floor.GetComponent<Renderer>();
                Material[] materialsArray = new Material[] { InvisibleMaterial, InvisibleMaterial };
                rend.materials = materialsArray;
            }
        }
    }
    private void TeleportBlock(){
        if (SelectedBlock != null){
            string blockNum = SelectedBlock.name[5] + selectedFloor.ToString();
            foreach (Transform block in TeleportationPoints.transform)
            {
                if (block.gameObject.name.EndsWith(blockNum)){
                    Character.transform.position = block.position;
                }
            }
        }
    }

    void Start()
    {
        ColapsMiniMap();
        Invoke("WaitAndWork", 1);
    }

    // Update is called once per frame
    void Update()
    {
        float trigVal = ChooseBlockAction.action.ReadValue<float>();
        float gripVal = LeftGrip.action.ReadValue<float>();
        if (trigVal > 0){   
            TeleportBlock();
        }
        if (gripVal > 0){   
            ExpandMiniMap();
        }
        else{   
            ColapsMiniMap();
        }
    }
}

// B11 (-12.4, 3.97, 6.20)
// B21 (-13.04, 3.97, 2.18)
// B31 (7.52, 3.97, -13.14)
// B41 (9.07, 3.97, -0.27)

// B10 (-20.45, -1.06, -15.49)
// B20 (-7.66, -1.06, 12.15)
// B30 (6.54, -1.06, -13)
// B40 (6.36, 3.97, 14.79)