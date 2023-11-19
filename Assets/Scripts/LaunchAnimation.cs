using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchAnimation : MonoBehaviour
{
    [SerializeField] Text textbtn ;
    public void OnClickDoor (GameObject door )
    {
        Animator animator = door.GetComponent<Animator>() ;
        bool isOpenVar = animator.GetBool("IsOpen");
        if(!isOpenVar)
        {
            animator.SetBool("IsOpen" , true) ;
            textbtn.text = "Close" ;
        }
        else
        {
            animator.SetBool("IsOpen" , false) ;
            textbtn.text = "Open" ;

        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
