using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, -1);
        Destroy(gameObject, 0.1f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
