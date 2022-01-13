using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    Camera arCamera;

    float speed = 0.1f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;

        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set rotation to camera
        Vector3 arCameraVirtualPosition = arCamera.transform.position;
        arCameraVirtualPosition.y = transform.position.y;
        Vector3 lookVector = arCameraVirtualPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookVector);
        transform.rotation = rotation;

        //move to camera
        if (Vector3.Distance(arCameraVirtualPosition, transform.position) >= 1.0f)
        {
            anim.SetFloat("Speed", speed);
            transform.Translate(lookVector * Time.deltaTime * -speed, Space.Self);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }    
    }
}
