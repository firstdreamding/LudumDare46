using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    private RectTransform content;
    private Vector2 hpVec;

    // Start is called before the first frame update
    void Start()
    {
        content = transform.Find("Content").GetComponent<RectTransform>();
        hpVec = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        hpVec.x = 100 - MainScript.MSCRIPT.heath;
        content.offsetMin = hpVec;
    }
}
