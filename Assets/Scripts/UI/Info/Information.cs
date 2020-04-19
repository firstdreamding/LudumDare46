﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Information : MonoBehaviour
{

    private RectTransform damageTrans;
    private RectTransform fireTrans;
    public TowerStats ts;

    // Start is called before the first frame update
    void Awake()
    {
        damageTrans = transform.Find("Damage").Find("Bar").Find("Content").GetComponent<RectTransform>();
        fireTrans = transform.Find("Fire Rate").Find("Bar").Find("Content").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInfo(float damage, float firerate, TowerStats ts)
    {
        Vector2 dvector = damageTrans.sizeDelta;
        dvector.x = 200 * (damage / 6);
        damageTrans.sizeDelta = dvector;

        Vector2 fvector = fireTrans.sizeDelta;
        fvector.x = 200 * (0.3f / firerate);
        fireTrans.sizeDelta = fvector;

        this.ts = ts;
        transform.Find("Dropdown").GetComponent<Dropdown>().value = ts.dir;

        if (!ts.dirMatter)
        {
            transform.Find("Dropdown").gameObject.SetActive(false);
        } else
        {
            transform.Find("Dropdown").gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}