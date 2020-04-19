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

    public void OnMouseDown()
    {
        if (MainScript.MSCRIPT.state != MainScript.State.MENU)
        {
            Debug.Log("CLICK");
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
