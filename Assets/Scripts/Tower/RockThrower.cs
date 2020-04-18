using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrower : MonoBehaviour
{
    public GameObject prefab;
    public float coolDown;
    public float damage;
    public int maxAmmo;

    List<GameObject> inRange;
    private float lastShoot;
    private GameObject information;
    private int currentAmmo;
    private GameObject needAmmo;

    // Start is called before the first frame update
    void Start()
    {
        inRange = new List<GameObject>();
        lastShoot = Time.time;

        information = GameObject.Find("UICanvas").transform.Find("Information").gameObject;
        currentAmmo = maxAmmo;

        needAmmo = transform.Find("NeedAmmo").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown + lastShoot < Time.time)
        {
            if (currentAmmo > 0)
            {
                foreach (GameObject go in inRange)
                {
                    GameObject temp = Instantiate(prefab, transform.position, Quaternion.identity);
                    temp.GetComponent<Projectile>().SetValues(go, damage);
                    currentAmmo--;
                }
                lastShoot = Time.time;
            } else
            {
                needAmmo.SetActive(true);
            }
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        needAmmo.SetActive(false);
    }

    void OnMouseDown()
    {
        information.SetActive(true);
        information.GetComponent<Information>().SetInfo(damage, coolDown);

        Reload();
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
