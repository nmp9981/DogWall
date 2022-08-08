using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
    public int level1 = 0, level2 = 0;
    public int x = 0, y = 0;
    int tap = 0;
    DataManager data;
    private bool upgrade = false;
    private bool fullStat = false;
    [SerializeField] int leftPiecesNum = 10; // 진짜 데이터는 받아서 써야함 구색만 맞춰 놓은 거임
    int basicSettingNum = 0;
    int presentUseNum = 0;
    int presentStatNum = 0;
    int futureStatNum = 0;

    public Text changeStat;
    public Text leftPiece;
    public Text usePiece;
    public float time = 0;
    void Awake()
    {
        data = GameObject.Find("Data_Manager").GetComponent<DataManager>();
    }
    void Start()
    {
        data.saveData.SetImg();
        Debug.Log("Load실행");
        Load();
        changeStat.text = "스탯 변화 : +" + presentStatNum.ToString() + "% -> +" + futureStatNum.ToString() + "%";
        leftPiece.text = "남은 기억의 조각 : " + leftPiecesNum.ToString();
        usePiece.text = "사용할 기억의 조각 : " + basicSettingNum.ToString();
        
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Character_ADD("Monster Dummy/000","질퍽이",10,5,10,"몰루?");
            Character_ADD("Monster Dummy/111","꼬북이",10,5,10,"물");
            Character_ADD("Monster Dummy/121","잉어킹",10,5,10,"물");
            Character_ADD("Monster Dummy/212","고라파덕",5,10,10,"물");
            Character_ADD("Monster Dummy/333","뭐더라",10,5,10,"어둠");
        }
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
                        UI("Team");//팀
                        break;
                    case 1:
                        UI("Team","Teams");//팀편성버튼
                        upgrade = false;
                        break;
                    case 2:
                        UI("Team","Unit_Select_Tap");//선택 버튼
                        Unit_Select_Change();
                        break;
                    case 3:
                        UI("Team","Unit_Select_Tap");//유닛 강화버튼
                        upgrade = true;
                        Unit_Select_Change();
                        break;
                    
                }
                break;
            case 2:
                UI("Gacha");
                break;
            case 3:
                UI("Quest");
                break;
        }
    }

    public void Tap(int a)
    {
        tap = a;
        for(int i = 0; i < 4; i++)
        { 
            GameObject target = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Teams").transform.Find("Character" + (i+1).ToString()).gameObject;
            if(data.saveData.my_team[4*tap + i].Img is null)
                target.transform.GetChild(0).GetComponent<Image>().sprite = null;
            else
                target.transform.GetChild(0).GetComponent<Image>().sprite = data.saveData.my_team[4*tap + i].Img;
                target.transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].HP.ToString();
                target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].ATK.ToString();
                target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].Type;
        }
        data.Save();
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

    public void Character_ADD(string path, string name, int hp,int energy, int atk, string type)
    {
        //string Path = Monster Dummy/ + path =>이런식으로 수정해서 쓸거임
        data.saveData.my_characterList.Add(new Character(path,Resources.Load<Sprite>(path),name,hp,energy,atk,type));
        Load();
    }

    void Load()
    {
        int max = data.saveData.my_characterList.Count;
        int i = 0;
        while(i < max)
        {
            x = i % 4;
            y = (i - x) / 4;
            
            Transform parent = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Unit_Select_Tap").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject.GetComponent<Transform>();
            if(y>=4)
                parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,1000 + (y-3)*300);
            GameObject temp = Instantiate(Resources.Load<GameObject>("Prefabs/Ch"));
            temp.transform.SetParent(parent);
            Vector3 pos = new Vector3(43 + (x * 259f), 1395 - (y * 253));
            temp.transform.position = pos;
            temp.name = data.saveData.my_characterList[i].Name;
            temp.GetComponent<Image>().sprite = data.saveData.my_characterList[i].Img;
            temp.transform.GetComponent<Button>().onClick.AddListener(Unit_Choose);
            i++;
        }
        for(i = 0; i < 4; i++)
        { 
            GameObject target = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Teams").transform.Find("Character" + (i+1).ToString()).gameObject;

            target.transform.GetChild(0).GetComponent<Image>().sprite = data.saveData.my_team[4*tap + i].Img;
            target.transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].HP.ToString();
            target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].ATK.ToString();
            target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].Type;
        }
    }

    void Unit_Select_Change()
    {
        GameObject panel = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Unit_Select_Tap").gameObject;
        if(upgrade == true)
        {
            panel.transform.Find("Text").GetComponent<Text>().text = "강화할 유닛 선택"; 
        }
        else
        {
            GameObject character = EventSystem.current.currentSelectedGameObject;
            string idx = character.name.Substring(character.name.Length-1);
            switch(idx)
            {
                case "1":
                    idx = "첫 ";
                    break;
                case "2":
                    idx = "두 ";
                    break;
                case "3":
                    idx = "세 ";
                    break;
                case "4":
                    idx = "네 ";
                    break;
            }
            panel.transform.Find("Text").GetComponent<Text>().text = idx + "번째 유닛 선택";
        }
    }

    void Unit_Choose()
    {
        GameObject Unit = EventSystem.current.currentSelectedGameObject;

        if(time < 1f)
        {
            if(!upgrade)
            {   
                GameObject panel = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Unit_Select_Tap").gameObject;
                string idx = panel.transform.Find("Text").gameObject.GetComponent<Text>().text.Substring(0,1);
                switch(idx)
                {
                    case "첫":
                        idx = "1";
                        break;
                    case "두":
                        idx = "2";
                        break;
                    case "세":
                        idx = "3";
                        break;
                    case "네":
                        idx = "4";
                        break;
                }
                GameObject target = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Teams").transform.Find("Character" + idx).gameObject;
                
                for(int i = 0; i < data.saveData.my_characterList.Count; i++)
                {
                    if(Unit.name == data.saveData.my_characterList[i].Name)
                    {
                        target.transform.GetChild(0).GetComponent<Image>().sprite = data.saveData.my_characterList[i].Img;
                        target.transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_characterList[i].HP.ToString();
                        target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_characterList[i].ATK.ToString();
                        target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_characterList[i].Type;
                        data.saveData.my_team[4*tap+int.Parse(idx)-1] = data.saveData.my_characterList[i];
                        UI_LEVEL2_Controll(1);
                        break;
                    }
                }
            }
            else
            {
                UI("Team","Unit_Upgrade_Tap");
                GameObject panel = GameObject.Find("Canvas").transform.Find("Team").transform.Find("Unit_Upgrade_Tap").gameObject;
                GameObject target = panel.transform.Find("Image").gameObject;
                
                for(int i = 0; i < data.saveData.my_characterList.Count; i++)
                {
                    if(Unit.name == data.saveData.my_characterList[i].Name)
                    {
                        target.GetComponent<Image>().sprite = data.saveData.my_characterList[i].Img;
                        break;
                    }
                }
            }
        }
        else
        {
            UI("Team","Unit_Details");
            GameObject target = GameObject.Find("Unit_Details");
            for(int i = 0; i < data.saveData.my_characterList.Count; i++)
            {
                if(Unit.name == data.saveData.my_characterList[i].Name)
                {
                    Color col;
                    string txt;
                    switch(data.saveData.my_characterList[i].Attribute)
                    {
                        case 0:
                            col = new Color(1,0,0);
                            txt = "불";
                            break;
                        case 1:
                            col = new Color(0,0,1);
                            txt = "물";
                            break;
                        case 2:
                            col = new Color(0,1,0);
                            txt = "독";
                            break;
                        case 3:
                            col = new Color(1,1,0);
                            txt = "기타";
                            break;
                        case 4:
                            col = new Color(0.5f,0.5f,0.5f);
                            txt = "기타";
                            break;
                        default :
                            col = new Color(1,1,1); 
                            txt = "";
                            break;
                    }
                    target.transform.GetChild(0).GetComponent<Image>().color = col;
                    target.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = txt;
                    target.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_characterList[i].Name;
                    target.transform.GetChild(1).GetComponent<Image>().sprite = data.saveData.my_characterList[i].Img;
                    target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_characterList[i].HP.ToString();
                    target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_characterList[i].ATK.ToString();
                    float extra_hp = data.saveData.my_characterList[i].HP * 0.2f, extra_atk = data.saveData.my_characterList[i].ATK * 0.2f;
                    target.transform.GetChild(4).GetComponent<Text>().text = ((int)(extra_hp*data.saveData.my_characterList[i].Stone)).ToString();
                    target.transform.GetChild(5).GetComponent<Text>().text = ((int)(extra_atk*data.saveData.my_characterList[i].Stone)).ToString();
                    target.transform.GetChild(6).GetComponent<Text>().text = data.saveData.my_characterList[i].Type;
                    break;
                }
            }
        }
        
    }


    public void Back_for_Select()
    {
        if(!upgrade)
        {
            UI_LEVEL1_Controll(1);
            UI_LEVEL2_Controll(1);
        }
        else
        {
            UI_LEVEL1_Controll(1);
            UI_LEVEL2_Controll(0);
        }
    }

    public void IncreasePieceNum()
    {
        if(!fullStat)
        {
            presentUseNum++;
            leftPiecesNum--;
            futureStatNum += 10;
            changeStat.text = "스탯 변화 : +" + presentStatNum.ToString() + "% -> +" + futureStatNum.ToString() + "%";
            leftPiece.text = "남은 기억의 조각 : " + leftPiecesNum.ToString();
            usePiece.text = "사용할 기억의 조각 : " + presentUseNum.ToString();
            if(futureStatNum == 50)
            {
                Debug.Log("뭐가 좀 이상한디,,,,");
            }
        }
    }

    public void DecreasePieceNum()
    {
        if(presentUseNum != 0)
        {
            presentUseNum--;
            leftPiecesNum++;
            futureStatNum -= 10;
            changeStat.text = "스탯 변화 : +" + presentStatNum.ToString() + "% -> +" + futureStatNum.ToString() + "%";
            leftPiece.text = "남은 기억의 조각 : " + leftPiecesNum.ToString();
            usePiece.text = "사용할 기억의 조각 : " + presentUseNum.ToString();
        }
    }

    public void ApplyBtn()
    {
        presentStatNum = futureStatNum;
        changeStat.text = "스탯 변화 : +" + presentStatNum.ToString() + "% -> +" + futureStatNum.ToString() + "%";
        if(presentStatNum == 50)
        {
            fullStat = true;
        }
    }

    public void ClosePopup()
    {
        GameObject panel = GameObject.Find("Canvas").transform.Find("Gacha").transform.Find("Percentage_Popup").gameObject;
        if(panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
    }

}
