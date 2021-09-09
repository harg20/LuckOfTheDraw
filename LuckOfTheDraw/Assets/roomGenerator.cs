using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class roomGenerator : MonoBehaviour
{
    [SerializeField] GameObject floorcube;
    [SerializeField] GameObject cover;
    [SerializeField] int baseFloorTiles;
    [SerializeField] int coverPieces;
    [SerializeField] int baseEnemies;
    [SerializeField] GameObject[] covers;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject enemyManager;
    public List<NavMeshSurface> meshSurfaces;
    float covertimer = 1f;
    bool coverplaced = false;
    int layerMask = 1 << 0;

    public List<GameObject> tiles;
    int p = 0;
    RaycastHit hit;
    // Start is called before the first frame update
    void Awake()
    {
        PlaceTiles();
       

    }
    private void Start()
    {
        //PlaceCover();
    }


    public void PlaceTiles()
    {
        var origin = Instantiate(floorcube);
        origin.transform.position = new Vector3(0, -6.8f, 10);
        origin.transform.localScale = new Vector3(origin.transform.localScale.x * Random.Range(5, 20), 1, origin.transform.localScale.z * Random.Range(5, 20));
        tiles.Add(origin);

        for (int i = 0; i < baseFloorTiles; i++)
        {
            int x = Random.Range(-18, 18);
            int z = Random.Range(-8, 8);
            var tile = Instantiate(floorcube,gameObject.transform);
            tile.transform.position = new Vector3(x, -6.8f, z);
            tile.transform.localScale = new Vector3(tile.transform.localScale.x * Random.Range(5, 20), 1, tile.transform.localScale.z * Random.Range(5, 20));
            tiles.Add(tile);
        }
        for(int j = 0; j < tiles.Count; j++)
        {
            
                if (Vector3.Distance(tiles[j].transform.position,tiles[p].transform.position) > 10)
                {
                    var tile = Instantiate(floorcube,gameObject.transform);
                    tile.name = "Connector";
                    tile.transform.position = tiles[j].transform.position + tiles[p].transform.position;
                tile.transform.position = new Vector3(tile.transform.position.x, -6.8f, tile.transform.position.z);
                    tile.transform.LookAt(tiles[j].transform);
                    tile.transform.localScale = new Vector3(tile.transform.localScale.x * Random.Range(5, 10), 1, tile.transform.localScale.z * 20);
                p++;
                }
            
        }
      
        //PlaceCover();
    }

    public void PlaceCover()
    {
        for (int i = 0; i < coverPieces; i++)
        {
            int x = Random.Range(-19, 19);
            int z = Random.Range(-8, 8);
            int j = Random.Range(0, covers.Length);
            Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out hit, 10,layerMask);
            Debug.DrawRay(new Vector3(x, 0, z), Vector3.down * 10,Color.blue,10);
            if (hit.collider)
            {
                Debug.Log(hit.collider.gameObject);
                var coverpiece = Instantiate(covers[j]);
                coverpiece.transform.position = hit.point;
                int y = Random.Range(-180, 180);
                coverpiece.transform.localEulerAngles = new Vector3(0, y, 0);
            }
            else
            {
                Debug.Log("Bruh");
            }
        }
    }

    void GenerateNavMesh()
    {
        for (int i = 0; i < 1; i++)
        {
           GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }

    void AddEnemies()
    {
        //Instantiate(enemyManager);
        for (int i = 0; i< baseEnemies; i++)
        {
            int x = Random.Range(-15, 15);
            int z = Random.Range(-8, 8);
            int j = Random.Range(0, enemies.Length);
            Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out hit, 10, layerMask);
            Debug.DrawRay(new Vector3(x, 0, z), Vector3.down * 10, Color.red, 10);
            if (hit.collider)
            {
                Debug.Log(hit.collider.gameObject);
                var enemy = Instantiate(enemies[j]);
                enemy.transform.position = new Vector3 (hit.point.x,hit.point.y+1,hit.point.z);
                
                
            }
            else
            {
                Debug.Log("Bruh");
            }
           

        }

    }
    // Update is called once per frame
    void Update()
    {
        covertimer -= Time.deltaTime;
        if (covertimer <= 0 && coverplaced == false)
        {
            
            PlaceCover();
            GenerateNavMesh();
            AddEnemies();
            coverplaced = true;
        }
      
    }
}
