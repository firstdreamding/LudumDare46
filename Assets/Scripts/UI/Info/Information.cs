﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Information : MonoBehaviour
{

    public GameObject currentGameObject;
    public TowerStats ts;

    private RectTransform damageTrans;
    private RectTransform fireTrans;

    // Start is called before the first frame update
    void Awake()
    {
        damageTrans = transform.Find("Damage").Find("Bar").Find("Content").GetComponent<RectTransform>();
        fireTrans = transform.Find("Fire Rate").Find("Bar").Find("Content").GetComponent<RectTransform>();
    }

    public void SetInfo(float damage, float firerate, TowerStats ts, GameObject go)
    {
        Vector2 dvector = damageTrans.sizeDelta;
        dvector.x = 60 * (damage / 6);
        damageTrans.sizeDelta = dvector;

        Vector2 fvector = fireTrans.sizeDelta;
        fvector.x = 60 * (0.3f / firerate);
        fireTrans.sizeDelta = fvector;

        if (!ts.dirMatter)
        {
            transform.Find("Dropdown").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Dropdown").gameObject.SetActive(true);
            this.ts = ts;
            transform.Find("Dropdown").GetComponent<Dropdown>().value = ts.dir;
        }

        transform.Find("Name").GetComponent<Text>().text = ts.hireName;
        transform.Find("Icon").Find("Content").GetComponent<Image>().sprite = ts.icon;

        this.ts = ts;

        transform.Find("Upgrade").GetComponent<UpgradeScript>().TsSetup(ts);

        currentGameObject = go;
        UpgradeSetup();
    }

    private void UpgradeSetup()
    {
        if (ts.canUpgrade)
        {
            transform.Find("Upgrade").gameObject.SetActive(true);
        } else
        {
            transform.Find("Upgrade").gameObject.SetActive(false);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Fire()
    {
        Destroy(currentGameObject);
        Close();
    }
}
