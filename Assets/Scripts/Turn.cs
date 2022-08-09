using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Turn : MonoBehaviour
{
    Data_Manager dataManager;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    TeamSelect teamSelect;
    Skill skill;

    public Text characterNameText;//캐릭터 이름
    public Text characterAttackText;//캐릭터 공격력

    public Text turnText;
    public Text stageText;
    public Text monsterText;

    public int teamNumber;//팀원의 번호
    public int totalDamage;//스킬데미지 총합

    public GameObject skill_1E;
    public GameObject skill_2E;
    public GameObject skill_3E;
    public GameObject skill_4E;
    public GameObject skill_1T;
    public GameObject skill_2T;
    public GameObject skill_3T;
    public GameObject skill_4T;
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

    public GameObject story;

    public int turnNumber = 1; // 턴 확인용 변수
    public int totalTurnNumber = 1;
    public int skillNumber = 1;  // 스킬 확인용 변수
    public int stageNumber = 1; // 스테이지
    public int skillCount = 0;//스킬을 사용한 캐릭터의 수
    public int mobHP;//몬스터 HP

    public List<int> playerSkillSelect = new List<int>();
    public List<int> monsters = new List<int>();

    public bool skillAvailable = false; // 스킬 사용중인지 확인

    public bool firstAttack = true; // 임의로 정한 선제공격 확인용 변수

    public bool isClick;

    public bool longClickAvailable;

    void Start()
    {
        playerSkillSelect.Add(0);// 스킬 선택 초기화
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);
        playerSkillSelect.Add(0);

        monsters.Add(1);// 몬스터 추가
        monsters.Add(1);

        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기

        turnText.text = totalTurnNumber.ToString();
        stageText.text = stageNumber + "/3";

        firstAttack = true; // 선제공격 여부 임의로 설정

        if (firstAttack) turnNumber = 1; // 선제공격 여부 확인
        else turnNumber = 5;

        monsterSet(); // 몬스터 배치
        UISetting(); // 턴 시작
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------

    // 플레이어 초상화 선택시 턴 전환

    public void playerSelect1()
    {
        turnNumber = 1;
        teamNumber = teamSelect.selectedTeamNumber[0];
        UISetting();
    }
    public void playerSelect2()
    {
        turnNumber = 2;
        teamNumber = teamSelect.selectedTeamNumber[1];
        UISetting();
    }
    public void playerSelect3()
    {
        turnNumber = 3;
        teamNumber = teamSelect.selectedTeamNumber[2];
        UISetting();
    }
    public void playerSelect4()
    {
        turnNumber = 4;
        teamNumber = teamSelect.selectedTeamNumber[3];
        UISetting();
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------


    // 모든 스킬을 선택후 턴 종료시 진행

    public void turnEnd()
    {
        mobHP = dataManager.MonsterList[0].HP*100;//임의의 몬스터 HP 설정
        //스킬계산
        totalDamage = 0;//초기화
        //4개의 스킬
        for(int i = 0; i < 4; i++)
        {
            if (playerSkillSelect[i] == 4)//필살기
            {
                //필살기 애니메이션 재생
            }
            int skillNumber = teamSelect.selectedTeamNumber[i] * 4 + playerSkillSelect[i] - 1;//스킬의 번호
            skill.InitTurn(skillNumber);//턴 초기화
            characterMgr.ColorCondition(skill.playerAttack, i, skillNumber);//캐릭터 상태 이상 색상 표시
            skill.TurnCountText(skillNumber,i);//남은 턴 수 나타내기
            totalDamage += skill.skillAttackDamage(skillNumber);//데미지 누적
        }
        monsterMgr.MonsterBloodDamage(totalDamage,mobHP);//몬스터 데미지
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


        if (monsters.Count == 0) // 몬스터가 다 죽었다면 스테이지 증가
        {
            stageNumber += 1;
            stageText.text = stageNumber.ToString();
            if (firstAttack) turnNumber = 1; // 선제공격 여부 확인
            else turnNumber = 5;
        }

        turnNumber = 5; // 보스로 턴 전환

        Invoke("UISetting", 1);
    }

    //-----------------------------------------------------------------------------------------------------



    // 플레어이 초상화 클릭 & 스킬 선택시 그에 맞춰서 UI를 세팅하는 함수
    // 어떤 값을 수정했으면 꼭 실행 해줘야함

    void UISetting() // 턴 관리 1, 2, 3, 4 - 플레이어 1, 2, 3 ,4    5 -  몬스터 턴
    {

        characterNameText.text = CharacterMgr.characterList[teamNumber].characterName; // 캐릭터 공격력 & 이름 UI 표시
        characterAttackText.text = "Attack : " + CharacterMgr.characterList[teamNumber].characterAttack;


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

    void monsterSet() // 몬스터 배치
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

    void monster() // 몬스터 턴
    {
        characterMgr.PlayerBloodDamage();//출혈 데미지
        turnNumber = 1;
        skillCount = 0;//스킬을 사용한 횟수 초기화
        totalDamage = 0;//스킬 데미지 초기화

        if (monster1.activeSelf == true) // 몬스터가 살아있으면 턴을 진행
        {
            // monster1
            monsterText.text = "화염방사";
        }
        if (monster2.activeSelf == true)
        {
            // monster1
            monsterText.text = "백만볼트";
        }
        if (monster3.activeSelf == true)
        {
            // monster1
            monsterText.text = "칼춤";
        }
        if (monster4.activeSelf == true)
        {
            // monster1
            monsterText.text = "잠자기";
        }


    }

    // --------------------------------------------------------------


    // 스킬 & 스킵 버튼 관련 턴

    public void buttonDown1()
    {
        skillNumber = 1;
        skillAvailable = true;
        longClickAvailable = true;
        isClick = true;
        Invoke("longClick", 1f);
    }
    public void buttonDown2()
    {
        skillNumber = 2;
        skillAvailable = true;
        longClickAvailable = true;
        isClick = true;
        Invoke("longClick", 1f);
    }
    public void buttonDown3()
    {
        skillNumber = 3;
        skillAvailable = true;
        longClickAvailable = true;
        isClick = true;
        Invoke("longClick", 1f);
    }
    public void buttonDown4()
    {
        skillNumber = 4;
        skillAvailable = true;
        longClickAvailable = true;
        isClick = true;
        Invoke("longClick", 1f);
    }

    public void turnSkip()
    {
        skillNumber = 5;
        playerSkillSelect[turnNumber - 1] = skillNumber;
        UISetting();
    }

    public void buttonUp()
    {
        longClickAvailable = false;
        playerSkillSelect[turnNumber - 1] = skillNumber;;
        isClick = false;
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

    public void longClick()
    {
        if (longClickAvailable)
        {
            skillAvailable = false;
            if (isClick)
            {
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