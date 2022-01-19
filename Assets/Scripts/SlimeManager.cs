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
    public IntVariable score;

    bool died = false;

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
        if (!died)
        {
            died = true;
            //anim.SetInteger("State", ((int)MonstersManager.MonsterState.Die));
            anim.Play("Die");
            score.value += 1;
            HighscoreController.SetHighscore(score.value);
            StartCoroutine(DestroyMonster(2));
        }    
    }

    IEnumerator DestroyMonster(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            Destroy(collision.gameObject);
            hp--;
        }    
        else if (collision.gameObject.tag == "ERZ")
        {
            hp -= 2;
        }
        else if (collision.gameObject.tag == "Thunder")
        {
            hp -= 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            Destroy(other.gameObject);
            hp--;
        }
        else if (other.gameObject.CompareTag("EZR"))
        {
            hp -= 2;
        }
        else if (other.gameObject.CompareTag("Thunder"))
        {
            hp -= 3;
        }
        anim.Play("GetHit");
    }

    void AttackPlayer()
    {
        playerHealth.value -=1;
    }
}
