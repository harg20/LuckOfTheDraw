using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardbounce : MonoBehaviour
{
    Rigidbody rb;
    public CardDataTracker cdt;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(.2f,1,0) * 500);
    }
  
    // Update is called once per frame

}
