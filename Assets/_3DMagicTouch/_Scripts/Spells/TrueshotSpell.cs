using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueshotSpell : Spell
{
    public float speed;
    Vector3 defaultPos;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed*transform.forward*Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)){
            transform.position = defaultPos;
        }
    }

    public override Vector3 FindPosition(PlayerTransform player)
    {
        Debug.Log("Child Invoke");
        Vector3 pos = player.position;
        pos.y /= 2;
        return pos;
    }
}
