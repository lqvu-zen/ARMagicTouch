using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] navMeshSurfaces;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var surface in navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
