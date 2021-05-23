using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour
{
    public GenerateHand hand;
    public GameObject crosshair;
    public bool grabbed;
 
  
    void Start()
    {
        hand = FindObjectOfType<GenerateHand>();
     
        
        crosshair = GameObject.Find("crosshair");
    }
    public void TaskOnClick()
    {
        
            hand.Reorder(gameObject);
            grabbed = true;
            Debug.Log("Grabbed");
        
      
    }
  

    // Update is called once per frame
    void Update()
    {
      
        if (grabbed && Input.GetKey(KeyCode.Mouse0))
        {
            transform.position = crosshair.transform.position;
            
        }
        if (grabbed && Input.GetKeyUp(KeyCode.Mouse0))
        {
          
            Debug.Log("unGrabbed");
            hand.Reorder(gameObject);
            Time.timeScale = 1;
            grabbed = false;
        }

    }
}
