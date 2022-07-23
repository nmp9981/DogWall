using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    GameObject option_panel;
    private bool option_toggle = false;
    void Start()
    {
        Get_EveryThing();
    }
    public void UI_Option()
    {
        option_toggle = !option_toggle;
        option_panel.SetActive(option_toggle);
    }
    private void Get_EveryThing()
    {
        option_panel = Find("Battle_Phase","Option_Panel");
    }

    GameObject Find(string parent, string child)//부모오브젝트를 이용하여 자식오브젝트 찾기용 - 이름 버전
    {
        GameObject target;
        for(int i = 0; i < GameObject.Find(parent).transform.childCount; i++)
        {
            target = GameObject.Find(parent).transform.GetChild(i).gameObject;
            if(target.name == child)
            {
                return target;
            }
        }
        Debug.Log("발견 할 수 없음");
        return null;
    }

    GameObject Find(string name)//그냥 바로 이름으로 오브젝트 찾기용
    {
        return GameObject.Find(name).gameObject;
    }
}
