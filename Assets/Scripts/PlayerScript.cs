using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Item
{
    EMPTY,
    SCYTHE,
    GOLD,
    EYE,
    ANKH,
}
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript player;
    public float speed;
    public float cooldown;
    public Scythe scythePrefab;
    public int gold;

    Animator anim;
    SpriteRenderer spr;
    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    private float nextActive = 0;
    int direction = 2;
    private float velocityX = 0;
    private float velocityY = 0;
    private int selected = 0;
    public Sprite[] idles;
    private void Awake()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        HUD.hud.setGold(gold);
        foreach (Item i in System.Enum.GetValues(typeof(Item)))
        {
            inventory[i] = 0;
        }
        inventory[Item.SCYTHE] = 1;
        inventory[Item.EYE] = 3;
        inventory[Item.ANKH] = 3;
        UpdateInv();
    }
    void UpdateInv()
    {
        HUD.hud.displayInv(inventory);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && Time.time > nextActive)
        {
            nextActive = Time.time + cooldown;
            Item selectedItem = HUD.hud.items[selected];
            if (inventory[selectedItem] < 1) { return; }
            switch (selectedItem)
            {
                case Item.SCYTHE:
                    Scythe clone = Instantiate(scythePrefab);
                    clone.transform.parent = gameObject.transform;
                    clone.doTransform(direction);
                    break;
                case Item.EYE:
                    inventory[selectedItem]--;
                    break;
                case Item.ANKH:
                    inventory[selectedItem]--;
                    break;
            }
            UpdateInv();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            selected = 0;
            HUD.hud.setPointer(selected);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            selected = 1;
            HUD.hud.setPointer(selected);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            selected = 2;
            HUD.hud.setPointer(selected);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            selected = 3;
            HUD.hud.setPointer(selected);
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
            Item drop = collision.GetComponent<Ruins>().Use();
            if (drop == Item.GOLD)
            {
                incGold(30);
            }
            else if (drop == Item.EYE)
            {
                inventory[drop]++;
            }
            else if (drop == Item.ANKH)
            {
                inventory[drop]++;
            }
            UpdateInv();
        }
    }
    public void incGold(int count)
    {
        gold += count;
        HUD.hud.setGold(gold);
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
