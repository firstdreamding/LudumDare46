using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public float waitTime;

    private bool waiting;
    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting && lastTime + waitTime < Time.time)
        {
            transform.Find("Background").GetComponent<BackgroundAnimation>().enabled = true;
            waiting = false;
        } 
    }

    private void OnEnable()
    {
        waiting = true;
        lastTime = Time.time;
    }

    private void OnDisable()
    {
        transform.Find("Background").GetComponent<BackgroundAnimation>().ResetPos();
    }

    public void Back()
    {
        MainScript.MSCRIPT.state = MainScript.State.GAME;
        gameObject.SetActive(false);
    }
}
