using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] meshSurfaces;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < meshSurfaces.Length; i++)
        {
            meshSurfaces[i].BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
