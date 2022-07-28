using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    TeamSelect teamSelect;
    Skill skill;

    public Text test; // 테스트용 텍스트(턴)
    public Text test2; // 테스트용 텍스트(스킬준비)
    public Text test3; // 테스트용 텍스트(스킬사용)
    public Text test4;
    public Text turnText;
    public Text stageText;

    public int teamNumber;//팀원의 번호
    public int totalDamage;//스킬데미지 총합

    public GameObject skill_1E;
    public GameObject skill_2E;
    public GameObject skill_3E;
    public GameObject skill_4E;
    public GameObject skipE;
    public GameObject player_1E;
    public GameObject player_2E;
    public GameObject player_3E;
    public GameObject player_4E;
    public GameObject turnEndButton;

    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public RectTransform monster1Rect;
    public RectTransform monster2Rect;
    public RectTransform monster3Rect;
    public RectTransform monster4Rect;

    public int turnNumber = 1; // 턴 확인용 변수
    public int totalTurnNumber = 1;
    public int skillNumber = 1;  // 스킬 확인용 변수
    public int stageNumber = 1; // 스테이지
    public int skillCount = 0;//스킬을 사용한 캐릭터의 수

    public List<int> playerSkillSelect = new List<int>();
    public List<int> monsters = new List<int>();

    public bool skillAvailable = false; // 스킬 사용중인지 확인

    public bool firstAttack = true; // 임의로 정한 선제공격 확인용 변수

    void Start()
    {
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);

        monsters.Add(1);
        monsters.Add(1);

        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기

        test.text = "시작";
        test2.text = "대기중";
        test3.text = "테스트";
        test4.text = "몬스터수" + monsters.Count;
        turnText.text = totalTurnNumber.ToString();
        stageText.text = stageNumber + "/3";
        skillAvailable = false; // 스킬 사용가능 확인용 변수
        firstAttack = true; // 선제공격 확인용 변수

        if (firstAttack) turnNumber = 1; // 선제공격 여부 확인
        else turnNumber = 5;

        monsterSet(); // 몬스터 배치
        battle(); // 턴 시작
    }
    public void playerSelect1()
    {
        turnNumber = 1;
        teamNumber = teamSelect.selectedTeamNumber[0];
        battle();
    }
    public void playerSelect2()
    {
        turnNumber = 2;
        teamNumber = teamSelect.selectedTeamNumber[1];
        battle();
    }
    public void playerSelect3()
    {
        turnNumber = 3;
        teamNumber = teamSelect.selectedTeamNumber[2];
        battle();
    }
    public void playerSelect4()
    {
        turnNumber = 4;
        teamNumber = teamSelect.selectedTeamNumber[3];
        battle();
    }

    void battle() // 턴 관리 1, 2, 3, 4 - 플레이어 1, 2, 3 ,4    5 -  보스 턴 시작
    {
        if (playerSkillSelect[0] != 0 & playerSkillSelect[1] != 0 & playerSkillSelect[2] != 0 & playerSkillSelect[3] != 0) turnEndButton.SetActive(true);
        else turnEndButton.SetActive(false);
        skill_1E.SetActive(false);
        skill_2E.SetActive(false);
        skill_3E.SetActive(false);
        skill_4E.SetActive(false);
        skipE.SetActive(false);
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
                test2.text = "오류발생";
                break;
        }
        skillNumber = playerSkillSelect[turnNumber - 1];
        switch (turnNumber)
        {
            case 1: // 플레이어 턴 전체적인 시작 순서  battle > 스킬 버튼 클릭 > 스킬 사용 > battle
                test.text = "1";
                skillAvailable = true;//사용 가능
                if (playerSkillSelect[0] == 0) player_1E.SetActive(false);
                else player_1E.SetActive(true);//버튼 활성화
                test2.text = "스킬 사용 대기중";
                break;
            case 2:
                test.text = "2";
                skillAvailable = true;
                if(playerSkillSelect[1] == 0) player_2E.SetActive(false);
                else player_2E.SetActive(true);
                test2.text = "스킬 사용 대기중";
                break;
            case 3:
                test.text = "3";
                skillAvailable = true;
                if(playerSkillSelect[2] == 0) player_3E.SetActive(false);
                else player_3E.SetActive(true);
                test2.text = "스킬 사용 대기중";
                break;
            case 4:
                test.text = "4";
                if(playerSkillSelect[3] == 0) player_4E.SetActive(false);
                else player_4E.SetActive(true);
                test2.text = "스킬 사용 대기중";
                break;
            case 5:
                test.text = "5";
                boss();
                if (!characterMgr.IsPlayerDie())//플레이어 체력 체크
                {
                    skillAvailable = true;
                    test2.text = "스킬 사용 대기중";
                }
                else
                {
                    test.text = "사망";//게임 오버 화면
                                     //홈화면으로
                }
                break;
            default:
                test.text = "버그발생";
                break;
        }
    }

    public void skillNumberSet1() // 버튼 클릭시 함수 실행 
    {
        if (skillAvailable)
        {
            skillNumber = 1;
            skillCount += 1;
            playerSkillSelect[turnNumber - 1] = skillNumber;
            totalDamage += skill.skill1(teamNumber);//스킬 데미지 더하기
            skillAvailable = false;
            test2.text = "스킬1 선택";
            battle(); // 스킬 사용후 턴 넘기기
        }
    }
    public void skillNumberSet2()
    {
        if (skillAvailable)
        {
            skillNumber = 2;
            skillCount += 1;
            playerSkillSelect[turnNumber - 1] = skillNumber;
            totalDamage += skill.skill2(teamNumber);//스킬 데미지 더하기
            skillAvailable = false;
            test2.text = "스킬2 선택";
            battle();
        }
    }
    public void skillNumberSet3()
    {
        if (skillAvailable)
        {
            skillNumber = 3;
            skillCount += 1;
            playerSkillSelect[turnNumber - 1] = skillNumber;
            totalDamage += skill.skill3(teamNumber);//스킬 데미지 더하기
            skillAvailable = false;
            test2.text = "스킬3 선택";
            battle();
        }
    }
    public void skillNumberSet4()
    {
        if (skillAvailable)
        {
            skillNumber = 4;
            skillCount += 1;
            playerSkillSelect[turnNumber - 1] = skillNumber;
            totalDamage += skill.skill4(teamNumber);//스킬 데미지 더하기
            skillAvailable = false;
            test2.text = "스킬4 선택";
            battle();
        }
    }
    public void turnSkip()
    {
        if (skillAvailable)
        {
            skillNumber = 5;
            playerSkillSelect[turnNumber - 1] = skillNumber;
            test2.text = "턴 스킵";
            battle();
        }
    }

    public void turnEnd()
    {
        test3.text = "스킬들 사용"; // 턴종료후 스킬 사용 ( 플레이어들 스킬은 여기서 전부 사용)
        monsterMgr.MonsterBloodDamage(totalDamage);//몬스터 데미지
        totalTurnNumber += 1;
        turnText.text = totalTurnNumber.ToString();

        playerSkillSelect[0] = 0; // 스킬 초기화
        playerSkillSelect[1] = 0;
        playerSkillSelect[2] = 0;
        playerSkillSelect[3] = 0;
        player_1E.SetActive(false);
        player_2E.SetActive(false);
        player_3E.SetActive(false);
        player_4E.SetActive(false);
        skill_1E.SetActive(false);
        skill_2E.SetActive(false);
        skill_3E.SetActive(false);
        skill_4E.SetActive(false);
        skipE.SetActive(false);
        turnNumber = 5;

        if (totalTurnNumber == 3) monsters.RemoveAt(0); // 당장은 몬스터 HP 계산이 없어서 임시로 몬스터 제거
        if (totalTurnNumber == 4) monsters.RemoveAt(0);
        monsterSet();

        if (monsters.Count == 0)
        {
            stageNumber += 1;
            stageText.text = stageNumber.ToString();
            if (firstAttack) turnNumber = 1; // 선제공격 여부 확인
            else turnNumber = 5;
        }

        if (stageNumber == 2) // 스테이지 넘어가면 몬스터 정보 받아오는게 원래 계획 지금은 데이터베이스가 없어서 임의로 추가
        {
            monsters.Add(0);
            monsters.Add(0);
            monsters.Add(0);
            monsters.Add(0);
            Invoke("monsterSet", 1);
        }

        Invoke("battle", 1);
    }

    // 몬스터 관련 턴 ------------------------------------

    void monsterSet()
    {
        monster1.SetActive(false);
        monster2.SetActive(false);
        monster3.SetActive(false);
        monster4.SetActive(false);
        if (monsters.Count == 0)
        {
            monster1.SetActive(false);
            monster2.SetActive(false);
            monster3.SetActive(false);
            monster4.SetActive(false);
        }
        if(monsters.Count == 1)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(0, 400, 0);
            monster1Rect.sizeDelta = new Vector2(700, 700);
            monster2.SetActive(false);
            monster3.SetActive(false);
            monster4.SetActive(false);
        }
        if(monsters.Count == 2)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-250, 400, 0);
            monster1Rect.sizeDelta = new Vector2(400, 400);
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(250, 400, 0);
            monster2Rect.sizeDelta = new Vector2(400, 400);
            monster3.SetActive(false);
            monster4.SetActive(false);
        }
        if (monsters.Count == 3)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-350, 400, 0);
            monster1Rect.sizeDelta = new Vector2(300, 300);
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(0, 400, 0);
            monster2Rect.sizeDelta = new Vector2(300, 300);
            monster3.SetActive(true);
            monster3Rect.anchoredPosition = new Vector3(350, 400, 0);
            monster3Rect.sizeDelta = new Vector2(300, 300);
            monster4.SetActive(false);
        }
        if (monsters.Count == 4)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-405, 400, 0);
            monster1Rect.sizeDelta = new Vector2(200, 200);
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(-135, 400, 0);
            monster2Rect.sizeDelta = new Vector2(200, 200);
            monster3.SetActive(true);
            monster3Rect.anchoredPosition = new Vector3(135, 400, 0);
            monster3Rect.sizeDelta = new Vector2(200, 200);
            monster4.SetActive(true);
            monster4Rect.anchoredPosition = new Vector3(405, 400, 0);
            monster4Rect.sizeDelta = new Vector2(200, 200);
        }
    }

    void boss()
    {
        test3.text = "몬스터 턴 시작"; // 보스들 행동 + 스킬은 여기서 전부 사용
        characterMgr.PlayerBloodDamage();//출혈 데미지
        turnNumber = 1;
        test2.text = "보스턴 진행중";
        test3.text = "몬스터 턴 끝";
        skillCount = 0;//스킬을 사용한 횟수 초기화
        totalDamage = 0;//스킬 데미지 초기화
        Invoke("battle", 1);
    }

}