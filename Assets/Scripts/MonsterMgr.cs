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
    public List<MonsterDataClass> monsters = new List<MonsterDataClass>();//출현 몬스터

    public int monsterFullHP;//몬스터 전체 체력
    public int currentMonsterHP;//몬스터 현재 체력
    public int monsterAttackDamage;//몬스터 공격 데미지
    public int monsterAttribute;//몬스터 속성

    public int monstersIndex = 0;//몬스터 인덱스

    public void LoadMonsterData()
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
        int mobCount = Random.Range(4, 5);
        monsters.Clear();//초기화
        for (int i = 0; i < mobCount; i++)
        {
            monsters.Add(dataManager.MonsterList[0]);
        }
    }
    public void InitMonster(int index)
    {
        monsterFullHP = monsters[index].HP * 3;//몬스터 체력 초기화
        currentMonsterHP = monsterFullHP;//처음엔 풀피
        monsterAttackDamage = monsters[index].Attack;//몬스터 공격력
        monsterAttribute = monsters[index].Attribute;//몬스터 속성
    }
    //출혈 데미지 계산
    public void MonsterBloodDamage(int hitDamage,int HP)
    {
        currentMonsterHP = Mathf.Max(HP - hitDamage, 0);
    }
    //몬스터가 죽었는가?
    public void MonsterDie()
    {
        if (currentMonsterHP <= 0)
        {
            monsters.RemoveAt(0);//원소 삭제
            if (monsters.Count>0)//남은 몬스터가 더 있는가?
            {
                InitMonster(monstersIndex);//다음 몬스터
                turn.monsterSet();//몬스터 재배치
            }
            else
            {
                monstersIndex = 0;
            }
        }
    }
    //다음 스테이지
    public void StageClear(int stageNum)
    {
        if(stageNum==3)
        {
            stageText.text = "게임 클러어";
            turn.stageNumber = 1;
            //보상
            //월드 선택 씬으로
        }
    }
}