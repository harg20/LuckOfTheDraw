using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followmouse : MonoBehaviour
{
    RaycastHit hit;
    Vector3 firsthit;
    bool sety = false;
    int layerMas = 1 << 8;
    Camera maincam;

    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       // var lookAtPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //ookAtPoint.y = transform.position.y;
        //transform.position = lookAtPoint;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.TransformDirection(Vector3.forward) * 500,Color.blue);

        if (Physics.Raycast(maincam.ScreenToWorldPoint(Input.mousePosition), maincam.transform.TransformDirection(Vector3.forward), out hit, 500,layerMas, QueryTriggerInteraction.Collide))
        {
            if (!sety)
            {
                firsthit = hit.point;
                sety = true;
            }

            transform.position = new Vector3( hit.point.x,firsthit.y +1,hit.point.z-1);

        }

        //transform.position = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }
}
