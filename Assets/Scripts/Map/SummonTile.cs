﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTile : MonoBehaviour
{
    public Vector2 targetMove;
    public int summoned;
    WaveManager wm;
    private float lastSummon;

    // Start is called before the first frame update
    void Start()
    {
        summoned = 0;
        lastSummon = 0;
        wm = WaveManager.wm;
    }
    //Are enemies left so spawn, retunr idx or -1
    int canSpawn()
    {
        for (int i = 0; i < wm.enemiesToSpawn.Length; ++i)
        {
            if (wm.enemiesToSpawn[i] > 0)
            {
                wm.enemiesToSpawn[i]--;
                return i;
            }
        }
        return -1;
    }
    public float summonDelay = 0.5f;
    // Update is called once per frame
    void Update()
    {
        if (wm.inWave && summonDelay + lastSummon < Time.time)
        {
            lastSummon = Time.time;
            int toSpawn = canSpawn();
            if (toSpawn == -1)
            {
                wm.nextWave();
            }
            else
            {
                EnemyStats spawned = Instantiate(wm.enemies[toSpawn], new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity).GetComponent<EnemyStats>();
                spawned.Init(targetMove);
                float nextWidth = 0.8f * gameObject.GetComponent<SpriteRenderer>().size.x;
                spawned.transform.Translate(nextWidth * (Random.value - 0.5f), nextWidth * (Random.value - 0.5f), -1);
            }
        }
    }
}
