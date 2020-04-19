using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //don't hardcode these, defined in functions
    public Vector2 resetPoint;
    public Vector2 knockbackPoint;
    public float knockbackSpeed;
    public enum State
    {
        KNOCKBACK,
        OFFPATH,
        ONPATH,
        DEAD
    }
    public List<Vector2> toNext;
    public State state;
    public int hp;

    public void SetUp(int hp)
    {
        this.hp = hp;
    }

    public void Init(Vector2 firstPoint)
    {

        state = State.ONPATH;
        toNext = new List<Vector2>();
        toNext.Add(new Vector3(firstPoint.x, firstPoint.y, transform.position.z));
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);

        anim.SetFloat("SpeedX", (firstPoint.x - transform.position.x));
        anim.SetFloat("SpeedY", (firstPoint.y - transform.position.y));
    }
    public void Attacked(int damage, Vector3 attackerPos, float knockbackConstant)
    {
        hp -= damage;
        Vector3 dispalcement = knockbackConstant * (transform.position - attackerPos).normalized;
        knockbackPoint = transform.position + dispalcement;
        knockbackSpeed = 0.5f * knockbackConstant;
        resetPoint = transform.position;
        state = State.KNOCKBACK;
    }
}
