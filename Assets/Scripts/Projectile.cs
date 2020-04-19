using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float timeLast;
    private Vector3 normalizedDirection;
    private float step;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        step = speed;
        Destroy(gameObject, timeLast);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += normalizedDirection * speed;
    }

    public void SetValues(GameObject target, float damage)
    {
        normalizedDirection = (new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z) - transform.position).normalized;
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
