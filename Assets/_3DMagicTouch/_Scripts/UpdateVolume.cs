using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVolume : MonoBehaviour
{
    public FloatVariable floatVariable;
    public Slider volume;

    void Awake()
    {
        volume.value = floatVariable.value;
    }

    void Update()
    {
        floatVariable.value = volume.value;
    }
}
