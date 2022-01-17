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

    static List<ARRaycastHit> arRaycastHits = new List<ARRaycastHit>();

    public MonsterInfo[] spawnableObjects;

    List<GameObject> spawnedObjects = new List<GameObject>();

    Camera arCamera;
    Vector3 arCameraPosition;

    public GameObject manaZone;
    bool manaZone_spwaned = false; 

    public GameObject plane;
    bool plane_spwaned = false;

    public GameObject panel;

    void Awake()
    {
        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();

        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        arPlaneManager.planesChanged += PlaneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (arCameraPosition == Vector3.zero)
        {
            arCameraPosition = arCamera.transform.position;
        }

        // Set mana zone
        if (!manaZone_spwaned)
        {
            SpawnManaZone();
        }    

        for (int i = 0; i < spawnableObjects.Length; ++i)
        {
            if (plane_spwaned && spawnableObjects[i].count > 0 && spawnableObjects[i].gatePosition != Vector3.zero)
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

    public void SetPanel(string text)
    {
        if (!panel.activeSelf)
        {
            DebugText.UpdateDebugText(text);
            panel.SetActive(true);
        }    
    }
    
    public void ClosePanel()
    {
        if (!manaZone_spwaned && manaZone.activeSelf)
        {
            SetManaZone();
            panel.SetActive(false);
        }
    }    

    void SpawnManaZone()
    {
        SetPanel("Let's choose where to protect!!");
        if (arRaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), arRaycastHits, TrackableType.All))
        {
            Pose hitPose = arRaycastHits[0].pose;
            if (!manaZone.activeSelf)
            {
                manaZone.SetActive(true);
            }

            manaZone.transform.position = hitPose.position;
        }
    }

    void SetManaZone()
    {
        if (!manaZone_spwaned)
        {
            manaZone_spwaned = true;

            if (!plane_spwaned)
            {
                plane_spwaned = true;
                plane.transform.position = new Vector3(0f, manaZone.transform.position.y, 0f);
                plane.SetActive(true);
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
    }
}
