using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTile : MonoBehaviour
{
    public float timeSummon;
    public GameObject prefab;
    public GameObject otherPrefab;
    public Vector2 targetMove;
    public int summoned;

    private float lastSummon;

    // Start is called before the first frame update
    void Start()
    {
        summoned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSummon + lastSummon < Time.time)
        {
            lastSummon = Time.time;
            if (summoned % 2 == 0)
            {
                GameObject temp = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                temp.GetComponent<EnemyStats>().Init(targetMove);
            } else
            {
                GameObject temp = Instantiate(otherPrefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                temp.GetComponent<EnemyStats>().Init(targetMove);
            }
            summoned++;
        }
    }
}
