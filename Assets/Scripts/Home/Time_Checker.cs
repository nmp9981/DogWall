using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Time_Checker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField]
    UI_Manager ui;
    bool time_check = false;
    void Start()
    {
        ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
    }

    void Update()
    {
        if(time_check)
            ui.time += Time.deltaTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        time_check = true;
    }

    public void OnPointerUp(PointerEventData evnetData)
    {
        time_check = false;
    }
}
