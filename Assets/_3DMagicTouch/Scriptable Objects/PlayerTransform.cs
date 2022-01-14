using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTransform", menuName = "ScriptableObjects/PlayerTransform")]
public class PlayerTransform : ScriptableObject 
{
    public Vector3 position;
    public Vector3 forward;
    public Quaternion rotation;
}
