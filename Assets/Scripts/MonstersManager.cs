using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class MonstersManager : MonoBehaviour
{
    [Serializable]
    public struct MonsterInfo
    {
        public int count;
        public GameObject spawnablePrefabs;

        public Vector3 gatePosition;
    }

    [SerializeField]
    ARRaycastManager arRaycastManager;
    ARPlaneManager arPlaneManager;

    public MonsterInfo[] spawnableObjects;

    List<GameObject> spawnedObjects = new List<GameObject>();

    Camera arCamera;
    Vector3 arCameraPosition;

    bool plane_enable = false;
    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();

        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();

        arPlaneManager.planesChanged += PlaneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (arCameraPosition == Vector3.zero)
        {
            arCameraPosition = arCamera.transform.position;
        }  

        for (int i = 0; i < spawnableObjects.Length; ++i)
        {
            if (plane_enable && spawnableObjects[i].count > 0 && spawnableObjects[i].gatePosition != Vector3.zero)
            {
                /*// Get gate position
                if (spawnableObjects[i].gatePosition == Vector3.zero)
                {
                    Vector3 planePosition = arPlaneManager.transform.position;
                    spawnableObjects[i].gatePosition = arCameraPosition + new Vector3(0, planePosition.y, 2);
                }*/    

                // Spawn pbjects
                spawnedObjects.Add(Instantiate(spawnableObjects[i].spawnablePrefabs, spawnableObjects[i].gatePosition, Quaternion.identity));

                spawnableObjects[i].count--;
            }
        }
    }

    void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null)
        {
            ARPlane arPlane = args.added[0];

            Vector3 arPlanePosition = arPlane.transform.position;

            float distance = Vector3.Distance(arCameraPosition, arPlanePosition);

            for (int i = 0; i < spawnableObjects.Length; ++i)
            {
                if (spawnableObjects[i].count > 0 && distance >= 2.0f)
                {
                    // Get gate position
                    if (spawnableObjects[i].gatePosition == Vector3.zero)
                    {
                        spawnableObjects[i].gatePosition = arCameraPosition + new Vector3(0, arPlanePosition.y, arPlanePosition.z);
                    }
                }
            }
        }    

        if (!plane_enable)
        {
            plane_enable = true;

            plane.SetActive(true);
        }    
    }
}
