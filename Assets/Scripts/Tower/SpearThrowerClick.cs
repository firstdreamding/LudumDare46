﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrowerClick : MonoBehaviour
{
    SpearThrower script;
    Animator anim;

    private void Awake()
    {
        script = transform.parent.GetComponent<SpearThrower>();
        anim = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        if (MainScript.MSCRIPT.state != MainScript.State.MENU)
        {
            script.Click();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Tower" || collision.tag == "Path" || collision.tag == "PathTurn")
        {
            script.inCollision++;
            script.CollisionDown();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tower" || collision.tag == "Path" || collision.tag == "PathTurn")
        {
            script.inCollision--;
            script.CollisionUp();
        }
    }
}