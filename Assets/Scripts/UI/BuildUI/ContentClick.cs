using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefab;
    public int delay;

    private bool isHovering;
    private bool init;
    private Vector3 target;
    private Vector3 og;
    private Vector3 first;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        target.y = transform.position.y + 50;
        og = transform.position;

        first = transform.position;
        first.y = transform.position.y - 150 - delay;

        transform.position = first;
    }

    void OnDisable()
    {
        isHovering = false;
        transform.position = first;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHovering)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 320f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, og, 450f * Time.deltaTime);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MainScript.MSCRIPT.state = MainScript.State.BUILD;
        Instantiate(prefab);
        transform.parent.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
