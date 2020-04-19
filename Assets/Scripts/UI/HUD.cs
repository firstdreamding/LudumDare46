using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Text goldText;
    Text waveText;
    public void setGold(int count)
    {
        goldText.text = "GOLD: " + count;
    }
    public void setWave(int wave)
    {
        waveText.text = "WAVE: " + wave;
    }
    // Start is called before the first frame update
    void Start()
    {
        goldText = transform.Find("Gold").GetComponent<Text>();
        waveText = transform.Find("Wave").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
