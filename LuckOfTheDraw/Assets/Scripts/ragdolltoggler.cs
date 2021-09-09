using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdolltoggler : MonoBehaviour
{
   
    public Ikfabric larm;
    public Ikfabric rarm;

    public bool flopped;
    // Start is called before the first frame update
    void Start()
    {
        RagdolltoggleOff();
    }
    public void RagdolltoggleOff()
    {

        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                if (c.gameObject.GetComponent<Rigidbody>())
                {
                    c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    c.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }

            }

        }

    }

    public void RagdolltoggleOn()
    {
        //Instantiate(HitEffect, transform.position, Quaternion.identity);
       // GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().isKinematic = false;
        flopped = true;
        rarm.enabled = false;
        larm.enabled = false;
       // if (lance && lance.transform.parent)mov.DiscardLance();

       // if (shield && shield.transform.parent)mov.DiscardShield();
        //mov.enabled = false;
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = false;
                if (c.gameObject.GetComponent<Rigidbody>())
                {
                    c.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    c.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    c.gameObject.GetComponent<Rigidbody>().mass += 2;
                }

            }

        }
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
  
    
    // Update is called once per frame
   
}
