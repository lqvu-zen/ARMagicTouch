using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    Camera arCamera;

    float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;

        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set rotation to camera
        Vector3 lookVector = arCamera.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookVector);
        transform.rotation = rotation;

        //move to camera
        if (Vector3.Distance(arCamera.transform.position, transform.position) >= 2.0f)
        {
            transform.Translate(lookVector * Time.deltaTime * -speed, Space.Self);
        }    
    }
}
