using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{
    Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropdownValueChanged(Dropdown change)
    {
        GetComponentInParent<Information>().ts.dir = change.value;
        GetComponentInParent<Information>().ts.UpdateDir();
    }
}
