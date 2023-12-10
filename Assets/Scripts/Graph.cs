using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Graph : MonoBehaviour
{

    [SerializeField] private Sprite dotSprite;
    [SerializeField] private Transform pendulum;

    [SerializeField] RectTransform graphContainor;
    private float c = 0.0f;
    private float elapsedTime = 0f;
    private float logInterval = 0.1f; 
    private float magnitude = 0.0f;

    private bool max = false;
    private bool min = false;

    void Start()
    {

    }
    private void CreateDot(Vector2 position){
        GameObject dot = new GameObject("dot", typeof(Image));
        dot.transform.SetParent(graphContainor, false);
        dot.GetComponent<Image>().sprite = dotSprite;
        RectTransform dotRectTrans = dot.GetComponent<RectTransform>();
        dotRectTrans.anchoredPosition = position;
        dotRectTrans.sizeDelta = new Vector2(0.1f, 0.1f);
        dotRectTrans.anchorMin = new Vector2(0, 0.5f);
        dotRectTrans.anchorMax = new Vector2(0, 0.5f);
    }
    void Update()
    {
        Debug.Log(pendulum.rotation);
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= logInterval)
        {
            // if (!max && magnitude < 2.5f){
            //     magnitude += 0.2f;
            //     if (magnitude >= 2.5f){
            //         max = true;
            //     }
            // }
            // else if(max){
            //     magnitude -= 0.2f;
            //     if (magnitude <= -2.5f){
            //         max = false;
            //         min = true;
            //     }

            // }






            CreateDot(new Vector2(c, pendulum.rotation.x*3.3f));
            elapsedTime = 0f;
            c += 0.01f;
        }
        
    }
}
