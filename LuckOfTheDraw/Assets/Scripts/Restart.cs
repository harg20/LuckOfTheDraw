using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public CardDataTracker carddatatracker;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Button>().onClick.AddListener(resetti);
    }
    void resetti()
    {
        SceneManager.LoadScene(0);
        carddatatracker.restart();
    }
    // Update is called once per frame
  
}
