using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject magic;
    public float timeLast;
    private Vector3 normalizedDirection;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeLast);
    }

    public void SetValues(float damage)
    {
        this.damage = damage;
    }

    public float GetDamage()
    {
        return damage;
    }
}
