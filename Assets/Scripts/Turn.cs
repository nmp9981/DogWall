using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    public Text test;
    public int turnNumber = 1;


    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        test.text = "시작";
        battle();
    }

    void battle()
    {
        switch(turnNumber)
        {
            case 1:
                Invoke("player1", 2);
                break;
            case 2:
                Invoke("player2", 2);
                break;
            case 3:
                Invoke("player3", 2);
                break;
            case 4:
                Invoke("player4", 2);
                break;
            case 5:
                Invoke("boss", 2);
                break;
            default:
                test.text = "버그발생";
                break;
        }
    }

    void player1()
    {
        turnNumber += 1;
        test.text = "1";
        battle();
    }

    void player2()
    {
        turnNumber += 1;
        test.text = "2";
        battle();
    }

    void player3()
    {
        turnNumber += 1;
        test.text = "3";
        battle();
    }

    void player4()
    {
        test.text = "4";
        characterMgr.PlayerBloodDamage();//출혈 데미지
        if (!characterMgr.IsPlayerDie())//플레이어 체력 체크
        {
            turnNumber += 1;
            battle();
        }
        else
        {
            test.text = "사망";//게임 오버 화면
            //홈화면으로
        }
    }

    void boss()
    {
        turnNumber = 1;
        test.text = "5";
        battle();
    }
}