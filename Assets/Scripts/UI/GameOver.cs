using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Color endColor;
    private Color startingColor;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
    }


    private void FixedUpdate()
    {
        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, endColor, t);
        if (t < 1)
        {
            t += Time.deltaTime/0.5f;
        }
    }

    private void OnEnable()
    {
        MainScript.MSCRIPT.state = MainScript.State.GAMEOVER;
        endColor = GetComponent<Image>().color;
        startingColor = GetComponent<Image>().color;
        startingColor.a = 0;
        GetComponent<Image>().color = startingColor;

        GetComponent<Animator>().SetTrigger("GameOver");
        t = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
