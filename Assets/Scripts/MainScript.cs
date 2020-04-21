using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public static MainScript MSCRIPT;
    public float nextPause = 0;
    public enum State
    {
        GAME,
        MENU,
        BUILD,
        GAMEOVER
    }
    public State state;
    public List<GameObject> towerActive;
    public GameObject towerTemp;
    public int heath;

    private GameObject buyMenu;
    private GameObject pauseMenu;

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
        pauseMenu = GameObject.Find("UICanvas").transform.Find("Pause").gameObject;
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
            else if (Input.GetKey(KeyCode.Escape) && Time.time > nextPause)
            {
                pauseMenu.GetComponent<PauseScript>().StopTime();
                pauseMenu.SetActive(true);
            }
            if (heath <= 0)
            {
                GameObject.Find("UICanvas").transform.Find("GameOver").gameObject.SetActive(true);
            }
        }
        else if (state == State.BUILD)
        {
            transform.position = PlayerScript.player.transform.position + offSet;
            if (Input.GetKey(KeyCode.Escape) && towerTemp != null)
            {
                Destroy(towerTemp);
                state = State.GAME;
            }
        }
    }
}
