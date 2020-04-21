using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ankh : MonoBehaviour
{
    private float active;
    Transform laser;
    private float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        active = 0.25f + Time.time;
        laser = transform.Find("Hit").transform;
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        Destroy(gameObject, duration);
    }

    void FixedUpdate()
    {
        if (transform.localScale.y < 1)
        {
            transform.localScale += new Vector3(0, Time.deltaTime / 0.5f, 0);
            Debug.Log(transform.localScale);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > active && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyStats>().hp -= 100;
        }
    }
}
