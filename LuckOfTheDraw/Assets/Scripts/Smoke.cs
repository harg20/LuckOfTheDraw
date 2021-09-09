using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    EnemyManager enemyManager;
    public float distractiontime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        distractiontime -= Time.deltaTime;
        if (distractiontime > 0)
        {
           // enemyManager.TargetSmoke(transform);
        }
        else
        {
           // enemyManager.Undistract();
        }
    }
}
