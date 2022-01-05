using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : Spell
{
    public float distance;
	public override Vector3 FindPosition(PlayerTransform player)
	{
        Vector3 pos = player.position;
        pos += player.forward*distance;
        return pos;
	}

	public override void Start()
	{
		base.Start();
	}
}
