using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class Quest_Manager : MonoBehaviour
{
    DataManager data;
    SaveDataClass saveData;
    public List<StageInfo> questList;
    int a, e, q, s, d;
    public int listIndex = 0;
    public int selectTeamPage = 1; // 선택한 팀 구성 페이지 (1번 페이지 = 1, 2번 페이지 = 2...)
    // appear = 0 현재
    // episode = 0 월령 , 1 엠피레오, 2 제한구혁
    //public GameObject questDetailPrefab;
    //public Transform justParent;
    public GameObject questDetailTap;
    public GameObject questScroll;
    public List<GameObject> clearImage;
    public Text questName;
    public List<int> infoList = new List<int>() {0, 0, 0, 0};
    public List<int> typeList;
    public List<string> charList;
    UI_Manager ui;
    // Start is called before the first frame update
    void Awake()
    {
        data = DataManager.singleTon;
        ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        saveData = data.saveData;
    }

    // Update is called once per frame
    public void FindChar(int diff) // 퀘스트 선택창에서 받은 난이도값을 토대로 몬스터정보, 플레이어 정보를 싱글톤에 저장해서 인게임쪽으로 전달
    {
        string findappear = "현재";
        string findepisode = "월령";
        int maxStage = 1;
        List<int> tempNum = new List<int> { };
        if (infoList[0] == 0) findappear = "현재";
        if (infoList[1] == 0)
        {
            findepisode = "월령";
            Sound_Manager.sound.Play("월령국_전투BGM_최종");
        }
        if (infoList[1] == 1)
        {
            findepisode = "엠피리오";
            Sound_Manager.sound.Play("엠피리언_전투BGM_2");
        }
        if (infoList[1] == 2)
        {
            findepisode = "제한구역"; // 인포값을 string으로 변환 (json에 한글로 저장돼있어서 비교할려면 변환해줘야함)
            Sound_Manager.sound.Play("제한구역");
        }

        DataManager.singleTon.saveData.inGameData.playerData.Clear();

        for (int i = 0; i < 4; i++)
        {
            DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[data.saveData.my_team[(selectTeamPage - 1) * 4 + i].idx]);
        } // 팀정보를 추가

        for (int j = 0; j < data.saveData.QuestData.Count; j++)
        {
            if (
                (data.saveData.QuestData[j].Appear == findappear) &&
                (data.saveData.QuestData[j].Episode == findepisode) &&
                (data.saveData.QuestData[j].Quest == infoList[2]) &&
                (data.saveData.QuestData[j].Difficulty == diff)
                )
            {
                //Debug.Log(j);
                tempNum.Add(j); // 시간 에피소드 퀘스트 난이도에 맞는 모든 데이터 인덱스를 저장
            }
        }

        for (int i = 0; i < tempNum.Count; i++)
        {
            if (maxStage < data.saveData.QuestData[tempNum[i]].Stage) maxStage = data.saveData.QuestData[tempNum[i]].Stage; // 총 몇 스테이지인지 판별
        }

        DataManager.singleTon.saveData.inGameData.monsterData.Clear();
        List<MonstersDataClass> monster1 = new List<MonstersDataClass>();
        List<MonstersDataClass> monster2 = new List<MonstersDataClass>();
        List<MonstersDataClass> monster3 = new List<MonstersDataClass>(); // 몬스터 정보값에 들어가있는 기본값을 초기화

        for (int j = 0; j < tempNum.Count; j++)
        {
            if (data.saveData.QuestData[tempNum[j]].Stage == 1)
            {
                
                monster1.Add(DataManager.singleTon.saveData.MonsterData[data.saveData.QuestData[tempNum[j]].MonsterIndex - 2]);
            }
            if (data.saveData.QuestData[tempNum[j]].Stage == 2)
            {
                monster2.Add(DataManager.singleTon.saveData.MonsterData[data.saveData.QuestData[tempNum[j]].MonsterIndex - 2]);
            }
            if (data.saveData.QuestData[tempNum[j]].Stage == 3)
            {
                monster3.Add(DataManager.singleTon.saveData.MonsterData[data.saveData.QuestData[tempNum[j]].MonsterIndex - 2]);
            }
        } // 스테이지별로 분리해서 각각의 몬스터인덱스를 저장

        DataManager.singleTon.saveData.inGameData.monsterData.Add(monster1);
        DataManager.singleTon.saveData.inGameData.monsterData.Add(monster2);
        DataManager.singleTon.saveData.inGameData.monsterData.Add(monster3);

        // 플레이어 몬스터인덱스를 전부 저장했으니 씬 넘기기~

        //SceneManager.LoadScene("InGame");
        LoadingScene.SceneLoad("InGame");
    }

    public void SetName(string name)
    {
        clearImage[0].SetActive(false);
        clearImage[1].SetActive(false);
        clearImage[2].SetActive(false);
        //GameObject questDetailTap = Instantiate(questDetailPrefab, justParent);
        //questDetailTap.SetActive(true);
        //questScroll.SetActive(false);
        ui.UI_LEVEL1_Controll(7);
        StartCoroutine("SettingClear");
        questName.text = name;
        //questDetailTap.SetActive(true);
    }
    public void SetAppear(int appear)
    {
        infoList[0] = appear;
    }
    public void SetEpisode(int episode)
    {
        infoList[1] = episode;
    }
    public void SetQuest(int quest)
    {
        infoList[2] = quest;
    }
    public void SetStage(int stage)
    {
        infoList[3] = stage;
    }
    public void SelectDifficulty(int diff) // 0 에필로그 1 쉬움 2 보통 3 어려움 4 프롤로그
    {
        if(diff == 0 || diff == 4)
        {
            string story_name = "";
            switch(infoList[1])
            {
                case 0:
                    story_name += "Wulryeong_";
                    break;
                case 1:
                    story_name += "Empyrean_";
                    break;
                case 2:
                    story_name += "RestrictedArea_";
                    break;
            }

            switch(infoList[0])//현재만 나온 관계로 일단은 전부다 Present_
            {
                case 0:
                    story_name += "Present_";
                    break;
                case 1:
                    story_name += "Present_";
                    break;
                case 2:
                    story_name += "Present_";
                    break;
            }
            if(diff == 0)
                story_name += ("Quest" + infoList[2].ToString() + "_Epilogue");
            else
                story_name += ("Quest" + infoList[2].ToString() + "_Prolog");
            data.story= story_name;     
            LoadingScene.SceneLoad("Epilog");
        }
        else
            FindChar(diff);
    }

    IEnumerator SettingClear()
    {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < 3; i++)
        {
            if (data.saveData.clearInfo[infoList[1] * 9 + (infoList[2]-1) * 3 + i] == 1) clearImage[i].SetActive(true);
        }
    }

    public void SetDetailPanel()
    {

    }

}
