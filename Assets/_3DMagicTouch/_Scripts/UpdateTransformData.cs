using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTransformData : MonoBehaviour
{
    public PlayerTransform playerTransform;

    // Update is called once per frame
    void Update()
    {
        playerTransform.position = transform.position;
        playerTransform.forward = transform.forward;
        playerTransform.rotation = transform.rotation;
    }
}
