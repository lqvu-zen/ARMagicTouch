using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public IntVariable health;
    public IntVariable maxHealth;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = 1f*health.value/maxHealth.value;
        if (health.value <= 0 && !GameManager.instance.isDead)
        {
            GameManager.instance.OnDead();
        }
    }

    public void Reset()
    {
        health.value = maxHealth.value;
    }
}
