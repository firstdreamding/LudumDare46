﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private enum finish
    {
        moving,
        wait,
        down,
        done
    }

    public GameObject prefab;
    public int delay;

    private bool isHovering;
    private bool finishing;
    private Vector3 target;
    private Vector3 og;
    private Vector3 first;

    private InfoHandler infoInstance;
    private GameObject infoGO;
    private Vector3 seal;
    private finish state;
    private float wait;
    private Vector3 down;
    public float speed1;
    public float speed2;
    public float speed3;


    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(transform.position);

        target = transform.position;
        target.y = transform.position.y + Screen.height/13;
        og = transform.position;

        first = transform.position;
        first.y = transform.position.y - 150 - delay;

        transform.position = first;

        infoInstance = transform.parent.Find("Info").GetComponent<InfoHandler>();
        infoGO = transform.parent.Find("Info").gameObject;

        GameObject temp = transform.parent.Find("Seal").gameObject;
        seal = new Vector3(temp.transform.position.x, temp.transform.position.y, transform.position.z);

        speed1 = Vector3.Distance(target, og) / speed1;
        speed2 = Vector3.Distance(target, og) / speed2;
        speed3 = Vector3.Distance(og, seal) / speed3;
    }

    void OnEnable()
    {
        finishing = false;
        isHovering = false;
        transform.parent.Find("Seal").gameObject.SetActive(false);
        state = finish.moving;
        transform.position = first;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (finishing)
        {
            switch (state)
            {
                case (finish.moving):
                    transform.position = Vector3.MoveTowards(transform.position, seal, speed3);
                    if (transform.position == seal)
                    {
                        state = finish.wait;
                        wait = Time.time;
                    }
                    break;
                case (finish.wait):
                    if (wait + 0.25f < Time.time)
                    {
                        transform.parent.Find("Seal").gameObject.SetActive(true);
                        state = finish.down;
                        down = transform.position;
                        wait = Time.time;
                    }
                    break;
                case (finish.down):
                    down.x += 25;
                    transform.position = down;
                    if (wait + 0.3f < Time.time)
                    {
                        MainScript.MSCRIPT.towerTemp = Instantiate(prefab);
                        GetComponentInParent<BuildManager>().Exit();
                        state = finish.done;
                    }
                    break;
            }
        } else
        {
            if (isHovering)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed1);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, og, speed2);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        infoGO.SetActive(false);
        foreach (Transform child in transform.parent)
        {
            if (child.tag == "Stamp" && child.gameObject != gameObject)
            {
                child.gameObject.SetActive(false);
            }
        }
        finishing = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (!finishing)
        {
            infoGO.SetActive(true);
            infoInstance.UpdateTs(prefab.GetComponent<TowerStats>());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoGO.SetActive(false);
        isHovering = false;
        if (!finishing)
        {
            infoGO.SetActive(false);
        }
    }
}
