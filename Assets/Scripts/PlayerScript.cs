using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Item
{
    BOW,
    MAGIC
}
public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float cooldown;
    public Magic magicPrefab;

    private Item item;
    private float nextActive = 0;

    void Start()
    {
        item = Item.MAGIC;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Time.time > nextActive)
        {
            nextActive = Time.time + cooldown;
            switch (item)
            {
                case Item.MAGIC:
                    Magic clone = Instantiate(magicPrefab);
                    clone.transform.parent = gameObject.transform;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement);

    }
}
