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
    public Text questName;
    public List<int> infoList = new List<int>() {0, 0, 0, 0};
    public List<int> typeList;
    public List<string> charList;

    // Start is called before the first frame update
    void Awake()
    {
        data = DataManager.singleTon;
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
        if (infoList[1] == 0) findepisode = "월령";
        if (infoList[1] == 1) findepisode = "엠피리오";
        if (infoList[1] == 2) findepisode = "제한구역"; // 인포값을 string으로 변환 (json에 한글로 저장돼있어서 비교할려면 변환해줘야함)

        for (int i = 0; i < 4; i++)
        {
            data.playerCharaterNumber[i] = data.saveData.my_team[(selectTeamPage - 1) * 4 + i].idx; // 플레이어 정보를 저장
            //data.playerCharaterNumber[i] = i+1;
        }

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

        data.monsterCharaterNumber[0] = new List<int> { };
        data.monsterCharaterNumber[1] = new List<int> { };
        data.monsterCharaterNumber[2] = new List<int> { }; // 몬스터 정보값에 들어가있는 기본값을 초기화

        for (int i = 0; i < maxStage; i++)
        {
            for (int j = 0; j < tempNum.Count; j++)
            {
                if (data.saveData.QuestData[tempNum[j]].Stage == i + 1) data.monsterCharaterNumber[i].Add(data.saveData.QuestData[tempNum[j]].MonsterIndex);
                //Debug.Log(i + "번 등장 ㅁㄴ스터 " + data.saveData.QuestData[tempNum[j]].MonsterIndex);
            }
        } // 스테이지별로 분리해서 각각의 몬스터인덱스를 저장

        // 플레이어 몬스터인덱스를 전부 저장했으니 씬 넘기기~

        //SceneManager.LoadScene("InGame");
        LoadingScene.SceneLoad("InGame");
    }
    public void SetName(string name)
    {
        //GameObject questDetailTap = Instantiate(questDetailPrefab, justParent);
        questDetailTap.SetActive(true);
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
    public void SelectDifficulty(int diff) // 1 에필로그 2 쉬움 3 보통 4 어려움
    {
        if(diff == 0)
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
            
            story_name += ("Quest" + infoList[2].ToString() + "_Epilogue");

            data.story= story_name;     
            LoadingScene.SceneLoad("Epilog");
        }
        else
            FindChar(diff);
    }

    public void SetDetailPanel()
    {

    }

}
