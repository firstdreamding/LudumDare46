using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        MainScript.MSCRIPT.state = MainScript.State.GAME;
        gameObject.SetActive(false);
    }
}
