using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public float lifetime;
    public int damage;
    public float knockback;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    public void doTransform(int dir)
    {
        Vector3 offset = new Vector3(dir == 1 ? 1 : dir == 3 ? -1 : 0, dir == 0 ? 1 : dir == 2 ? -1 : 0, -1);
        transform.localPosition = 0.15f * offset;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90 * (dir - 1)));
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && canHit(collision.GetComponent<EnemyStats>().GetInstanceID()))
        {
            collision.GetComponent<EnemyStats>().Attacked(damage, transform.position, knockback, 0.5f);
        }
    }
    HashSet<int> enemiesHit = new HashSet<int>();
    internal bool canHit(int v)
    {
        if (enemiesHit.Contains(v)) return false;
        enemiesHit.Add(v);
        return true;
    }
}
