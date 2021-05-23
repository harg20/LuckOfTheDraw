using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> enemies;
    Camera cam;
    public List<GameObject> shooters;
    public GameObject portal;
    //List<GameObject> shooters;
    // Start is called before the first frame update
    void Start()
    {
        // portal = GameObject.FindGameObjectWithTag("portal");
        //portal.SetActive(false);
        // enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        // Player = GameObject.FindGameObjectWithTag("Player");
        // cam = Camera.main;
        
        if (FindObjectsOfType<EnemyManager>().Length  == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewScene()
    {
        portal = GameObject.FindGameObjectWithTag("portal");
        portal.SetActive(false);
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        Player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        shooters.Clear();
    }
    public void TargetPlayer()
    {
        // base decision making for all AI
        for(int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].GetComponent<Enemy>().distracted)
            {
                if (enemies[i].GetComponent<Enemy>().enemyType == Enemy.EnemyType.Chasedown) { enemies[i].GetComponent<NavMeshAgent>().destination = Player.transform.position; }

                    if (enemies[i].GetComponent<Enemy>().enemyType == Enemy.EnemyType.Ranged)
                {

                    if (Vector3.Distance(enemies[i].transform.position, Player.transform.position) > 10)
                    {
                        enemies[i].GetComponent<NavMeshAgent>().destination = Player.transform.position;
                    }
                    else
                    {
                        enemies[i].GetComponent<NavMeshAgent>().destination = enemies[i].transform.position;


                        if (!shooters.Contains(enemies[i])) shooters.Add(enemies[i]);

                        if (shooters.Count <= 3 && shooters.Contains(enemies[i]))
                        {
                            ShootPlayer();
                        }
                        else
                        {
                            for (i = shooters.Count; i > 3; i--)
                            {
                                Debug.Log("sheesh");
                                shooters.Remove(shooters[shooters.Count - 1]);

                            }

                        }

                    }
                    var lookAtPoint = Player.transform.position;
                    lookAtPoint.y = enemies[i].transform.position.y;
                    enemies[i].transform.LookAt(lookAtPoint);
                    
                }
            }
            enemies[i].GetComponentInChildren<Canvas>().transform.LookAt(cam.transform);
        }

    }
    public void TargetSmoke(Transform Smoke)
    {
        //called when a smoke is spawned, draws attention of all nearby enemies for a few seconds
        for (int i = 0; i < enemies.Count; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, Smoke.position) < 10)
            {
                if (enemies[i].GetComponent<Enemy>().enemyType == Enemy.EnemyType.Ranged)
                {


                    var lookAtPoint = Smoke.transform.position;
                    lookAtPoint.y = enemies[i].transform.position.y;
                    enemies[i].transform.LookAt(lookAtPoint);
                    enemies[i].GetComponent<Enemy>().distracted = true;
                    enemies[i].GetComponent<Enemy>().shooting = true;
                    ShootPlayer();
                }
                if (enemies[i].GetComponent<Enemy>().enemyType == Enemy.EnemyType.Chasedown)
                {
                    var lookAtPoint = Smoke.transform.position;
                    lookAtPoint.y = enemies[i].transform.position.y;
                    enemies[i].transform.LookAt(lookAtPoint);
                    enemies[i].GetComponent<Enemy>().distracted = true;
                }
            }

        }
    }
    public void Undistract()
    {
        //called when smoke effect wears off
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Enemy>().distracted = false;
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Count == 1)
        {
            // last enemy of a level always drops a card
            Debug.Log(enemy.name + "Dropcards");
            GetComponent<CardDrop>().SpawnCard(enemies[0]);
            
        }
            enemies.Remove(enemy);
        if (shooters.Contains(enemy)) shooters.Remove(enemy);

        if (enemies.Count == 0)
        {
            portal.SetActive(true);
        }
    }

    public void ShootPlayer()
    {

            
            for(int i = 0; i < shooters.Count; i++)
            {
                shooters[i].GetComponent<Enemy>().ShootPlayer();
            }
        

    }
   
    // Update is called once per frame
    void Update()
    {
        TargetPlayer();

    }
}
