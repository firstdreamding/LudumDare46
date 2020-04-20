using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
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
        float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public float GetDamage()
    {
        return damage;
    }
}
