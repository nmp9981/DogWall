using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public int level1 = 0, level2 = 0;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    public void UI_LEVEL1_Controll(int level)
    {
        level1 = level;
        UI_Controll();
    }

    public void UI_LEVEL2_Controll(int level)
    {
        level2 = level;
        UI_Controll();
    }

    void UI_Controll()
    {
        switch(level1)
        {
            case 0:
                UI("Home");
                break;
            case 1:
                switch(level2)
                {
                    case 0:
                        UI("Team");
                        break;
                    case 1:
                        UI("Team","Teams");
                        break;
                }
                break;
        }
    }

    void UI(string name)//level1 변경용, 켜고싶은 UI이름
    {
        GameObject root = GameObject.Find("Canvas").gameObject;
        for(int i = 1; i < root.transform.childCount;i++)
        {
            GameObject child = root.transform.GetChild(i).gameObject;
            if(child.name == name)
            {
                child.SetActive(true);
                for(int j = 0; j < child.transform.childCount; j++)
                {
                    GameObject grandchild = child.transform.GetChild(j).gameObject;
                    if(grandchild.tag == "Panel")
                        grandchild.SetActive(false);
                }
            }
            else
                child.SetActive(false);
        }
        
    }

    void UI(string parent, string name)//level2 변경용, 켜고싶은 UI의 level1의 이름과 leve2의 이름
    {
        GameObject root = GameObject.Find(parent).gameObject;
        for(int i = 1; i < root.transform.childCount;i++)
        {
            GameObject child = root.transform.GetChild(i).gameObject;
            if(child.name == name)
                child.SetActive(true);
            else if(child.tag == "Panel")
                child.SetActive(false);
        }
    }
}
