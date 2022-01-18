using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public float speed;
    public int hp;

    Animator anim;

    GameObject target = null;

    public IntVariable playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Set rotation to target
        Vector3 targetVirtualPosition = MonstersManager.manaZonePosition;
        targetVirtualPosition.y = transform.position.y;
        Vector3 lookVector = targetVirtualPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookVector);
        transform.rotation = rotation;

        //move to target
        if (hp > 0)
        {
            if (Vector3.Distance(targetVirtualPosition, transform.position) >= 1.0f)
            {
                WalkTo(lookVector);
            }
            else
            {
                AttackTarget();
            }
        }
        else
        {
            Die();
        }
         
    }

    void WalkTo(Vector3 lookVector)
    {
        anim.SetInteger("State", ((int)MonstersManager.MonsterState.Walk));
        transform.Translate(lookVector * Time.deltaTime * -speed, Space.Self);
    }   
    
    void AttackTarget()
    {
        anim.SetInteger("State", ((int)MonstersManager.MonsterState.Attack));
    }   
    
    void Die()
    {
        anim.SetInteger("State", ((int)MonstersManager.MonsterState.Die));
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            Destroy(collision.gameObject);
            hp--;
        }    
    }
    void AttackPlayer()
    {
        playerHealth.value -=1;
    }
}
