using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTile : MonoBehaviour
{
    public float timeSummon;
    public GameObject prefab;
    public Vector2 targetMove;

    private float lastSummon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSummon + lastSummon < Time.time)
        {
            lastSummon = Time.time;
            Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        }
    }
}
