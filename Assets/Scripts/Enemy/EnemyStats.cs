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
        Vector2 displacement = knockbackConstant * (transform.position - attackerPos).normalized;
        knockbackPoint = (Vector2)transform.position + displacement;
        this.knockbackSpeed = knockbackSpeed * knockbackConstant;
        Vector2 b = toNext[0] - (Vector2)transform.position;
        resetPoint = (Vector2)transform.position + b * Vector2.Dot(displacement, b) / Vector2.Dot(b, b);
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
            float nextWidth = 0.8f * collision.gameObject.GetComponent<SpriteRenderer>().size.x;
            Vector2 randVec = new Vector2(nextWidth * (Random.value - 0.5f), nextWidth * (Random.value - 0.5f));
            toNext.Add(tempVec + randVec);
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
