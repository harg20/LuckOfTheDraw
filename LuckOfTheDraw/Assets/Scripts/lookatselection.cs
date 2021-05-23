using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatselection : MonoBehaviour
{
    public float multi = 0;
    public GameObject cube;
    Camera m_camera;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var lookAtPoint = cube.transform.position;
        lookAtPoint.y = transform.position.y;
        transform.LookAt(lookAtPoint);
        Debug.DrawRay(transform.position, transform.forward*100);
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, lookAtPoint);
        lr.startColor = Color.red;
        // Debug.Log(lookAtPoint);
        //Ray mouseray = m_camera.ScreenPointToRay(Input.mousePosition);
        // transform.LookAt(mouseray.origin + mouseray.direction * transform.position.y* multi);

        // Debug.DrawRay(mouseray.origin, mouseray.direction * transform.position.y * multi);
    }
}
