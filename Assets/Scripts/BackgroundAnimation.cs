using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    public float speed;

    private GameObject mask;
    private GameObject oneScroll;
    private GameObject actual;
    private RectTransform oneScrollRect;
    private RectTransform maskRect;
    private float scrollX;

    private Vector3 oneScrollChange;
    private Vector3 maskChange;

    private 

    // Start is called before the first frame update
    void Awake()
    {
        mask = transform.Find("BackgroundHandler").gameObject;
        oneScroll = transform.Find("OneScroll").gameObject;
        maskRect = mask.GetComponent<RectTransform>();
        oneScrollRect = oneScroll.GetComponent<RectTransform>();
        scrollX = oneScrollRect.localPosition.x;
        actual = gameObject.transform.Find("Actual").gameObject;

        oneScrollChange = oneScrollRect.localPosition;
        maskChange = maskRect.sizeDelta;
    }

    public void ResetPos()
    {
        oneScrollRect.localPosition = new Vector3(115, oneScrollRect.localPosition.y, oneScrollRect.localPosition.z);
        maskRect.sizeDelta = new Vector2(376, maskRect.sizeDelta.y);
        oneScroll.SetActive(true);
        mask.SetActive(true);
        actual.SetActive(false);
        scrollX = oneScrollRect.localPosition.x;

        oneScrollChange = oneScrollRect.localPosition;
        maskChange = maskRect.sizeDelta;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scrollX -= speed;
        oneScrollChange.x = oneScrollRect.localPosition.x - speed;
        oneScrollRect.localPosition = oneScrollChange;
        maskChange.x = maskRect.sizeDelta.x + speed * 2;
        maskRect.sizeDelta = maskChange;
        if (scrollX <= -94)
        {
            oneScroll.SetActive(false);
            mask.SetActive(false);
            actual.SetActive(true);
            enabled = false;
        }
    }
}
