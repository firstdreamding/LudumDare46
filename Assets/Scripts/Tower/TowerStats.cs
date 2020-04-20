using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerStats : MonoBehaviour
{
    public int dir;
    public bool dirMatter;
    public bool canUpgrade;
    public GameObject upgradeTower;
    public float coolDown;
    public float damage;

    public string hireName;
    public int wage;
    public int tolerance;
    private int roundsNotPayed;
    private int moneyOwed;
    public Sprite icon;
    public string blurb;

    void Start()
    {
        moneyOwed = 0;
        roundsNotPayed = 0;
    }
    public void Pay()
    {
        moneyOwed += wage;
        if (moneyOwed > PlayerScript.player.gold)
        {
            roundsNotPayed++;
            transform.Find("NeedMoney").gameObject.SetActive(true);
            if (roundsNotPayed > tolerance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Find("NeedMoney").gameObject.SetActive(false);
            roundsNotPayed = 0;
            moneyOwed = 0;
            PlayerScript.player.incGold(-moneyOwed);
        }

    }
    public void UpdateDir()
    {
        GetComponent<Animator>().SetFloat("direction", dir);
        GetComponent<Animator>().SetTrigger("updatedir");
    }

    public bool CheckIfUIHovered()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0;
    }
}
