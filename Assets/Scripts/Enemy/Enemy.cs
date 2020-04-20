using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : MonoBehaviour
{
    //don't hardcode these, defined in functions
    private Animator anim;
    private EnemyStats enemyStats;

    public void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemyStats.hp == 0 && enemyStats.state != EnemyStats.State.DEAD)
        {
            enemyStats.state = EnemyStats.State.DEAD;
            anim.SetBool("Dead", true);
            GetComponent<BoxCollider2D>().enabled = false;
            PlayerScript.player.incGold(enemyStats.goldDrop);
        }
    }
    private void FixedUpdate()
    {
        switch (enemyStats.state)
        {
            case EnemyStats.State.OFFPATH:
                transform.position = Vector3.MoveTowards(transform.position, enemyStats.resetPoint, enemyStats.speed);
                Vector2 dr = (enemyStats.resetPoint - (Vector2)transform.position).normalized;
                Debug.Log(dr);
                anim.SetFloat("SpeedX", dr.x);
                anim.SetFloat("SpeedY", dr.y);
                if (Vector3.Distance(transform.position, enemyStats.resetPoint) < 0.001f)
                {
                    enemyStats.state = EnemyStats.State.ONPATH;
                }
                break;
            case EnemyStats.State.ONPATH:
                OnPath();
                break;
            case EnemyStats.State.KNOCKBACK:
                transform.position = Vector3.MoveTowards(transform.position, enemyStats.knockbackPoint, enemyStats.knockbackSpeed);
                if (Vector3.Distance(transform.position, enemyStats.knockbackPoint) < 0.001f)
                {
                    //Vector2 b = enemyStats.toNext[0] - enemyStats.resetPoint;
                    //Vector2 displacement = (Vector2)transform.position - enemyStats.resetPoint;
                    //enemyStats.resetPoint = enemyStats.resetPoint + b * Vector2.Dot(displacement, b) / Vector2.Dot(b, b);
                    //Debug.Log(enemyStats.resetPoint + " ARRIVED");
                    //Instantiate(deathPrefab, enemyStats.resetPoint, Quaternion.identity);
                    enemyStats.state = EnemyStats.State.OFFPATH;
                }
                break;
        }
    }

    private void OnPath()
    {
        if (enemyStats.toNext.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyStats.toNext[0], enemyStats.speed);
            if (transform.position.x == enemyStats.toNext[0].x && transform.position.y == enemyStats.toNext[0].y)
            {
                enemyStats.toNext.Remove(enemyStats.toNext[0]);
                if (enemyStats.toNext.Count > 0)
                {
                    anim.SetFloat("SpeedX", (enemyStats.toNext[0].x - transform.position.x));
                    anim.SetFloat("SpeedY", (enemyStats.toNext[0].y - transform.position.y));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyStats.OnTrigger(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyStats.OnTriggerEx(collision);
    }

}
