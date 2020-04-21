using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOfHorus : MonoBehaviour
{
    private float active;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
        active = Time.time + 1.5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > active && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyStats>().Attacked(100, transform.position, 1f, 0.1f);
        }
    }
}
