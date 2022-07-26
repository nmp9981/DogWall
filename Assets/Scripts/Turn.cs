using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    public Text test; // 테스트용 텍스트(턴)
    public Text test2; // 테스트용 텍스트(스킬)


    public int turnNumber = 1; // 턴 확인용 변수
    public int skillNumber = 1;  // 스킬 확인용 변수

    bool skillAvailable = false; // 스킬 사용중인지 확인

    public bool firstAttack = true; // 임의로 정한 선제공격 확인용 변수

    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        test.text = "시작";
        test2.text = "대기중";
        skillAvailable = false;
        firstAttack = true;

        if (skillAvailable) turnNumber = 1; // 선제공격 여부 확인
        else turnNumber = 5;

        battle(); // 턴 시작
    }

    void battle() // 턴 관리 1, 2, 3, 4 - 플레이어 1, 2, 3 ,4    5 -  보스 턴 시작
    {
        skillNumber = 0;
        switch (turnNumber)
        {
            case 1: // 플레이어 턴 전체적인 시작 순서  battle > 스킬 버튼 클릭 > 스킬 사용 > battle
                test.text = "1";
                skillAvailable = true;
                test2.text = "스킬 사용 대기중";
                break;
            case 2:
                test.text = "2";
                skillAvailable = true;
                test2.text = "스킬 사용 대기중";
                break;
            case 3:
                test.text = "3";
                skillAvailable = true;
                test2.text = "스킬 사용 대기중";
                break;
            case 4:
                test.text = "4";
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


    void skill1()
    {
        skillAvailable = false;
        test2.text = "스킬1 사용";
        turnNumber += 1; // 턴 증가
        Invoke("battle", 1); // 스킬 사용후 턴 넘기기
    }
    void skill2()
    {
        skillAvailable = false;
        test2.text = "스킬2 사용";
        turnNumber += 1;
        Invoke("battle", 1);
    }
    void skill3()
    {
        skillAvailable = false;
        test2.text = "스킬3 사용";
        turnNumber += 1;
        Invoke("battle", 1);
    }
    void skill4()
    {
        skillAvailable = false;
        test2.text = "스킬4 사용";
        turnNumber += 1;
        Invoke("battle", 1);
    }

    public void skillNumberSet1() { if (skillAvailable) skill1(); } // 버튼 클릭시 함수 실행 
    public void skillNumberSet2() { if (skillAvailable) skill2(); }
    public void skillNumberSet3() { if (skillAvailable) skill3(); }
    public void skillNumberSet4() { if (skillAvailable) skill4(); }

    public void turnSkip()
    {
        if (skillAvailable)
        {
            skillAvailable = false;
            turnNumber += 1;
            test2.text = "턴 스킵";
            Invoke("battle", 1);
        }
    }


    void boss()
    {
        turnNumber = 1;
        test2.text = "보스턴 진행중";
        Invoke("battle", 1);
    }
}