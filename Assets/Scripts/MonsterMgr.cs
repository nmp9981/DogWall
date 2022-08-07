using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMgr : MonoBehaviour
{
    Data_Manager dataManager;
    CharacterMgr characterMgr;
    Skill skill;
    Turn turn;
    public Text stageText;//스테이지 번호

    public int monsterFullHP;//몬스터 전체 체력
    public int currentMonsterHP;//몬스터 현재 체력
    public int monsterAttackDamage;//몬스터 공격 데미지
    public int monsterAttribute;//몬스터 속성

    int stage = 1;//스테이지

    void LoadMonsterData()
    {
        dataManager.Add_Monster(1, 1, "왕궁영술사", 1, 3500, 3000);
        dataManager.Add_Monster(1, 1, "왕궁영술사", 2, 4200, 3600);
        dataManager.Add_Monster(1, 1, "왕궁영술사", 3, 5040, 4320);
        dataManager.Add_Monster(1, 1, "도깨비", 1, 4500, 2000);
        dataManager.Add_Monster(1, 1, "도깨비", 2, 5400, 2400);
        dataManager.Add_Monster(1, 1, "도깨비", 3, 6480, 2880);
        dataManager.Add_Monster(1, 2, "왕궁영술사", 1, 3500, 3000);
        dataManager.Add_Monster(1, 2, "왕궁영술사", 2, 4200, 3600);
        dataManager.Add_Monster(1, 2, "왕궁영술사", 3, 5040, 4320);
        dataManager.Add_Monster(1, 2, "도깨비", 1, 4500, 2000);
        dataManager.Add_Monster(1, 2, "도깨비", 2, 5400, 2400);
        dataManager.Add_Monster(1, 2, "도깨비", 3, 6480, 2880);
        dataManager.Add_Monster(1, 2, "언령", 1, 3000, 5000);
        dataManager.Add_Monster(1, 2, "언령", 2, 3600, 6000);
        dataManager.Add_Monster(1, 2, "언령", 3, 4320, 7200);
        dataManager.Add_Monster(1, 3, "왕궁영술사", 1, 3500, 3000);
        dataManager.Add_Monster(1, 3, "왕궁영술사", 2, 4200, 3600);
        dataManager.Add_Monster(1, 3, "왕궁영술사", 3, 5040, 4320);
        dataManager.Add_Monster(1, 3, "도깨비", 1, 4500, 2000);
        dataManager.Add_Monster(1, 3, "도깨비", 2, 5400, 2400);
        dataManager.Add_Monster(1, 3, "도깨비", 3, 6480, 2880);
        dataManager.Add_Monster(1, 3, "언령", 1, 3000, 5000);
        dataManager.Add_Monster(1, 3, "언령", 2, 3600, 6000);
        dataManager.Add_Monster(1, 3, "언령", 3, 4320, 7200);
    }
    // Start is called before the first frame update
    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기

        LoadMonsterData();//데이터 불러오기

        monsterFullHP = dataManager.MonsterList[0].HP*100;//몬스터 체력 초기화
        currentMonsterHP = monsterFullHP;//처음엔 풀피
        monsterAttackDamage = dataManager.MonsterList[0].Attack;//몬스터 공격력
        monsterAttribute = dataManager.MonsterList[0].Attribute;//몬스터 속성
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //출혈 데미지 계산
    public void MonsterBloodDamage(int hitDamage,int HP)
    {
        currentMonsterHP = Mathf.Max(HP - hitDamage, 0);
    }
    //몬스터가 죽었는가?
    public bool IsMonsterDie()
    {
        if (currentMonsterHP <= 0)
        {
            NextStage();//다음 스테이지로
            return true;
        }
        else
        {
            return false;
        }
    }
    //다음 스테이지
    public void NextStage()
    {
        if (stage < 3)
        {
            stage++;
        }
        else
        {
            stageText.text = "게임 클러어";
            stage = 1;
        }
    }
}