using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    KNOCKBACK,
    OFFPATH,
    ONPATH
}
public class Enemy : MonoBehaviour
{

    public int hp;
    public float speed;

    //don't hardcode these, defined in functions
    private Vector2 resetPoint;
    private Vector2 knockbackPoint;
    private float knockbackSpeed;


    private State state;
    void Start()
    {
        state = State.ONPATH;
    }

    void Update()
    {
        if (hp == 0)
        {
            Destroy(gameObject);
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
                transform.localPosition += new Vector3(speed, 0, 0);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Projectile")
        {
            Debug.Log("Ouch");
            hp = (int)(hp - collision.GetComponent<Projectile>().GetDamage());
        }
        else
        {
            Debug.Log("else");
        }
    }
    public void Attacked(int damage, Vector3 attackerPos, float knockbackConstant)
    {
        hp -= damage;
        Vector3 dispalcement = knockbackConstant * (transform.position - attackerPos).normalized;
        knockbackPoint = transform.position + dispalcement;
        knockbackSpeed = 5 * knockbackConstant;
        resetPoint = transform.position;
        state = State.KNOCKBACK;
    }
}
