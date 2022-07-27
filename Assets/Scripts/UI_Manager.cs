using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public int level1 = 0, level2 = 0;
    public int x = 0, y = 0;
    Data_Manager data;
    void Awake()
    {
        data = GameObject.Find("Data_Manager").GetComponent<Data_Manager>();
    }
    void Start()
    {
        Character_ADD("Monster Dummy/000","질퍽이",10,10,"몰루?");
        Character_ADD("Monster Dummy/111","꼬북이",10,10,"물");
        Character_ADD("Monster Dummy/121","잉어킹",10,10,"물");
        Character_ADD("Monster Dummy/212","고라파덕",10,10,"물");
        Character_ADD("Monster Dummy/333","뭐더라",10,10,"어둠");
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
                    case 2:
                        UI("Team","Unit_Assign");
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

    public void Character_ADD(string path, string name, int hp, int atk, string type)
    {
        //string Path = Monster Dummy/ + path =>이런식으로 수정해서 쓸거임
        data.Add_Character(path,name,hp,atk,type);
        int i = data.Get_Character_Count();
        x = (i-1) % 4;
        y = (i - x) / 4;
        GameObject temp = Instantiate(Resources.Load<GameObject>("Prefabs/Ch"));
        Transform parent = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Unit_Assign").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject.GetComponent<Transform>();
        temp.transform.SetParent(parent);
        Vector3 pos = new Vector3(43 + (x * 259f), 395 - (y * 253));
        temp.transform.position = pos;
        temp.name = data.list[i-1].Name;
        temp.GetComponent<Image>().sprite = data.list[i-1].Img;//보여주기식용, 나중에 리스트 먼저 나오면 수정해야함
    }
}
