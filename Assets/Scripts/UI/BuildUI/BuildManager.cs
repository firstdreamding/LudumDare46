using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public float waitTime;

    private bool waiting;
    private MainScript.State setState;
    private float lastTime;
    private Vector3 old;
    private Vector3 move;
    private float oldTime;
    private bool finish;

    // Start is called before the first frame update
    void Start()
    {
        old = transform.position;
    }

    private void FixedUpdate()
    {
        if (finish)
        {
            move.y -= 25;
            transform.position = move;
            if (oldTime + 0.5f < Time.time)
            {
                MainScript.MSCRIPT.state = setState;
                gameObject.SetActive(false);
            }
        }
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

    public void Exit()
    {
        setState = MainScript.State.BUILD;
        finish = true;
        oldTime = Time.time;
        move = transform.position;
    }

    private void OnEnable()
    {
        waiting = true;
        lastTime = Time.time;
    }

    private void OnDisable()
    {
        transform.Find("Background").GetComponent<BackgroundAnimation>().ResetPos();
        transform.Find("Info").gameObject.SetActive(false);
        finish = false;
        transform.position = old;
    }

    public void Back()
    {
        setState = MainScript.State.GAME;
        finish = true;
        oldTime = Time.time;
        move = transform.position;
    }
}
