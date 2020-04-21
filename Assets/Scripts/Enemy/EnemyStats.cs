using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //don't hardcode these, defined in functions
    [HideInInspector] public Vector2 resetPoint;
    [HideInInspector] public Vector2 knockbackPoint;
    [HideInInspector] public float knockbackSpeed;
    [HideInInspector] public State state;
    [HideInInspector] public List<Vector2> toNext;
    public GameObject deathPrefab;
    public int goldDrop;
    public enum State
    {
        KNOCKBACK,
        OFFPATH,
        ONPATH,
        DEAD
    }
    public int hp;
    public float speed;

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
    public void Attacked(int damage, Vector3 attackerPos, float knockbackConstant, float knockbackSpeed)
    {
        hp -= damage;
        Vector2 displacement = knockbackConstant * (transform.position - attackerPos).normalized;
        knockbackPoint = (Vector2)transform.position + displacement;
        this.knockbackSpeed = knockbackSpeed;
        Vector2 b = toNext[0] - (Vector2)transform.position;
        resetPoint = (Vector2)transform.position + b * Vector2.Dot(displacement, b) / Vector2.Dot(b, b);
        state = State.KNOCKBACK;
    }

    private HashSet<int> cornersmet = new HashSet<int>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            hp = (int)(hp - collision.GetComponent<Projectile>().GetDamage());
        }
        else if (collision.tag == "WindProjectile" && collision.GetComponent<WindProjectile>().canHit(gameObject.GetInstanceID()))
        {
            Attacked(0, collision.transform.position, 1f, 0.05f);
        }
        else if (collision.tag == "Spear")
        {
            hp = (int)(hp - collision.GetComponent<Spear>().GetDamage());
        }
        else if (collision.tag == "Magic")
        {
            GameObject temp1 = Instantiate(collision.GetComponent<Magic>().magic, transform.position, Quaternion.identity);
            temp1.transform.parent = transform;
            hp = (int)(hp - collision.GetComponent<Magic>().GetDamage());
        }
        else if (collision.tag == "PathTurn")
        {
            if (!cornersmet.Contains(collision.GetInstanceID()))
            {
                cornersmet.Add(collision.GetInstanceID());
                Vector2 tempVec = collision.gameObject.GetComponent<TurnTile>().targetMove;
                float nextWidth = 0.8f * collision.gameObject.GetComponent<SpriteRenderer>().size.x;
                Vector2 randVec = new Vector2(nextWidth * (Random.value - 0.5f), nextWidth * (Random.value - 0.5f));
                toNext.Add(tempVec + randVec);
            }
        }
        else if (collision.tag == "Pharaoh")
        {
            MainScript.MSCRIPT.heath -= 1;
            CompletedDeath();
        }
    }

    public void CompletedDeath()
    {
        WaveManager.wm.enemiesInGame--;
        Instantiate(deathPrefab, new Vector3(transform.position.x, transform.position.y, -4), Quaternion.identity);
        Destroy(gameObject);
    }
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        WaveManager.wm.enemiesInGame++;
    }

    private void Update()
    {
        if (hp <= 0 && state != EnemyStats.State.DEAD)
        {
            state = EnemyStats.State.DEAD;
            anim.SetBool("Dead", true);
            GetComponent<BoxCollider2D>().enabled = false;
            PlayerScript.player.incGold(goldDrop);
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.OFFPATH:
                transform.position = Vector3.MoveTowards(transform.position, resetPoint, speed);
                Vector2 dr = (resetPoint - (Vector2)transform.position).normalized;
                anim.SetFloat("SpeedX", dr.x);
                anim.SetFloat("SpeedY", dr.y);
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
            anim.SetFloat("SpeedX", toNext[0].x - transform.position.x);
            anim.SetFloat("SpeedY", toNext[0].y - transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, toNext[0], speed);
            if (Vector2.Distance(transform.position, toNext[0]) < 0.001f)
            {
                toNext.Remove(toNext[0]);
            }
        }
    }
}
