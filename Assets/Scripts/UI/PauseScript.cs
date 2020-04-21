using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StopTime()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        MainScript.MSCRIPT.nextPause = Time.time + 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
