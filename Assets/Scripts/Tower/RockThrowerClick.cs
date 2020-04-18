using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowerClick : MonoBehaviour
{
    RockThrower script;

    private void Start()
    {
        script = transform.parent.GetComponent<RockThrower>();
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICK");
        script.Click();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Tower")
        {
            script.inCollision++;
            script.CollisionDown();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tower")
        {
            script.inCollision--;
            script.CollisionUp();
        }
    }
}
