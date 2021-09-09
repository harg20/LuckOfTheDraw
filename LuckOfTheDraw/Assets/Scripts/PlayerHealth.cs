using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image hpBar;
    public float maxHealth;
    public float  currentHealth;
    public GameObject resetmenu;
    public CharmUI charmUI;
    public DeckDisplay deckisplay;
    public DeckDisplay deckDisplay;
    public Rigidbody rb;
    public CardDataTracker cdt;
    public DeckDisplay dd;
    public Text hptext;
    public ragdolltoggler ragdollToggle;
    // Start is called before the first frame update
    void Start()
    {

        deckDisplay = FindObjectOfType<DeckDisplay>();
        hptext = GameObject.Find("hptxt").GetComponent<Text>();
        hpBar = GameObject.Find("Health").GetComponent<Image>();
        charmUI = FindObjectOfType<CharmUI>();
        //currentHealth = maxHealth;
        resetmenu = FindObjectOfType<Restart>().transform.parent.gameObject;
        resetmenu.SetActive(false);
        dd = FindObjectOfType<DeckDisplay>();
        hpBar.rectTransform.localScale = new Vector3((currentHealth > 0) ? currentHealth / maxHealth : 0, hpBar.rectTransform.localScale.y, hpBar.rectTransform.localScale.z);
        hptext.text = currentHealth.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!charmUI) { charmUI = FindObjectOfType<CharmUI>(); }
        
        if (collision.gameObject.tag == "EnemyBullet" && currentHealth > 0)
        {
            TakeDamage(collision.gameObject.GetComponent<BulletStats>().Damage);
            Destroy(collision.gameObject);
           
        }
        if (collision.gameObject.tag == "Enemy" && currentHealth > 0)
        {
            TakeDamage(2);
            Vector3 knockback = -(collision.transform.position - transform.position);
            knockback.y = 0;
            rb.AddRelativeForce(knockback*10000);
        }
        if (collision.gameObject.tag == "pickup")
        {
            cdt.pickupcard(collision.gameObject.GetComponentInChildren<CardDisplay>().card);
            dd.UpdateDisplay();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 8)
        {
            TakeDamage(2);
            rb.AddRelativeForce(0, 10000, 0);
        }
        if (collision.gameObject.tag == "Charm")
        {
            charmUI.SetCharm(collision.gameObject.GetComponent<CharmObject>().charm);
            ActivateCharm(collision.gameObject.GetComponent<CharmObject>().charm);
            Destroy(collision.gameObject);
        }
        
    }
    public void ActivateCharm(CharmScript charm)
    {
        GetComponent<Shoot>().chain = false;
        for (int i = 0; i<charm.CharmTypes.Length; i++)
        {
            if (charm.CharmTypes[i].Equals(CharmScript.Effect.BulletChain))
            {
                GetComponent<Shoot>().chain = true;
            }
            if (charm.CharmTypes[i].Equals(CharmScript.Effect.none))
            {
                Debug.Log("gorks");
            }
            if (charm.CharmTypes[i].Equals(CharmScript.Effect.oddsBoost))
            {
                Debug.Log("gorky");
            }
        }
    }
    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        hpBar.rectTransform.localScale = new Vector3((currentHealth > 0) ? currentHealth / maxHealth : 0, hpBar.rectTransform.localScale.y, hpBar.rectTransform.localScale.z);
        if (currentHealth <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Shoot>().enabled = false;
            resetmenu.SetActive(true);
            ragdollToggle.RagdolltoggleOn();
            

        }
        hptext.text = currentHealth.ToString();
    }
    public void AddHealth(float hpgain)
    {

        currentHealth += hpgain;
        hpBar.rectTransform.localScale = new Vector3((currentHealth > 0) ? currentHealth / maxHealth : 0, hpBar.rectTransform.localScale.y, hpBar.rectTransform.localScale.z);
        hptext.text = currentHealth.ToString();
    }
  
}
