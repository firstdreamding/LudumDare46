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
    public Scythe scythePrefab;
    public int gold = 0;

    Animator anim;
    SpriteRenderer spr;
    HUD hud;
    private Item item;
    private float nextActive = 0;
    int direction = 2;
    private float velocityX = 0;
    private float velocityY = 0;
    public Sprite[] idles;
    void Start()
    {
        item = Item.MAGIC;
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();
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
                    Scythe clone = Instantiate(scythePrefab);
                    clone.transform.parent = gameObject.transform;
                    clone.doTransform(direction);
                    break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            velocityY = 0;
            velocityX = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ruin" && Input.GetKey(KeyCode.E))
        {
            string drop = collision.GetComponent<Ruins>().Use();
            if (drop == "GOLD")
            {
                gold += 5;
                hud.setGold(gold);
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(velocityX, velocityY);
        transform.Translate(speed * movement);
        if (velocityX == 0 && velocityY == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            if (velocityY < velocityX)
            {
                if (velocityY > -velocityX) { direction = 1; } else { direction = 2; }
            }
            else
            {
                if (velocityY > -velocityX) { direction = 0; } else { direction = 3; }
            }
            anim.SetFloat("velocityX", velocityX);
            anim.SetFloat("velocityY", velocityY);
            anim.SetBool("isWalking", true);
        }
        velocityX = Input.GetAxis("Horizontal");
        velocityY = Input.GetAxis("Vertical");
    }
}
