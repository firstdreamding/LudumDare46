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
    public float speed;
    public float maxSpeed;

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
        speed = maxSpeed;
    }
    public void Attacked(int damage, Vector3 attackerPos, float knockbackConstant, float knockbackSpeed)
    {
        hp -= damage;
        Vector3 dispalcement = knockbackConstant * (transform.position - attackerPos).normalized;
        knockbackPoint = transform.position + dispalcement;
        this.knockbackSpeed = knockbackSpeed * knockbackConstant;
        resetPoint = transform.position;
        state = State.KNOCKBACK;
    }

    public void OnTrigger(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            hp = (int)(hp - collision.GetComponent<Projectile>().GetDamage());
        }
        else if (collision.tag == "WindProjectile" && collision.GetComponent<WindProjectile>().canHit(gameObject.GetInstanceID()))
        {
            Attacked(0, collision.transform.position, 0.25f, 0.1f);
        }
        else if (collision.tag == "PathTurn")
        {
            Vector2 tempVec = collision.gameObject.GetComponent<TurnTile>().targetMove;
            toNext.Add(tempVec);
        }
        else if (collision.tag == "Pharaoh")
        {
            Debug.Log("Oh no, he was hit");
            Destroy(gameObject);
        }
    }
    public void OnTriggerEx(Collider2D collider)
    {
    }
    private void FixedUpdate()
    {
        if (speed != maxSpeed)
        {
            speed += 0.002f;
        }
        else if (speed > 0)
        {
            speed = maxSpeed;
        }
    }
}
