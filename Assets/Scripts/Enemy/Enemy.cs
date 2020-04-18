using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.localPosition;
        temp.x += 0.01f;
        transform.localPosition = temp;
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Projectile")
        {
            Debug.Log("Ouch");
            hp = (int) (hp - collision.GetComponent<Projectile>().GetDamage());
        } else
        {
            Debug.Log("else");
        }
    }
}
