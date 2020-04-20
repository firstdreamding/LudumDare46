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
    private float spawnRate = 1.00f;
    private WaitForSeconds delay = new WaitForSeconds(2);
    static Item[] drops = { Item.GOLD, Item.EYE, Item.ANKH };
    static float[] probs = { 0.5f, 0.75f, 1.01f };

    private Item selectedDrop = Item.EMPTY;
    void Start()
    {
        state = State.EMPTY;
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = emptyRuin;
        StartCoroutine("Spawner");
    }
    public Item Use()
    {
        Item returnval = selectedDrop;
        if (state == State.FULL)
        {
            spr.sprite = emptyRuin;
            state = State.EMPTY;
            selectedDrop = Item.EMPTY;
        }
        return returnval;
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
