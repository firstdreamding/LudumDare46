using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : MonoBehaviour
{
    public enum State
    {
        KNOCKBACK,
        OFFPATH,
        ONPATH,
        DEAD
    }
    
    public float speed;

    //don't hardcode these, defined in functions
    private Vector2 resetPoint;
    private Vector2 knockbackPoint;
    private float knockbackSpeed;
    private Animator anim;
    private EnemyStats enemyStats;

    public List<Vector2> toNext;


    private State state;

    public void Init(Vector2 firstPoint)
    {
        enemyStats = GetComponent<EnemyStats>();

        state = State.ONPATH;
        toNext = new List<Vector2>();
        toNext.Add(new Vector3(firstPoint.x, firstPoint.y, transform.position.z));
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

        anim.SetFloat("SpeedX", (firstPoint.x - transform.position.x));
        anim.SetFloat("SpeedY", (firstPoint.y - transform.position.y));
    }

    void Update()
    {
        if (enemyStats.hp == 0 && state != State.DEAD)
        {
            state = State.DEAD;
            anim.SetBool("Dead", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.OFFPATH:
                transform.position = Vector3.MoveTowards(transform.position, resetPoint, speed);
                if (Vector3.Distance(transform.position, resetPoint) < 0.001f)
                {
                    state = State.ONPATH;
                }
                break;
            case State.ONPATH:
                OnPath();

                break;
            case State.KNOCKBACK:
                transform.position = Vector3.MoveTowards(transform.position, knockbackPoint, knockbackSpeed);
                if (Vector3.Distance(transform.position, knockbackPoint) < 0.001f)
                {
                    state = State.OFFPATH;
                }
                break;
        }
    }

    private void OnPath()
    {
        if (toNext.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, toNext[0], speed);
            if (transform.position.x == toNext[0].x && transform.position.y == toNext[0].y)
            {
                toNext.Remove(toNext[0]);
                if (toNext.Count > 0)
                {
                    anim.SetFloat("SpeedX", (toNext[0].x - transform.position.x));
                    anim.SetFloat("SpeedY", (toNext[0].y - transform.position.y));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Projectile")
        {
            Debug.Log("Ouch");
            enemyStats.hp = (int)(enemyStats.hp - collision.GetComponent<Projectile>().GetDamage());
        } else if (collision.tag == "PathTurn")
        {
            Vector2 tempVec = collision.gameObject.GetComponent<TurnTile>().targetMove;
            toNext.Add(tempVec);
        } else if (collision.tag == "Pharaoh")
        {
            Debug.Log("Oh no, he was hit");
            Destroy(gameObject);
        }
    }
    public void Attacked(int damage, Vector3 attackerPos, float knockbackConstant)
    {
        enemyStats.hp -= damage;
        Vector3 dispalcement = knockbackConstant * (transform.position - attackerPos).normalized;
        knockbackPoint = transform.position + dispalcement;
        knockbackSpeed = 0.5f * knockbackConstant;
        resetPoint = transform.position;
        state = State.KNOCKBACK;
    }

    public void CompletedDeath()
    {
        Destroy(gameObject);
    }
}
