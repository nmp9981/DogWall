﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Manager : MonoBehaviour
{
    DataManager data;
    SaveDataClass saveData;
    public List<StageInfo> questList;
    int a, e, q, s, d;
    public int listIndex = 0;
    // appear = 0 현재
    // episod = 0 월령 , 1 엠피, 2 제한
    //public GameObject questDetailPrefab;
    //public Transform justParent;
    public GameObject questDetailTap;
    public Text questName;
    public List<int> infoList = new List<int>() {0, 0, 0, 0};
    public List<int> typeList;
    public List<string> charList;

    // Start is called before the first frame update
    void Start()
    {
        data = DataManager.singleTon;
        saveData = data.saveData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FindChar(int diff)
    {

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
    public void SelectDifficulty(int diff)
    {
        FindChar(diff);
    }
}
