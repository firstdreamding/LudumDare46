using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeScript : MonoBehaviour, IPointerClickHandler
{

    Information instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponentInParent<Information>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject test = Instantiate(instance.ts.upgradeTower, instance.currentGameObject.transform.position, Quaternion.identity);
        TowerStats temp = test.GetComponent<TowerStats>();
        Destroy(instance.currentGameObject);
        instance.SetInfo(temp.damage, temp.coolDown, temp, test);
    }
}
