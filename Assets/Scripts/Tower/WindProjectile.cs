using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProjectile : MonoBehaviour
{
    public float speed;
    public float timeLast;
    private Vector3 normalizedDirection;
    private float step;
    private float damage;
    private HashSet<int> enemiesHit;
    void Start()
    {
        step = speed;
        enemiesHit = new HashSet<int>();
        Destroy(gameObject, timeLast);
    }

    void FixedUpdate()
    {
        transform.position += normalizedDirection * speed;
    }

    public void SetValues(Vector3 dir, float damage)
    {
        normalizedDirection = dir.normalized;
        this.damage = damage;
    }

    public float GetDamage()
    {
        return damage;
    }

    internal bool canHit(int v)
    {
        if (enemiesHit.Contains(v)) return false;
        enemiesHit.Add(v);
        return true;
    }
}
