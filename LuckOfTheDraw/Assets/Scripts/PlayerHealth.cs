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
    public Rigidbody rb;
    public CardDataTracker cdt;
    public DeckDisplay dd;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        resetmenu = FindObjectOfType<Restart>().transform.parent.gameObject;
        resetmenu.SetActive(false);
        dd = FindObjectOfType<DeckDisplay>();
    }
    private void OnCollisionEnter(Collision collision)
    {
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
        
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        hpBar.rectTransform.localScale = new Vector3((currentHealth > 0) ? currentHealth / maxHealth : 0, hpBar.rectTransform.localScale.y, hpBar.rectTransform.localScale.z);
        if (currentHealth <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            
            resetmenu.SetActive(true);
        }
    }
    public void AddHealth(float hpgain)
    {
        currentHealth += hpgain;
        hpBar.rectTransform.localScale = new Vector3((currentHealth > 0) ? currentHealth / maxHealth : 0, hpBar.rectTransform.localScale.y, hpBar.rectTransform.localScale.z);
    }
  
}
