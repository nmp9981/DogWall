using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//몬스터 정보
public struct MonsterInfo
{
    public int monsterHP, monsterAttack;

    public MonsterInfo(int monsterHP, int monsterAttack)
    {
        this.monsterHP = monsterHP;
        this.monsterAttack = monsterAttack;
    }
}

public class MonsterMgr : MonoBehaviour
{
    //몬스터 배열 선언 
    static List<MonsterInfo> monsterList = new List<MonsterInfo>
    {
        new MonsterInfo(1000000,2700),
        new MonsterInfo(1300000,3200),
        new MonsterInfo(2200000,4000)
    };

    CharacterMgr characterMgr;

    public int monsterHP;//몬스터 체력
    bool isMonsterBlood = true;//몬스터 출혈 여부
    public int monsterAttackDamage;//몬스터 공격 데미지

    // Start is called before the first frame update
    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterHP = monsterList[0].monsterHP;//몬스터 체력 초기화
        monsterAttackDamage = monsterList[0].monsterAttack;//몬스터 공격력
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //출혈 데미지 계산
    public void MonsterBloodDamage()
    {
        if (isMonsterBlood)
        {
            int hitDamage = characterMgr.playerAttack;
            monsterHP = Mathf.Max(monsterHP - hitDamage, 0);
        }
    } 
    //몬스터가 죽었는가?
    public bool IsMonsterDie()
    {
        if (monsterHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
