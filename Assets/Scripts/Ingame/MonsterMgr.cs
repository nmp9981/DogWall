﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterMgr : MonoBehaviour
{
    Data_Manager dataManager;
    private DataManager Data;
    CharacterMgr characterMgr;
    MonsterSkill monsterSkillMgr;
    SepcialMonster specialMonster;
    Skill skill;
    Turn turn;
    public Text stageText;//스테이지 번호
    public List<int> MonsterNum;//출현 몬스터 번호
    public List<MonsterDataClass> monsters = new List<MonsterDataClass>();//출현 몬스터(삭제 예정)
    public List<MonstersDataClass> stageMonster = new List<MonstersDataClass>();//출현 몬스터

    public List<int> monsterFullHP = new List<int>();//몬스터 전체 체력
    public List<int> currentMonsterHP = new List<int>();//몬스터 현재 체력
    public int monsterAttackDamage;//몬스터 공격 데미지
    public int monsterAttribute;//몬스터 속성

    public int monstersIndex = 0;//몬스터 인덱스
    CSV_Reader CSVReader;
    public List<Dictionary<string, object>> MonstersData;//몬스터 데이터
    public List<Dictionary<string, object>> MonsterSkillNormal;//일반 스킬
    public List<Dictionary<string, object>> MonsterSkillSpecial;//특수 스킬

    void Awake()
    {
        MonstersData = CSV_Reader.Read("MonsterData"); //몬스터 데이터 불러오기
        MonsterSkillNormal = CSV_Reader.Read("MonsterSkill"); //몬스터 일반 스킬 데이터 불러오기
        MonsterSkillSpecial = CSV_Reader.Read("MonsterSpecialSkill");//몬스터 특수 스킬 데이터 불러오기
    }
    // Start is called before the first frame update
    void Start()
    {
        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기
        monsterSkillMgr = GameObject.FindWithTag("MonsterSkill").GetComponent<MonsterSkill>();//MonsterSkill 스크립트에서 변수 가져오기
        specialMonster = GameObject.FindWithTag("MonsterSpecialSkill").GetComponent<SepcialMonster>();//SepcialMonster스크립트에서 변수 가져오기
        //dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기

        MonsterSetting();//몬스터 리젠
        InitMonster(monstersIndex);//초기 몬스터 세팅
        turn.monsterSet(); // 몬스터 배치
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //몬스터 출현(한 몬스터가 여러마리 등장)
    public void MonsterSetting()
    {
        int mobCount = Random.Range(1, 5);
        MonsterNum.Clear();//출현 몬스터 번호 초기화
        monsters.Clear();//초기화
        monsterFullHP.Clear();
        currentMonsterHP.Clear();
        for (int i = 0; i < mobCount; i++)
        {
            monsters.Add(Data.saveData.MonsterData[0]);//등장 몹은 서로 다름
            MonsterNum.Add(i);//몬스터 번호 담기
            skill.mobProvocation.Clear();//몬스터 도발 초기화
        }
        monsterSkillMgr.monsterNormalSkillSet();//몬스터 스킬 세팅(일반)
        specialMonster.monsterSpecialSkillSet();//몬스터 스킬 세팅(특수)
    }
    public void InitMonster(int index)
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            monsterFullHP.Add(monsters[index].HP);//몬스터 체력 초기화
            currentMonsterHP.Add(monsterFullHP[i]);//처음엔 풀피
        }
        monsterAttackDamage = monsters[index].Attack;//몬스터 공격력
        monsterAttribute = monsters[index].Attribute;//몬스터 속성
    }
    //출혈 데미지 계산
    public void MonsterBloodDamage(int hitDamage,int index)
    {
        currentMonsterHP[index] = Mathf.Max(currentMonsterHP[index] - hitDamage, 0);
    }
    //몬스터가 죽었는가?
    public void MonsterDie(int index)
    {
        if (currentMonsterHP[index] <= 0)
        {
            monsters.RemoveAt(0);//원소 삭제
            if (monsters.Count>0)//남은 몬스터가 더 있는가?
            {
                InitMonster(monstersIndex);//다음 몬스터
                turn.monsterSet();//몬스터 재배치
                currentMonsterHP[index] = monsterFullHP[index];//몬스터는 처음에 풀피
            }
            else
            {
                monstersIndex = 0;
            }
        }
    }
    //다음 스테이지
    public bool StageClear(int stageNum)
    {
        if(stageNum==3)
        {
            stageText.text = "게임 클러어";
            turn.stageNumber = 1;
            //보상
            //월드 선택 씬으로
            SceneManager.LoadScene("Home");//홈으로
            return true;
        }
        return false;
    }
}