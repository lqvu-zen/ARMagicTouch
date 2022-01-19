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
        public GameObject spawnablePrefabs;
        public float timeDelay;
    }
    bool continueSpawn = true;
    public enum MonsterState 
    { 
        Idle, 
        Walk, 
        Attack, 
        Die, 
        Hit
    };

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
    static public Vector3 manaZonePosition;

    public GameObject plane;
    bool plane_spwaned = false;

    public GameObject gate;
    bool gate_spawned = false;
    Vector3 gatePosition = Vector3.zero;

    public GameObject panel;
    public GameObject panel_warning;

    public Transform monsterHolder;

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
        else
        {
            // Set gate
            if (!gate_spawned)
            {
                SpawnGate();
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
            SetWarningPanel("Now! Let find the gate!!");
        }
        
        panel.SetActive(false);
    }

    public void SetWarningPanel(string text)
    {
        if (!panel_warning.activeSelf)
        {
            PanelWarningText.UpdateWarningText(text);
            panel_warning.SetActive(true);
        }
    }

    public void CloseWarningPanel()
    {
        panel_warning.SetActive(false);
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

            manaZone.transform.position = new Vector3(hitPose.position.x, hitPose.position.y - 1f, hitPose.position.z);
        }
    }

    void SetManaZone()
    {
        if (!manaZone_spwaned)
        {
            manaZone_spwaned = true;
            manaZonePosition = manaZone.transform.position;

            if (!plane_spwaned)
            {
                plane_spwaned = true;
                plane.SetActive(true);
                plane.transform.position = new Vector3(manaZone.transform.position.x, manaZone.transform.position.y - 0.05f, manaZone.transform.position.z);
            } 
        }
    }

    void SpawnGate()
    {
        if (arRaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), arRaycastHits, TrackableType.All))
        {
            Pose hitPose = arRaycastHits[0].pose;

            float distance = Vector3.Distance(hitPose.position, manaZone.transform.position);

            if (distance >= 5.0f)
            {
                gatePosition = new Vector3(hitPose.position.x, hitPose.position.y + 1f, hitPose.position.z);

                GameObject gatenew = Instantiate(gate, gatePosition, Quaternion.identity);
                gatenew.transform.parent = monsterHolder;
                gate_spawned = true;

                CloseWarningPanel();

                AutoSpawnMonsters();
            }
        }
    }

    void AutoSpawnMonsters()
    {
        //Spawn monsters
        for (int i = 0; i < spawnableObjects.Length; ++i)
        {
            // Spawn pbjects
            StartCoroutine(SpawnMonster(spawnableObjects[i].spawnablePrefabs, spawnableObjects[i].timeDelay));
        }
    }

    IEnumerator SpawnMonster(GameObject gameObjecty, float timeDelay)
    {
        
        while (continueSpawn)
        {
            spawnedObjects.Add(Instantiate(gameObjecty, gatePosition, Quaternion.identity));
            spawnedObjects[spawnedObjects.Count - 1].transform.parent = monsterHolder;
            yield return new WaitForSeconds(timeDelay);
        }
    }

    void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null)
        {
            ARPlane arPlane = args.added[0];

            Vector3 arPlanePosition = arPlane.transform.position;

        }    
    }
}
