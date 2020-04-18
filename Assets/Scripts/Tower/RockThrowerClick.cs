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
}
