using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxvel;
    
    Vector3 positionlastframe;
    public Transform rotdat;
    Rigidbody rb;
    float fdt;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fdt = Time.fixedDeltaTime;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerInput();

    }

    void PlayerInput()
    {
     
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = .2f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
         else
         {
             Time.timeScale = 1;
             Time.fixedDeltaTime = fdt;
         }
        if (rb.velocity.magnitude < maxvel)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity += rotdat.forward * speed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity += -rotdat.right * speed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity += -rotdat.forward * speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity += rotdat.right * speed;
            }

        }
    }
}
