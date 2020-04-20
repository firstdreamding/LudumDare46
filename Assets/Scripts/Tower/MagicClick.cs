using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicClick : MonoBehaviour
{
    Priest script;

    private void Awake()
    {
        script = transform.parent.GetComponent<Priest>();
    }

    private void OnMouseDown()
    {
        Debug.Log("CLICK");
        script.Click();
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
