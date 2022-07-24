using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthPlayer : MonoBehaviour
{
    CharacterMgr chracterMgr;
    MonsterMgr monsterMgr;
    // Start is called before the first frame update
    void Start()
    {
        chracterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
    }

    public void Skill1()
    {
        chracterMgr.playerEnerge -= 10;
        monsterMgr.monsterHP -= 1000;
    }
    public void Skill2()
    {
        chracterMgr.playerEnerge -= 20;
        monsterMgr.monsterHP -= 2000;
    }
    public void Skill3()
    {
        chracterMgr.playerEnerge -= 30;
        monsterMgr.monsterHP -= 5000;
    }
    public void Skill4()
    {
        chracterMgr.playerEnerge -= 40;
        monsterMgr.monsterHP -= 10000;
    }
}
