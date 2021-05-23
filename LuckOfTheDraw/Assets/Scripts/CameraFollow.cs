using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
  public  float height = 5f;
   public float distance = 10f;
   public float rotDamping = 5f;
    bool freecam;

	
	// Update is called once per frame
	void FixedUpdate () {
        

            float wantedRotationAngle = target.eulerAngles.y +180;
            float currentRotationAngle = transform.eulerAngles.y;
            float wantedHeight = target.position.y + height;
            float currentHeight = transform.position.y;

       // currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotDamping * Time.deltaTime);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            freecam = true;
             //wantedHeight = (Input.mousePosition.y * -.1f  + 50) + target.position.y + height;
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, Input.mousePosition.magnitude, rotDamping * Time.deltaTime);

        }

       
        //LERP the rotation
        

      
        //LERP the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, Time.deltaTime);
        //Get the rotation
        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);
        //position the camera
        transform.position = target.transform.position;
        //set its offset distance
        transform.position -= currentRotation * Vector3.forward * distance;
        //set its offset height
        Vector3 newHeight = transform.position;
        newHeight.y = currentHeight;
        transform.position = newHeight;
        transform.LookAt(target);
      
       
    }
}
