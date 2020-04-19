using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
public class WaveManager : MonoBehaviour
{
    public static WaveManager wm;
    public GameObject[] enemies;
    public TextAsset waveDataText;
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
        waveDelay = 0.3f;
        wave = 0;
        nextWave();
    }
    public void nextWave()
    {
        inWave = true;
        string[] fLines = Regex.Split(waveDataText.text, "[\r\n]+");
        enemiesToSpawn = fLines[wave].Split(' ').Select(int.Parse).ToArray();
        HUD.hud.setWave(++wave);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
