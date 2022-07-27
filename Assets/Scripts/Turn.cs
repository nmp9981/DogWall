using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    public Text test; // 테스트용 텍스트(턴)
    public Text test2; // 테스트용 텍스트(스킬준비)
    public Text test3; // 테스트용 텍스트(스킬사용)
    public Text test4;

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
    public int skillNumber = 1;  // 스킬 확인용 변수

    public List<int> playerSkillSelect = new List<int>();
    public List<int> monsters = new List<int>();

    bool skillAvailable = false; // 스킬 사용중인지 확인

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
        monsters.Add(1);
        monsters.Add(1);

        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        test.text = "시작";
        test2.text = "대기중";
        test3.text = "테스트";
        test4.text = "몬스터수" + monsters.Count;
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
        battle();
    }
    public void playerSelect2()
    {
        turnNumber = 2;
        battle();
    }
    public void playerSelect3()
    {
        turnNumber = 3;
        battle();
    }
    public void playerSelect4()
    {
        turnNumber = 4;
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
                skillAvailable = true;
                if (playerSkillSelect[0] == 0) player_1E.SetActive(false);
                else player_1E.SetActive(true);
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
                characterMgr.PlayerBloodDamage();//출혈 데미지
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
            case 5:
                test.text = "5";
                boss();
                break;
            default:
                test.text = "버그발생";
                break;
        }
    }


    //oid skill1()

    //void skill2()

    //void skill3()

    //void skill4()

    public void skillNumberSet1() // 버튼 클릭시 함수 실행 
    {
        if (skillAvailable)
        {
            skillNumber = 1;
            playerSkillSelect[turnNumber - 1] = skillNumber;
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
            playerSkillSelect[turnNumber - 1] = skillNumber;
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
            playerSkillSelect[turnNumber - 1] = skillNumber;
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
            playerSkillSelect[turnNumber - 1] = skillNumber;
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
        test3.text = "스킬들 사용"; // 턴종료후 스킬 사용
        
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
        Invoke("battle", 1);
    }

    // 몬스터 관련 턴 ------------------------------------

    void monsterSet()
    {
        if(monsters.Count == 1)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(0, 400, 0);
            monster1Rect.sizeDelta = new Vector2(700, 700);
        }
        if(monsters.Count == 2)
        {
            monster1.SetActive(true);
            monster1Rect.anchoredPosition = new Vector3(-250, 400, 0);
            monster1Rect.sizeDelta = new Vector2(400, 400);
            monster2.SetActive(true);
            monster2Rect.anchoredPosition = new Vector3(250, 400, 0);
            monster2Rect.sizeDelta = new Vector2(400, 400);
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
        test3.text = "몬스터 턴 시작";
        turnNumber = 1;
        test2.text = "보스턴 진행중";
        test2.text = "몬스터 턴 끝";
        Invoke("battle", 1);
    }

}