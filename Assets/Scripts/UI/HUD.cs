using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD hud;
    public Item[] items;
    public Texture[] itemSprites;
    private Dictionary<Item, Texture> itemSpriteMap = new Dictionary<Item, Texture>();
    void Awake()
    {
        if (hud != null)
        {
            Destroy(hud);
        }
        hud = this;
        goldText = transform.Find("Gold").GetComponent<Text>();
        waveText = transform.Find("Wave").GetComponent<Text>();
        for (int i = 0; i < 4; i++)
        {
            inventorySlots[i] = transform.Find("Inventory/INV" + i).gameObject.GetComponent<RawImage>();
            inventoryCounts[i] = transform.Find("Inventory/COUNT" + i).gameObject.GetComponent<Text>();
        }
        for (int i = 0; i < items.Length; i++)
        {
            itemSpriteMap[items[i]] = itemSprites[i];
        }
        pointer = (RectTransform)transform.Find("Inventory/POINTER");
    }
    Text goldText;
    Text waveText;
    RawImage[] inventorySlots = new RawImage[4];
    Text[] inventoryCounts = new Text[4];
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

    }

    // Update is called once per frame
    void Update()
    {

    }
    RectTransform pointer;
    public void setPointer(int i)
    {
        pointer.anchoredPosition = new Vector3(12 + 36 * i, -1, 0);
    }
    public void displayInv(Dictionary<Item, int> inventory)
    {
        for (int i = 0; i < 4; ++i)
        {
            inventorySlots[i].texture = itemSpriteMap[items[i]];
            inventoryCounts[i].text = "" + inventory[items[i]];
        }
    }
}
