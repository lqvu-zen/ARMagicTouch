using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public ParticleSystem vfx;
    public string spellName;
    // Start is called before the first frame update
    public virtual void Start()
    {
        vfx.Play();
        Destroy(gameObject, 5f);
    }

    public virtual Vector3 FindPosition( PlayerTransform player){
        Debug.Log("Parent Invoke");
        return Vector3.zero;
    }
}
