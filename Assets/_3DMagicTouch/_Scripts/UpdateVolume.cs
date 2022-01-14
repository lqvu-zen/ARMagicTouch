using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVolume : MonoBehaviour
{
    public FloatVariable floatVariable;
    public Slider volume;

    void Update()
    {
        floatVariable.value = volume.value;
    }
}
