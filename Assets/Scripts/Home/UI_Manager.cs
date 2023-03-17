using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
    public int level1 = 0, level2 = 0;
    public int x = 0, y = 0;
    int tap = 0;
    public float time = 0;
    DataManager data;
    //SaveDataClass saveData; 일단 폐기 나중에 한번에 고치든가

    // About Unit Upgrade
    private bool upgrade = false;
    private bool fullStat = false;
    int leftPiecesNum = 10; // 진짜 데이터는 받아서 써야함 구색만 맞춰 놓은 거임
    int basicSettingNum = 0;
    int presentUseNum = 0;
    int presentStatNum = 0;
    int futureStatNum = 0;
    int charUpgrade;
    float[] possibility = {10f,20f,70f};//뽑기 확률 변수 저장하는 곳
    public Text changeStat;
    public Text leftPiece;
    public Text usePiece;

    // About Unit Gacha
    int currency;
    int currentDotPos = 0;
    public GameObject bannerImg;
    public List<GameObject> sliderDotsList;
    public List<Sprite> bannerImgList;
    [SerializeField] Sprite fullDot, emptyDot;
    [SerializeField] GameObject leftBtn, rightBtn, mainbanner, gachaResult, alertPop;


    void Awake()
    {
        data = GameObject.Find("Data_Manager").GetComponent<DataManager>();
    }
    void Start()
    {
        //Debug.Log(DataManager.singleTon.monsterCharaterNumber[0][1]); 형원이형 보라고 냅둔거
        //saveData = data.saveData;
        data.saveData.SetImg();
        Load();
        //currency = data.saveData.ui.money[0];
        currency = 10000; // for Test
        
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Character_ADD("Monster Dummy/000","질퍽이",10,5,1,10,"몰루?");
            Character_ADD("Monster Dummy/111","꼬북이",10,5,2,10,"물");
            Character_ADD("Monster Dummy/121","잉어킹",10,5,1,10,"물");
            Character_ADD("Monster Dummy/212","고라파덕",5,10,2,10,"물");
            Character_ADD("Monster Dummy/333","뭐더라",10,5,3,10,"어둠");
            data.Save();
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            data.saveData.list.Clear();
            Debug.Log("list클리어");
            data.Save();
        }

    }
    #region yeongchan
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
                UI("Team");//팀
                break;
            case 2:
                UI("Teams");//팀편성버튼
                upgrade = false;
                break;
            case 3:
                UI("Unit_Select_Tap");//선택 버튼
                Unit_Select_Change();
                break;
            case 4:
                UI("Unit_Select_Tap");//유닛 강화버튼
                upgrade = true;
                Unit_Select_Change();
                break;
            case 5:
                UI("Gacha");
                break;
            case 6:
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

    public void Character_ADD(string path, string name, int hp,int energy,int attribute, int atk, string type)
    {
        //string Path = Monster Dummy/ + path =>이런식으로 수정해서 쓸거임
        data.saveData.list.Add(new Character(path,Resources.Load<Sprite>(path),name,hp,energy,attribute,atk,type));
        Load();
    }

    void Load()
    {
        int max = data.saveData.my_characterlist.Count;
        int i = 0;
        while(i < max)//가지고 있는 캐릭터 리스트 생성
        {
            x = i % 4;
            y = (i - x) / 4;
            
            Transform parent = GameObject.Find("Canvas").transform.Find("Unit_Select_Tap").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject.GetComponent<Transform>();
            if(y>=4)
                parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,1000 + (y-3)*300);
            GameObject temp = Instantiate(Resources.Load<GameObject>("Prefabs/Ch"));
            temp.transform.SetParent(parent);
            Vector3 pos = new Vector3(43 + (x * 259f), 1395 - (y * 253));
            temp.transform.position = pos;
            temp.name = data.saveData.my_characterlist[i].Name;
            temp.GetComponent<Image>().sprite = data.saveData.my_characterlist[i].Img;
            temp.transform.GetComponent<Button>().onClick.AddListener(Unit_Choose);
            i++;
        }
        for(i = 0; i < 4; i++)//내 팀 리스트 생성
        { 
            GameObject target = GameObject.Find("Canvas").transform.Find("Teams").transform.Find("Character" + (i+1).ToString()).gameObject;
            
            target.transform.GetChild(0).GetComponent<Image>().sprite = data.saveData.my_team[4*tap + i].Img;
            target.transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].HP.ToString();
            target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].ATK.ToString();
            target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_team[4*tap + i].Type;
        }
        GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(data.saveData.ui.home_img_path);//홈 이미지 변경

    }

    void Unit_Select_Change()
    {
        GameObject panel = GameObject.Find("Canvas").transform.Find("Unit_Select_Tap").gameObject;
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
                GameObject panel = GameObject.Find("Canvas").transform.Find("Unit_Select_Tap").gameObject;
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
                GameObject target = GameObject.Find("Canvas").transform.Find("Teams").transform.Find("Character" + idx).gameObject;
                
                for(int i = 0; i < data.saveData.my_characterlist.Count; i++)
                {
                    if(Unit.name == data.saveData.my_characterlist[i].Name)
                    {
                        target.transform.GetChild(0).GetComponent<Image>().sprite = data.saveData.my_characterlist[i].Img;
                        target.transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_characterlist[i].HP.ToString();
                        target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_characterlist[i].ATK.ToString();
                        target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_characterlist[i].Type;
                        data.saveData.my_team[4*tap+int.Parse(idx)-1] = data.saveData.my_characterlist[i];
                        UI_LEVEL2_Controll(1);
                        break;
                    }
                }
            }
            else
            {
                UI("Unit_Upgrade_Tap");
                GameObject panel = GameObject.Find("Canvas").transform.Find("Unit_Upgrade_Tap").gameObject;
                GameObject target = panel.transform.Find("Image").gameObject;
                
                for(int i = 0; i < data.saveData.my_characterlist.Count; i++)
                {
                    if(Unit.name == data.saveData.my_characterlist[i].Name)
                    {
                        target.GetComponent<Image>().sprite = data.saveData.my_characterlist[i].Img;
                        leftPiecesNum = data.saveData.my_characterlist[i].Same;
                        charUpgrade = data.saveData.my_characterlist[i].Upgrade; //유닛 강화 창에서 보여지는 캐릭터의 업그레이드 저장할 int
                        SetStatForm();
                        usePiece.text = "사용할 기억의 조각 : " + basicSettingNum.ToString();
                        break;
                    }
                }
            }
        }
        else
        {
            UI("Unit_Details");
            GameObject target = GameObject.Find("Unit_Details");
            for(int i = 0; i < data.saveData.my_characterlist.Count; i++)
            {
                if(Unit.name == data.saveData.my_characterlist[i].Name)
                {
                    Color col;
                    string txt;
                    switch(data.saveData.my_characterlist[i].Attribute)
                    {
                        case 0:
                            col = new Color(1,0,0);
                            txt = "열정";
                            break;
                        case 1:
                            col = new Color(0,0,1);
                            txt = "냉정";
                            break;
                        case 2:
                            col = new Color(0,1,0);
                            txt = "조화";
                            break;
                        case 3:
                            col = new Color(1,1,0);
                            txt = "외계";
                            break;
                        case 4:
                            col = new Color(0.5f,0.5f,0.5f);
                            txt = "초월";
                            break;
                        default :
                            col = new Color(1,1,1); 
                            txt = "";
                            break;
                    }
                    target.transform.GetChild(0).GetComponent<Image>().color = col;
                    target.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = txt;
                    target.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = data.saveData.my_characterlist[i].Name;
                    target.transform.GetChild(1).GetComponent<Image>().sprite = data.saveData.my_characterlist[i].Img;
                    target.transform.GetChild(2).GetComponent<Text>().text = data.saveData.my_characterlist[i].HP.ToString();
                    target.transform.GetChild(3).GetComponent<Text>().text = data.saveData.my_characterlist[i].ATK.ToString();
                    float extra_hp = data.saveData.my_characterlist[i].HP * 0.2f, extra_atk = data.saveData.my_characterlist[i].ATK * 0.2f;
                    target.transform.GetChild(4).GetComponent<Text>().text = ((int)(extra_hp*data.saveData.my_characterlist[i].Appear)).ToString();
                    target.transform.GetChild(5).GetComponent<Text>().text = ((int)(extra_atk*data.saveData.my_characterlist[i].Appear)).ToString();
                    target.transform.GetChild(6).GetComponent<Text>().text = data.saveData.my_characterlist[i].Type;
                    break;
                }
            }
        }
        time = 0;
        
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

    public void Change_Home_Img()
    {
        GameObject target = GameObject.Find("Unit_Details");
        string path = AssetDatabase.GetAssetPath(target.transform.GetChild(1).GetComponent<Image>().sprite);
        Debug.Log(path);
        path = path.Replace("Assets/Resources/",string.Empty);
        path = path.Replace(".png",string.Empty);
        GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        data.saveData.ui.home_img_path = path;
        data.Save();
        UI_LEVEL1_Controll(0);
        UI_LEVEL2_Controll(0);
    }
    #endregion
    #region doyeon
    // Functions for Unit Upgrade
    public void SetStatForm()
    {
        changeStat.text = "스탯 변화 : +" + presentStatNum.ToString() + "% -> +" + futureStatNum.ToString() + "%";
        leftPiece.text = "남은 기억의 조각 : " + leftPiecesNum.ToString();
        //usePiece.text = "사용할 기억의 조각 : " + basicSettingNum.ToString();
    }
    public void IncreasePieceNum()
    {
        if(leftPiecesNum != 0)
        {
            if(presentStatNum == 50)
            {
                fullStat = true;
            }
            if(!fullStat)
            {
                presentUseNum++;
                leftPiecesNum--;
                futureStatNum += 10;
                SetStatForm();
                usePiece.text = "사용할 기억의 조각 : " + presentUseNum.ToString();
                if(futureStatNum == 50)
                {
                    fullStat = true;
                }
            }
        }
        
    }

    public void DecreasePieceNum()
    {
        if(presentStatNum == 50)
        {
            fullStat = true;
        }
        if(presentUseNum != 0)
        {
            presentUseNum--;
            leftPiecesNum++;
            futureStatNum -= 10;
            SetStatForm();
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
        charUpgrade = presentUseNum;
        data.Save();
    }

    // Functions for Gacha

    public void GachaBasicSetting()
    {
        if(currentDotPos >= 0 && currentDotPos < 8) 
        { // Change dot imgs
            sliderDotsList[currentDotPos].GetComponent<Image>().sprite = fullDot;
            for (int i = 0; i < currentDotPos; i++)
            {
                sliderDotsList[i].GetComponent<Image>().sprite = emptyDot;
            }
            for (int i = 7; i > currentDotPos; i--)
            {
                sliderDotsList[i].GetComponent<Image>().sprite = emptyDot;
            }
            // Change gacha banner img
            bannerImg.GetComponent<Image>().sprite = bannerImgList[currentDotPos];
        }
        if(currentDotPos != 0)
        {
            leftBtn.GetComponent<Button>().interactable = true;
        }
        if(currentDotPos != 7)
        {
            rightBtn.GetComponent<Button>().interactable = true;
        }
    }
    public void SliderLeftBtn()
    {
        if (currentDotPos == 0)
        {
            leftBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            leftBtn.GetComponent<Button>().interactable = true;
            currentDotPos--;
            GachaBasicSetting();
        }

    }
    public void SliderRightBtn()
    {
        if (currentDotPos == 7)
        {
            rightBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            rightBtn.GetComponent<Button>().interactable = true;
            currentDotPos++;
            GachaBasicSetting();
        }
    }

    public void OnePopBtn()
    {
        if(currency >= 10)
        {
            mainbanner.SetActive(false);
            gachaResult.SetActive(true);
            currency -= 10;
            //data.saveData.ui.money[0] = currency;
            Pop_Up(1,currentDotPos);
            data.Save();
        }
        else
        {
            alertPop.SetActive(true);
        }
    }
    public void TenPopBtn()
    {
        if(currency >= 100)
        {
            mainbanner.SetActive(false);
            gachaResult.SetActive(true);
            currency -= 100;
            //data.saveData.ui.money[0] = currency;
            Pop_Up(10,currentDotPos);
            data.Save();
        }
        else
        {
            alertPop.SetActive(true);
        }
    }

    public void ClosePopup(string name)
    {
        GameObject panel = GameObject.Find("Canvas").transform.Find("Gacha").transform.Find(name).gameObject;
        if(panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
    }

    void Pop_Up(int num, int stage)
    {
        List<Character> output_list = new List<Character>();//화면 출력할 때 사용할 리스트
        List<Character> cur_list = new List<Character>();//새로운 리스트 선언
        GameObject clear = GameObject.Find("Gacha").transform.GetChild(2).transform.GetChild(0).gameObject;
        if(clear.transform.childCount > 0)
        {
            for(int i = 0; i < clear.transform.childCount; i++)
            {
                Destroy(clear.transform.GetChild(i).gameObject);
            }
        }
        for(int i = 0; i < data.saveData.list.Count; i++)
        {
            if(data.saveData.list[i].Appear == stage)//입력받은 스테이지와 같다면 새로운 리스트에 추가
                cur_list.Add(data.saveData.list[i]);
        }
        for(int count = 0; count < num; count++)
        {
            int random = (int)Random.Range(0,10000);//난수하나 생성 - 문도현 "확률은 소수 2째자리까지 -> 대충 100 곱하면 10000이하니깐 이거 이하의 난수 하나 생성
            int level = 0;//몇성을 뽑을지 결정하기 위한 변수
            if(random <= (int)(possibility[0] * 100))//3성 확률 변수 * 100보다 작거나 같다면 3성
            {
                level = 3;
            }
            else if(random <= (int)((possibility[0] + possibility[1]) * 100))//3성 확률변수에 100을 곱한 값보다 크고 2성확률변수와 3확률변수를 더한 값에 100을 곱한 값보다 작다면 2성
            {
                level = 2;
            }
            else//아니면 1성이지 뭐..
            {
                level = 1;
            }
            for(int i = 0; i < cur_list.Count; i++)
            {
                if(cur_list[i].Star != level)//현재 리스트에서 결정된 level이 아닌 요소들을 제거해줌
                    cur_list.RemoveAt(i);
            }
            int new_random = (int)Random.Range(0,cur_list.Count);//0번째 인덱스부터 현재 리스트의 길이사이의 난수 생성
            Character final = cur_list[new_random];//마지막으로 뽑힌 캐릭터
            if(data.saveData.my_characterlist.Contains(final))//만약 뽑힌녀석이 이미 뽑힌 놈이라면?
            {
                for(int i = 0 ; i< data.saveData.my_characterlist.Count; i++)//그녀석을 찾아서 Same변수를 1더해줌
                {
                    if(data.saveData.my_characterlist[i] == final)
                        data.saveData.my_characterlist[i].Same++;
                }
            }
            else
                data.saveData.my_characterlist.Add(final);//아니면 내 캐릭터 리스트에 추가

            output_list.Add(final);//final들을 출력 리스트들에 저장함
        }
        Transform parent = GameObject.Find("Gacha").transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Transform>();//부모 객체를 찾는 내용, Gatcha패널이 켜져있다는 가정하에 사용
        Vector2 base_pos = new Vector2(100f,1000f);
        if(num == 1)
        {
            Color col;
            switch(output_list[0].Attribute)
            {
                case 1:
                    col = Color.red;
                    break;
                case 2:
                    col = Color.blue;
                    break;
                case 3:
                    col = Color.green;
                    break;
                case 4:
                    col = new Color(1f,1f,0);
                    break;
                case 5:
                    col = Color.gray;
                    break;
                default:
                    col = Color.white;
                    break;
            }
            GameObject temp = Instantiate(Resources.Load<GameObject>("Prefabs/GaCha"));
             temp.GetComponent<Image>().color = col;
            temp.transform.GetChild(0).GetComponent<Image>().sprite = output_list[0].Img;
            temp.transform.SetParent(parent);
            temp.transform.localPosition = new Vector3(425f,-425f);
            temp.name = "뽑기";
        }
        else
        {
            for(int i = 0; i < 10; i++)
            {
                Color col;
                switch(output_list[i].Attribute)
                {
                    case 1:
                        col = Color.red;
                        break;
                    case 2:
                        col = Color.blue;
                        break;
                    case 3:
                        col = Color.green;
                        break;
                    case 4:
                        col = new Color(1f,1f,0);
                        break;
                    case 5:
                        col = Color.gray;
                        break;
                    default:
                        col = Color.white;
                        break;
                }
                GameObject temp = Instantiate(Resources.Load<GameObject>("Prefabs/GaCha"));
                temp.GetComponent<Image>().color = col;
                temp.transform.GetChild(0).GetComponent<Image>().sprite = output_list[i].Img;
                temp.transform.SetParent(parent);
                if(i < 3)
                    temp.transform.localPosition = new Vector3(200f + 225f*i,-200f);
                else if(i < 7)
                    temp.transform.localPosition = new Vector3(100f + 215f*(i-3),-425f);
                else
                    temp.transform.localPosition = new Vector3(200f + 225f*(i-7),-650f);
                temp.name = "뽑기";
            }
            data.Save();
            Load();
        }


    }
    #endregion
}
