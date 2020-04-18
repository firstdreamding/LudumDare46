using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ruins : MonoBehaviour
{
    enum State
    {
        EMPTY, FULL
    }
    public Sprite emptyRuin;
    public Sprite fullRuin;
    // Start is called before the first frame update
    private State state;
    private SpriteRenderer spr;
    private float spawnRate = 1f;
    private WaitForSeconds delay = new WaitForSeconds(1);
    static string[] drops = { "COIN", "SCRAP", "SCYTHE" };
    static float[] probs = { 0.5f, 0.75f, 1.01f };

    private string selectedDrop;
    void Start()
    {
        state = State.EMPTY;
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = emptyRuin;
        StartCoroutine("Spawner");
    }
    public void Use()
    {
        Debug.Log(selectedDrop);
        spr.sprite = emptyRuin;
        state = State.EMPTY;
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return delay;
            if (state == State.EMPTY && Random.value < spawnRate)
            {
                spr.sprite = fullRuin;
                float randF = Random.value;
                int i = 0;
                while (i < drops.Length && randF > probs[i])
                {
                    ++i;
                }
                selectedDrop = drops[i];
                state = State.FULL;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
