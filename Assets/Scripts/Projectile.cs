using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
    private GameObject target;
    private float step;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        step = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetValues(GameObject target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
