using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    EnemyManager emg;
    int scn;
    // Start is called before the first frame update
    void Start()
    {
       emg = GameObject.FindObjectOfType<EnemyManager>();
       emg.NewScene();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        scn = Random.Range(0, SceneManager.sceneCountInBuildSettings);
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scn);
            emg.NewScene();
            Debug.Log("Teleport");
        }
    }
    // Update is called once per frame
   
}
