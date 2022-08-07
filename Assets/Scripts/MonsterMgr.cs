using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//몬스터 정보
public struct MonsterInfo
{
    public int monsterHP, monsterAttack, monsterAttribute;

    public MonsterInfo(int monsterHP, int monsterAttack, int monsterAttribute)
    {
        this.monsterHP = monsterHP;
        this.monsterAttack = monsterAttack;
        this.monsterAttribute = monsterAttribute;
    }
}

public class MonsterMgr : MonoBehaviour
{
    //몬스터 배열 선언 
    public static List<MonsterInfo> monsterList = new List<MonsterInfo>
    {
        new MonsterInfo(1000000,2700,1),
        new MonsterInfo(1300000,3200,2),
        new MonsterInfo(2200000,4000,3)
    };

    CharacterMgr characterMgr;
    Skill skill;
    Turn turn;
    public Text stageText;//스테이지 번호

    public int monsterFullHP;//몬스터 전체 체력
    public int currentMonsterHP;//몬스터 현재 체력
    public int monsterAttackDamage;//몬스터 공격 데미지
    public int monsterAttribute;//몬스터 속성

    int stage = 1;//스테이지

    // Start is called before the first frame update
    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기

        monsterFullHP = monsterList[0].monsterHP;//몬스터 체력 초기화
        currentMonsterHP = monsterFullHP;//처음엔 풀피
        monsterAttackDamage = monsterList[0].monsterAttack;//몬스터 공격력
        monsterAttribute = monsterList[0].monsterAttribute;//몬스터 속성
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //출혈 데미지 계산
    public void MonsterBloodDamage(int hitDamage)
    {
        currentMonsterHP = Mathf.Max(currentMonsterHP - hitDamage, 0);
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