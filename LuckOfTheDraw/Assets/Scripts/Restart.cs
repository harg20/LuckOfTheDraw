using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public CardDataTracker carddatatracker;
    public PlayerHealth ph;
    public DeckDisplay dd;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Button>().onClick.AddListener(resetti);
        ph = FindObjectOfType<PlayerHealth>();
    }
    void resetti()
    {
        SceneManager.LoadScene(0);
        carddatatracker.restart();
        ph.currentHealth = ph.maxHealth;

        dd.ResetDisplay();
    }
    private void Update()
    {
       if (!ph) ph = FindObjectOfType<PlayerHealth>();
    }
    // Update is called once per frame

}
