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
    public int maxAmmo;
    public int inCollision;
    public GameObject prefab;
    public bool isUpgrade;

    List<GameObject> inRange;
    private State state;
    private float lastShoot;
    private GameObject information;
    private int currentAmmo;
    private GameObject needAmmo;
    private GameObject highlight;
    private Animator anim;
    private Vector3 mousePosition;
    private TowerStats ts;

    void Awake()
    {
        inRange = new List<GameObject>();
        lastShoot = Time.time;
        anim = GetComponent<Animator>();
        information = GameObject.Find("UICanvas").transform.Find("Information").gameObject;
        currentAmmo = maxAmmo;

        needAmmo = transform.Find("NeedAmmo").gameObject;

        highlight = transform.Find("Highlight").gameObject;
        highlight.GetComponent<SpriteRenderer>().color = canBuild;

        if (isUpgrade)
        {
            state = State.BUILT;
        }
        else
        {
            state = State.SELECT;
        }

        ts = GetComponent<TowerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.BUILT)
        {
            if (lastShoot + ts.coolDown < Time.time)
            {
                if (currentAmmo > 0)
                {
                    if (inRange.Count > 0)
                    {
                        lastShoot = Time.time;
                        anim.SetTrigger("blow");
                        Invoke("spawnProjectile", 0.15f);
                    }
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
            transform.position = mousePosition;
        }
    }
    void spawnProjectile()
    {
        WindProjectile temp = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<WindProjectile>();
        temp.SetValues(Quaternion.Euler(new Vector3(0, 0, -90 * ts.dir)) * new Vector3(0, 1), ts.damage);
        temp.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90 * ts.dir));
        currentAmmo--;
    }
    public void SetDown()
    {
        if (inCollision == 0)
        {
            MainScript.MSCRIPT.towerActive.Add(gameObject);

            highlight.SetActive(false);
            state = State.BUILT;
            MainScript.MSCRIPT.state = MainScript.State.GAME;
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
            if (!ts.CheckIfUIHovered() && MainScript.MSCRIPT.state != MainScript.State.BUILD)
            {
                information.SetActive(true);
                information.GetComponent<Information>().SetInfo(ts.damage, ts.coolDown, ts, gameObject);

                Reload();
            }
        } else if (MainScript.MSCRIPT.state != MainScript.State.MENU)
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
