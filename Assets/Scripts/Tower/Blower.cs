using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    enum State
    {
        SELECT,
        BUILDING,
        BUILT
    }

    public Color canBuild;
    public Color cannotBuild;
    public float coolDown;
    public float damage;
    public int maxAmmo;
    public int inCollision;

    List<GameObject> inRange;
    private State state;
    private float lastShoot;
    private GameObject information;
    private int currentAmmo;
    private GameObject needAmmo;
    private GameObject highlight;
    private Animator anim;
    private Vector3 mousePosition;

    void Start()
    {
        inRange = new List<GameObject>();
        lastShoot = Time.time;
        anim = GetComponent<Animator>();
        information = GameObject.Find("UICanvas").transform.Find("Information").gameObject;
        currentAmmo = maxAmmo;

        needAmmo = transform.Find("NeedAmmo").gameObject;

        highlight = transform.Find("Highlight").gameObject;
        highlight.GetComponent<SpriteRenderer>().color = canBuild;

        state = State.SELECT;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.BUILT)
        {
            if (lastShoot + coolDown < Time.time)
            {
                if (currentAmmo > 0 && inRange.Count > 0)
                {
                    lastShoot = Time.time;
                    Debug.Log("SHOOT");
                    anim.SetTrigger("blow");
                    currentAmmo--;
                }
                else
                {
                    needAmmo.SetActive(true);
                }
            }
        }
        else if (state == State.SELECT)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            mousePosition.y -= 0.4f;
            transform.position = mousePosition;
        }
    }

    public void SetDown()
    {
        if (inCollision == 0)
        {
            MainScript.MSCRIPT.towerActive.Add(gameObject);

            highlight.SetActive(false);
            state = State.BUILT;
        }
        else
        {
            Debug.Log("CANNOT BUILD");
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        needAmmo.SetActive(false);
    }

    public void Click()
    {
        if (state != State.SELECT)
        {
            information.SetActive(true);
            information.GetComponent<Information>().SetInfo(damage, coolDown);

            Reload();
        }
        else
        {
            SetDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            Debug.Log("yup1");
            inRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("yup1");
            inRange.Remove(collision.gameObject);
        }
    }

    public void CollisionDown()
    {
        if (inCollision > 0)
        {
            highlight.GetComponent<SpriteRenderer>().color = cannotBuild;
        }
    }

    public void CollisionUp()
    {
        if (inCollision == 0)
        {
            highlight.GetComponent<SpriteRenderer>().color = canBuild;
        }
    }
}
