using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public ParticleSystem vfx;
    public string spellName;
    // Start is called before the first frame update
    void Start()
    {
        vfx.Play();
        Destroy(gameObject, 5f);
    }
}
