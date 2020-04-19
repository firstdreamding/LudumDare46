using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    public int dir;
    public bool dirMatter;
    
    public void UpdateDir()
    {
        GetComponent<Animator>().SetFloat("direction", dir);
        GetComponent<Animator>().SetTrigger("updatedir");
    }
}
