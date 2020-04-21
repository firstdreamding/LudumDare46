﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
public class WaveManager : MonoBehaviour
{
    public static WaveManager wm;
    public GameObject[] enemies;
    // Every x rounds spawn y more
    public Vector2Int[] spawnRates;
    [HideInInspector] public bool inWave = false;
    [HideInInspector] public int[] enemiesToSpawn;
    [HideInInspector] public float waveDelay;
    [HideInInspector] public int wave;
    void Awake()
    {
        if (wm != null)
        {
            Destroy(wm);
        }
        wm = this;
    }
    void Start()
    {
        wave = 0;
        waveDelay = 10f;
        enemiesToSpawn = new int[enemies.Length];
        nextWave();
    }
    public void nextWave()
    {
        wave++;
        inWave = false;
        for (int i = 0; i < enemies.Length; ++i)
        {
            enemiesToSpawn[i] = spawnRates[i].y * (wave / spawnRates[i].x);
        }
        enemiesToSpawn[0] += 1;
        Invoke("startWave", waveDelay);
    }
    private void startWave()
    {
        foreach (TowerStats ts in FindObjectsOfType<TowerStats>())
        {
            ts.Pay();
        }
        HUD.hud.setWave(wave);
        inWave = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
