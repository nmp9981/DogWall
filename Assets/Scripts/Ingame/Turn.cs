﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Turn : MonoBehaviour
{
    #region 변수
    Data_Manager dataManager; private DataManager Data; CharacterMgr characterMgr; MonsterMgr monsterMgr; TeamSelect teamSelect; Skill skill;MonsterSkill monsterSkillMgr;
    public Text characterNameText, characterAttackText, turnText, stageText, monsterText;
    public int teamNumber;//팀원의 번호
    public int totalDamage;//스킬데미지 총합
    public GameObject[] skill33 = { };
    public GameObject skill_1E; public GameObject skill_2E; public GameObject skill_3E; public GameObject skill_4E;
    public GameObject skill_1T; public GameObject skill_2T; public GameObject skill_3T; public GameObject skill_4T;
    public GameObject skipE; public GameObject player_1E; public GameObject player_2E; public GameObject player_3E; public GameObject player_4E;
    public GameObject turnEndButton;
    public GameObject monster1; public GameObject monster2; public GameObject monster3; public GameObject monster4;
    public RectTransform monster1Rect; public RectTransform monster2Rect; public RectTransform monster3Rect; public RectTransform monster4Rect;
    //몬스터 HP바
    public RectTransform monster1HPBar; public RectTransform monster2HPBar; public RectTransform monster3HPBar; public RectTransform monster4HPBar;
    public GameObject story;
    public int turnNumber = 1; // 턴 확인용 변수
    public int totalTurnNumber = 1;
    public int skillNumber = 1;  // 스킬 확인용 변수
    public int stageNumber = 1; // 스테이지
    public int skillCount = 0;//스킬을 사용한 캐릭터의 수
    public int mobHP;//몬스터 HP
    public List<int> playerSkillSelect = new List<int>();
    public bool skillAvailable = false; // 스킬 사용중인지 확인
    public bool firstAttack = true; // 임의로 정한 선제공격 확인용 변수
    #endregion
    Coroutine longClickCoroutine;

    void Start()
    {
        for (int i = 0; i < 5; i++) playerSkillSelect.Add(0);

        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        monsterSkillMgr = GameObject.FindWithTag("MonsterSkill").GetComponent<MonsterSkill>();//MonsterSkill 스크립트에서 변수 가져오기

        turnText.text = totalTurnNumber.ToString();
        stageText.text = stageNumber + "/3";

        firstAttack = true; // 선제공격 여부 임의로 설정

        if (firstAttack) turnNumber = 1; // 선제공격 여부 확인
        else turnNumber = 5;

        UISetting(); // 턴 시작
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------

    // 플레이어 초상화 선택시 턴 전환

    public void playerSelect(int number)
    {
        turnNumber = number;
        teamNumber = teamSelect.selectedTeamNumber[number - 1];
        UISetting();
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------


    // 모든 스킬을 선택후 턴 종료시 진행

    public void turnEnd()
    {
        mobHP = monsterMgr.currentMonsterHP[0];//임의의 몬스터 HP 설정
        //스킬계산
        totalDamage = 0;//초기화
        //4개의 스킬
        for(int i = 0; i < 4; i++)
        {
            if (playerSkillSelect[i] == 4)//필살기
            {
                //필살기 애니메이션 재생
            }else if (playerSkillSelect[i] == 5)//스킵 버튼
            {
                continue;
            }
            int skillNumber = teamSelect.selectedTeamNumber[i] * 4 + playerSkillSelect[i] - 1;//스킬의 번호
            skill.InitTurn(skillNumber);//턴 초기화
            characterMgr.ColorCondition(skill.playerAttack, i, skillNumber);//캐릭터 상태 이상 색상 표시
            skill.TurnCountText(skillNumber,i);//남은 턴 수 나타내기
            totalDamage += skill.skillAttackDamage(skillNumber,i);//데미지 누적
        }
        monsterMgr.MonsterBloodDamage(totalDamage,mobHP,0);//몬스터 데미지
        totalTurnNumber += 1;
        turnText.text = totalTurnNumber.ToString();

        playerSkillSelect[0] = 0; // 스킬 초기화
        playerSkillSelect[1] = 0;
        playerSkillSelect[2] = 0;
        playerSkillSelect[3] = 0;

        player_1E.SetActive(false); // 이펙트들 초기화
        player_2E.SetActive(false);
        player_3E.SetActive(false);
        player_4E.SetActive(false);
        skill_1E.SetActive(false);
        skill_2E.SetActive(false);
        skill_3E.SetActive(false);
        skill_4E.SetActive(false);
        skipE.SetActive(false);

        //몬스터 사망여부 확인
        monsterMgr.MonsterDie(0);

        if (monsterMgr.monsters.Count == 0) // 몬스터가 다 죽었다면 스테이지 증가
        {
            //클리어 여부 확인
            if (!monsterMgr.StageClear(stageNumber))
            {
                stageNumber += 1;
                stageText.text = stageNumber.ToString();

                //다음 스테이지 몬스터 등장
                monsterMgr.MonsterSetting();//몬스터 리젠
                monsterMgr.InitMonster(monsterMgr.monstersIndex);//초기 몬스터 세팅
                monsterSet(); // 몬스터 배치

                if (firstAttack) turnNumber = 1; // 선제공격 여부 확인
                else turnNumber = 5;
            }
        }

        turnNumber = 5; // 보스로 턴 전환

        Invoke("UISetting", 1);
    }

    //-----------------------------------------------------------------------------------------------------



    // 플레어이 초상화 클릭 & 스킬 선택시 그에 맞춰서 UI를 세팅하는 함수
    // 어떤 값을 수정했으면 꼭 실행 해줘야함

    void UISetting() // 턴 관리 1, 2, 3, 4 - 플레이어 1, 2, 3 ,4    5 -  몬스터 턴
    {

        //characterNameText.text = Data.saveData.CharacterData[teamNumber / 5].Name; // 캐릭터 공격력 & 이름 UI 표시
        //characterAttackText.text = "Attack : " + Data.saveData.CharacterData[teamNumber/5].Attack;


        skillNumber = playerSkillSelect[turnNumber - 1]; // 저장된 스킬 넘버를 턴에 맞춰서 가져옴


        if (playerSkillSelect[0] != 0 & playerSkillSelect[1] != 0 & playerSkillSelect[2] != 0 & playerSkillSelect[3] != 0) turnEndButton.SetActive(true);
        else turnEndButton.SetActive(false); // 모든 플레이어의 스킬이 선택 됐다면 턴 넘기기 버튼이 나타남



        skill_1E.SetActive(false); // 스킬선택 이펙트 초기화
        skill_2E.SetActive(false);
        skill_3E.SetActive(false);
        skill_4E.SetActive(false);
        skipE.SetActive(false);

        if (playerSkillSelect[0] == 0) player_1E.SetActive(false); // 플레이어 초상화 선택 이펙트 관리
        else player_1E.SetActive(true);
        if (playerSkillSelect[1] == 0) player_2E.SetActive(false);
        else player_2E.SetActive(true);
        if (playerSkillSelect[2] == 0) player_3E.SetActive(false);
        else player_3E.SetActive(true);
        if (playerSkillSelect[3] == 0) player_4E.SetActive(false);
        else player_4E.SetActive(true);


        switch (playerSkillSelect[turnNumber - 1])
        {
            case 0:
                skill_1E.SetActive(false);
                skill_2E.SetActive(false);
                skill_3E.SetActive(false);
                skill_4E.SetActive(false);
                skipE.SetActive(false);
                break;
            case 1:
                skill_1E.SetActive(true);
                break;
            case 2:
                skill_2E.SetActive(true);
                break;
            case 3:
                skill_3E.SetActive(true);
                break;
            case 4:
                skill_4E.SetActive(true);
                break;
            case 5:
                skipE.SetActive(true);
                break;
            default:
                break;
        } // 스킬 이펙트 설정

        if (turnNumber == 5) // 턴이 5라면 몬스터 턴 시작
        {
            monster();
        }
    }


    // 몬스터 관련 턴 ------------------------------------

    public void monsterSet() // 몬스터 배치(위치, HPbar)
    {
        monster1.SetActive(false);
        monster2.SetActive(false);
        monster3.SetActive(false);
        monster4.SetActive(false);
        
        if (monsterMgr.monsters.Count == 0)
        {
            monster1.SetActive(false);
            monster2.SetActive(false);
            monster3.SetActive(false);
            monster4.SetActive(false);
        }
        if(monsterMgr.monsters.Count == 1)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(0, 400, 0);
            monster1Rect.sizeDelta = new Vector2(700, 700);
            monster1HPBar.anchoredPosition = new Vector3(0, -400, 0);
            monster1HPBar.localScale = new Vector2(1.0f, 1.0f);
            //몹 이미지 배치
            monster2.SetActive(false);
            monster3.SetActive(false);
            monster4.SetActive(false);
        }
        if(monsterMgr.monsters.Count == 2)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-250, 400, 0);
            monster1Rect.sizeDelta = new Vector2(400, 400);
            monster1HPBar.anchoredPosition = new Vector3(-50, -370, 0);
            monster1HPBar.localScale = new Vector2(0.5f, 0.5f);
            //몹 이미지 배치
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(250, 400, 0);
            monster2Rect.sizeDelta = new Vector2(400, 400);
            monster2HPBar.anchoredPosition = new Vector3(50, -370, 0);
            monster2HPBar.localScale = new Vector2(0.5f, 0.5f);
            //몹 이미지 배치
            monster3.SetActive(false);
            monster4.SetActive(false);
        }
        if (monsterMgr.monsters.Count == 3)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-250, 400, 0);
            monster1Rect.sizeDelta = new Vector2(300, 300);
            monster1HPBar.anchoredPosition = new Vector3(-100, -310, 0);
            monster1HPBar.localScale = new Vector2(0.33f, 0.33f);
            //몹 이미지 배치
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(0, 400, 0);
            monster2Rect.sizeDelta = new Vector2(300, 300);
            monster2HPBar.anchoredPosition = new Vector3(0, -310, 0);
            monster2HPBar.localScale = new Vector2(0.33f, 0.33f);
            //몹 이미지 배치
            monster3.SetActive(true);
            monster3Rect.anchoredPosition = new Vector3(250, 400, 0);
            monster3Rect.sizeDelta = new Vector2(300, 300);
            monster3HPBar.anchoredPosition = new Vector3(100, -310, 0);
            monster3HPBar.localScale = new Vector2(0.33f, 0.33f);
            //몹 이미지 배치
            monster4.SetActive(false);
        }
        if (monsterMgr.monsters.Count == 4)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-405, 400, 0);
            monster1Rect.sizeDelta = new Vector2(200, 200);
            monster1HPBar.anchoredPosition = new Vector3(-20, -250, 0);
            monster1HPBar.localScale = new Vector2(0.25f, 0.25f);
            //몹 이미지 배치
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(-135, 400, 0);
            monster2Rect.sizeDelta = new Vector2(200, 200);
            monster2HPBar.anchoredPosition = new Vector3(-7, -250, 0);
            monster2HPBar.localScale = new Vector2(0.25f, 0.25f);
            //몹 이미지 배치
            monster3.SetActive(true);
            monster3Rect.anchoredPosition = new Vector3(135, 400, 0);
            monster3Rect.sizeDelta = new Vector2(200, 200);
            monster3HPBar.anchoredPosition = new Vector3(7, -250, 0);
            monster3HPBar.localScale = new Vector2(0.25f, 0.25f);
            //몹 이미지 배치
            monster4.SetActive(true);
            monster4Rect.anchoredPosition = new Vector3(405, 400, 0);
            monster4Rect.sizeDelta = new Vector2(200, 200);
            monster4HPBar.anchoredPosition = new Vector3(20, -250, 0);
            monster4HPBar.localScale = new Vector2(0.25f, 0.25f);
            //몹 이미지 배치
        }
    }

    void monster() // 몬스터 턴
    {
        characterMgr.PlayerBloodDamage(0);//출혈 데미지, 캐릭터 지정?
        turnNumber = 1;
        skillCount = 0;//스킬을 사용한 횟수 초기화
        totalDamage = 0;//스킬 데미지 초기화

        if (monster1.activeSelf == true) // 몬스터가 살아있으면 턴을 진행
        {
            //퀘스트 시트에서 어떤 몬스터가 등장하고 어떤 스킬을 사용하는지 받아야함
            int mobHitDamage = monsterSkillMgr.monsterSkillDamage(0,1,0,0);//몬스터 인덱스, 스킬번호, 특수스킬, 피격 캐릭터 번호(배열을 받을 예정)
            characterMgr.PlayerBloodDamage(mobHitDamage);//공격
            monsterText.text = "화염방사";
        }
        if (monster2.activeSelf == true)
        {
            int mobHitDamage = monsterSkillMgr.monsterSkillDamage(1, 2, 0,0);//몬스터 인덱스, 스킬번호, 특수스킬, 피격 캐릭터 번호
            characterMgr.PlayerBloodDamage(mobHitDamage);//공격
            monsterText.text = "백만볼트";
        }
        if (monster3.activeSelf == true)
        {
            int mobHitDamage = monsterSkillMgr.monsterSkillDamage(2, 3, 0,0);//몬스터 인덱스, 스킬번호, 특수스킬, 피격 캐릭터 번호
            characterMgr.PlayerBloodDamage(mobHitDamage);//공격
            monsterText.text = "칼춤";
        }
        if (monster4.activeSelf == true)
        {
            int mobHitDamage = monsterSkillMgr.monsterSkillDamage(3, 4, 0,0);//몬스터 인덱스, 스킬번호, 특수스킬, 피격 캐릭터 번호
            characterMgr.PlayerBloodDamage(mobHitDamage);//공격
            monsterText.text = "잠자기";
        }


    }

    // --------------------------------------------------------------


    // 스킬 & 스킵 버튼 관련 턴
    public void buttonDown(int number)
    {
        skillNumber = number;
        longClickCoroutine = StartCoroutine(longClick());
        skillAvailable = true;
    }

    public void turnSkip()
    {
        skillNumber = 5;
        playerSkillSelect[turnNumber - 1] = skillNumber;
        UISetting();
    }

    public void buttonUp()
    {
        StopCoroutine(longClickCoroutine);
        playerSkillSelect[turnNumber - 1] = skillNumber;
        skill_1T.SetActive(false);
        skill_2T.SetActive(false);
        skill_3T.SetActive(false);
        skill_4T.SetActive(false);
        if (skillAvailable)
        {
            //totalDamage += skill.skillAttackDamage(teamNumber);//스킬 데미지 더하기
            UISetting(); // 스킬 사용후 턴 넘기기
        }
    }

    IEnumerator longClick()
    {
        yield return new WaitForSeconds(1f);
        skillAvailable = false;
        switch (skillNumber)
        {
            case 1:
                skill_1T.SetActive(true);
                break;
            case 2:
                skill_2T.SetActive(true);
                break;
            case 3:
                skill_3T.SetActive(true);
                break;
            case 4:
                skill_4T.SetActive(true);
                break;
            default:
                break;
        }

    }

    //------------------------------------------------------------------------------------------------

    // 스토리UI 관련
    public void storyOn()
    {
        story.SetActive(true);
    }
    public void storyOff()
    {
        story.SetActive(false);
    }
}