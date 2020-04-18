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
    public float coolDown;
    public float damage;
    public int maxAmmo;

    List<GameObject> inRange;
    private State state;
    private float lastShoot;
    private GameObject information;
    private int currentAmmo;
    private GameObject needAmmo;
    private GameObject highlight;

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

        state = State.SELECT;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.BUILT)
        {
            if (lastShoot + coolDown < Time.time)
            {
                if (currentAmmo > 0)
                {
                    foreach (GameObject go in inRange)
                    {

                        lastShoot = Time.time;
                        GameObject temp = Instantiate(prefab, transform.position, Quaternion.identity);
                        temp.GetComponent<Projectile>().SetValues(go, damage);
                        currentAmmo--;
                    }
                } else
                {
                    needAmmo.SetActive(true);
                }
            }
        } else if (state == State.SELECT)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition;
        }
    }

    public void SetDown()
    {
        MainScript.MSCRIPT.towerActive.Add(gameObject);

        highlight.SetActive(false);
        state = State.BUILT;
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
        } else
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
}
