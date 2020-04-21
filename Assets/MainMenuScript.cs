using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("play");
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {
        GameObject.Find("UICanvas").transform.Find("Instructions").gameObject.SetActive(true);
    }

    public void Credit()
    {
        GameObject.Find("UICanvas").transform.Find("Credit").gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
