using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    public float timeSummon;
    public GameObject prefab;

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
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
