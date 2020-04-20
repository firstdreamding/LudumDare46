using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public static MainScript MSCRIPT;

    public enum State
    {
        GAME,
        MENU,
        BUILD
    }
    public State state;
    public List<GameObject> towerActive;
    public GameObject towerTemp;

    private GameObject buyMenu;

    void Awake()
    {
        if (MSCRIPT != null)
            GameObject.Destroy(MSCRIPT);
        else
            MSCRIPT = this;
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        buyMenu = GameObject.Find("UICanvas").transform.Find("Build").gameObject;
        state = State.GAME;
        towerActive = new List<GameObject>();
    }
    private Vector3 offSet = new Vector3(0, 0, -10);
    // Update is called once per frame
    void Update()
    {
        if (state == State.GAME)
        {
            transform.position = PlayerScript.player.transform.position + offSet;
            if (Input.GetKey(KeyCode.B))
            {
                buyMenu.SetActive(true);
                state = State.MENU;
            }
        }
        else if (state == State.BUILD)
        {
            if (Input.GetKey(KeyCode.Escape) && towerTemp != null)
            {
                Destroy(towerTemp);
            }
        }
    }
}
