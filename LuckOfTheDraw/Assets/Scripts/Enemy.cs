using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image healthUI;
    public Transform muzzle;
    public enum EnemyType { Ranged, Chasedown };
    public EnemyType enemyType;
    public GameObject projectile;
    public GameObject hitEffect;
    public EnemyManager enemyManager;
    float firerate = 1;
    public bool distracted = false;
    public bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }
    public void TakeDamage(int Damage)
    {
        if (currentHealth > 0)
        {

            currentHealth -= Damage;
            healthUI.rectTransform.localScale = new Vector3( (currentHealth >0) ? currentHealth / maxHealth: 0, healthUI.rectTransform.localScale.y, healthUI.rectTransform.localScale.z);

        }
        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Instantiate(hitEffect, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
            TakeDamage(3);
        }
    }

    public void ShootPlayer()
    {
        if (firerate <= 0)
        {
            var bullet = Instantiate(projectile, muzzle.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000,ForceMode.Acceleration);
            firerate = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (firerate > 0)
        {
            firerate -= Time.deltaTime;

        }
        if (currentHealth <= 0)
        {
            enemyManager.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}
