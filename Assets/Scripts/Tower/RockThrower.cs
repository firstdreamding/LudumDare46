using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour
{
    enum State
    {
        SELECT,
        BUILDING,
        BUILT
    }

    public GameObject prefab;
    public Color canBuild;
    public Color cannotBuild;
    public int maxAmmo;
    public int inCollision;
    public float delay;
    public bool isUpgrade;

    List<GameObject> inRange;
    private State state;
    private float lastShoot;
    private float startShoot;
    private GameObject information;
    private int currentAmmo;
    private GameObject needAmmo;
    private GameObject highlight;
    private Animator anim;
    private bool shooting;
    private TowerStats ts;

    private Vector3 mousePosition;

    void Start()
    {
        inRange = new List<GameObject>();
        lastShoot = Time.time;

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

        anim = transform.Find("Content").GetComponent<Animator>();
        shooting = false;
        ts = GetComponent<TowerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.BUILT)
        {
            if (!shooting)
            {
                if (lastShoot + ts.coolDown < Time.time)
                {
                    if (currentAmmo > 0)
                    {
                        if (inRange.Count > 0)
                        {
                            if (inRange[0].GetComponent<EnemyStats>().hp > 0)
                            {
                                lastShoot = Time.time;
                                startShoot = Time.time;
                                anim.SetTrigger("IsThrowing");
                                anim.SetFloat("DirX", inRange[0].transform.position.x - transform.position.x);
                                anim.SetFloat("DirY", inRange[0].transform.position.y - transform.position.y);
                                currentAmmo--;
                                shooting = true;
                            }
                        }
                    }
                    else
                    {
                        needAmmo.SetActive(true);
                    }
                }
            } else
            {
                if (startShoot + delay < Time.time && inRange.Count > 0)
                {
                    GameObject temp = Instantiate(prefab, transform.position, Quaternion.identity);
                    temp.GetComponent<Projectile>().SetValues(inRange[0], ts.damage);
                    shooting = false;
                }
            }
        } else if (state == State.SELECT)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition;
        }

        if (MainScript.MSCRIPT.state == MainScript.State.BUILD)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click();
            }
        }
        //Debug.Log(ts.dir);
    }

    public void SetDown()
    {
        if (inCollision == 0)
        {
            MainScript.MSCRIPT.towerActive.Add(gameObject);

            highlight.SetActive(false);
            state = State.BUILT;
            MainScript.MSCRIPT.state = MainScript.State.GAME;
        } else
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
            if (!ts.CheckIfUIHovered())
            {
                information.SetActive(true);
                information.GetComponent<Information>().SetInfo(ts.damage, ts.coolDown, ts, gameObject);

                Reload();
            }
        } else
        {
            SetDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            inRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
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
