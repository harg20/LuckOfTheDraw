using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxvel;
    public Image slomometer;
    
    Vector3 positionlastframe;
    public Transform rotdat;
    Rigidbody rb;
    float fdt;
    public float slomocooldown = 1;
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

        if (Input.GetKey(KeyCode.Space) && slomometer.rectTransform.localScale.x > 0)
        {

            Time.timeScale = .2f;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            slomometer.rectTransform.localScale -= new Vector3(.005f, 0, 0);
            slomocooldown = 1;
        }
         else
         {
             Time.timeScale = 1;
             Time.fixedDeltaTime = fdt;
            slomocooldown -= .01f;
            if (slomocooldown <= 0 && slomometer.rectTransform.localScale.x < 1)
            {
                slomometer.rectTransform.localScale += new Vector3(.01f, 0, 0);
                
            }
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
