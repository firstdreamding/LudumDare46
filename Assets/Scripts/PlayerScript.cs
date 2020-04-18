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

    Animator anim;
    private Item item;
    private float nextActive = 0;
    int direction = 2;
    void Start()
    {
        item = Item.MAGIC;
        anim = GetComponent<Animator>();
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ruin" && Input.GetKey(KeyCode.E))
        {
            collision.GetComponent<Ruins>().Use();
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float speedX = Mathf.Abs(moveHorizontal);
        float speedY = Mathf.Abs(moveVertical);
        direction = 2;
        if (speedX > speedY)
        {
            if (moveHorizontal > 0) { direction = 1; }
            else if (moveHorizontal < 0) { direction = 3; }
        }
        else
        {
            if (moveVertical > 0) { direction = 0; }
            else if (moveVertical < 0) { direction = 2; }
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(speed * movement);
        anim.SetFloat("speed", speedX + speedY);
        anim.SetInteger("direction", direction);
    }
}
