using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.25f);
    }
    public void doTransform(int dir)
    {
        
        Vector3 offset = new Vector3(dir == 1 ? 1 : dir == 3 ? -1 : 0, dir == 0 ? 1 : dir == 2 ? -1 : 0, -1);
        transform.localPosition = 0.5f * offset;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90 * (dir + 1)));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
