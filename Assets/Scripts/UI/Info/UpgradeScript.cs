using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeScript : MonoBehaviour, IPointerClickHandler
{

    Information instance;
    bool canPay;
    GameObject cant;
    Text upgradeText;
    Text costText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = GetComponentInParent<Information>();
        cant = transform.Find("Cant").gameObject;
        upgradeText = transform.Find("UpgradeText").GetComponent<Text>();
        costText = transform.Find("Cost").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.player.gold < instance.ts.cost)
        {
            canPay = false;
            cant.SetActive(true);
        } else
        {
            canPay = true;
            cant.SetActive(false);
        } 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canPay)
        {
            GameObject test = Instantiate(instance.ts.upgradeTower, instance.currentGameObject.transform.position, Quaternion.identity);
            TowerStats temp = test.GetComponent<TowerStats>();
            int dir = instance.ts.dir;
            Destroy(instance.currentGameObject);
            if (temp.dirMatter)
            {
                temp.dir = dir;
                temp.UpdateDir();
            }
            instance.SetInfo(temp.damage, temp.coolDown, temp, test);
            PlayerScript.player.incGold(-1 * instance.ts.cost);
        }
    }

    public void TsSetup(TowerStats ts)
    {
        upgradeText.text = ts.upgradeBlurb;
        costText.text = "Cost: " + ts.cost;
    }
}
