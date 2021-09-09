using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
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
    public GameObject Player;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        Player = GameObject.Find("Player");
        cam = Camera.main;
        enemyManager.AddEnemy(gameObject);
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
        if (collision.gameObject.tag == "bullet")
        {
            //Debug.Log("projbullet");
            Instantiate(hitEffect, collision.GetContact(0).point, Quaternion.LookRotation(collision.GetContact(0).normal));
            TakeDamage(collision.gameObject.GetComponent<bulletinfo>().Damage);
            Destroy(collision.gameObject);
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
        TargetPlayer();
        if (!enemyManager) { enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>(); }

        if (firerate > 0)
        {
            firerate -= Time.deltaTime;

        }
        if (currentHealth <= 0)
        {
            enemyManager.RemoveEnemy(gameObject);
            if (GetComponent<Enemy>().enemyType == Enemy.EnemyType.Ranged) enemyManager.RemoveShooter(gameObject);

            Destroy(gameObject);
        }
    }
    public void TargetPlayer()
    {
        // base decision making for all AI
        
            if (!GetComponent<Enemy>().distracted)
            {
                if (GetComponent<Enemy>().enemyType == Enemy.EnemyType.Chasedown) { GetComponent<NavMeshAgent>().destination = Player.transform.position; }

                if (GetComponent<Enemy>().enemyType == Enemy.EnemyType.Ranged)
                {

                    if (Vector3.Distance(transform.position, Player.transform.position) > 10)
                    {
                        GetComponent<NavMeshAgent>().destination = Player.transform.position;
                    }
                    else
                    {
                        GetComponent<NavMeshAgent>().destination = transform.position;


                        if (!enemyManager.shooters.Contains(gameObject)) enemyManager.AddShooter(gameObject);

                        if (enemyManager.shooters.Count <= 3 && enemyManager.shooters.Contains(gameObject))
                        {
                            ShootPlayer();
                        }
                        else
                       {
                           for (int i = enemyManager.shooters.Count; i > 3; i--)
                           {
                               Debug.Log("sheesh");
                            enemyManager.RemoveShooter(gameObject);
                    
                            }

                        }

                    }
                    var lookAtPoint = Player.transform.position;
                    lookAtPoint.y = transform.position.y;
                    transform.LookAt(lookAtPoint);

                }
            }
           GetComponentInChildren<Canvas>().transform.LookAt(cam.transform);
        

    }
    public void TargetSmoke(Transform Smoke)
    {
        //called when a smoke is spawned, draws attention of all nearby enemies for a few seconds
      
            if (Vector3.Distance(transform.position, Smoke.position) < 10)
            {
                if (GetComponent<Enemy>().enemyType == Enemy.EnemyType.Ranged)
                {


                    var lookAtPoint = Smoke.transform.position;
                    lookAtPoint.y = transform.position.y;
                    transform.LookAt(lookAtPoint);
                    GetComponent<Enemy>().distracted = true;
                    GetComponent<Enemy>().shooting = true;
                    ShootPlayer();
                }
                if (GetComponent<Enemy>().enemyType == Enemy.EnemyType.Chasedown)
                {
                    var lookAtPoint = Smoke.transform.position;
                    lookAtPoint.y = transform.position.y;
                    transform.LookAt(lookAtPoint);
                    GetComponent<Enemy>().distracted = true;
                }
            }

        }
    }


  
    

    

