using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
//스킬 구조체 
public struct SkillInfo
{
    public int energe, attack;//에너지, 데미지
    string skillExplain;//설명

    public SkillInfo(int energe, int attack, string skillExplain)
    {
        this.energe = energe;
        this.attack = attack;
        this.skillExplain = skillExplain;
    }
}
*/
public class Skill : MonoBehaviour
{
    SaveDataClass saveDataClass;
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;

    public int[,] SkillCharacterTurnMatrix = new int[160, 40];//스킬-캐릭터간 남은 턴 수 배열 
    public int playerAttack;//플레이어 공격력

    //스킬데이터 추가
    void AddSkillData() {
        saveDataClass.skillList.Insert(0, new SkillDataClass {
            SkillName = "왕궁영술사",
            Energe = 2,
            Explain = "120%데미지로 공격",
            Attack = 120,
            DecreaseDamage = 100,
            HealHP = 0,
            Provocation = false,
            NotAction = false,
            HealEnerge = 0,
            blood = 0,
            TurnCount = 1,
            Rare = 1
        });
        saveDataClass.skillList.Insert(1, new SkillDataClass
        {
            SkillName = "왕궁영술사",
            Energe = -2,
            Explain = "90%데미지로 공격",
            Attack = 90,
            DecreaseDamage = 100,
            HealHP = 20,
            Provocation = false,
            NotAction = false,
            HealEnerge = 0,
            blood = 0,
            TurnCount = 3,
            Rare = 1
        });
        saveDataClass.skillList.Insert(2, new SkillDataClass
        {
            SkillName = "왕궁영술사",
            Energe = -3,
            Explain = "90%데미지로 공격",
            Attack = 90,
            DecreaseDamage = 70,
            HealHP = 0,
            Provocation = false,
            NotAction = false,
            HealEnerge = 0,
            blood = 0,
            TurnCount = 1,
            Rare = 1
        });
        saveDataClass.skillList.Insert(3, new SkillDataClass
        {
            SkillName = "왕궁영술사",
            Energe = -7,
            Explain = "220%데미지로 공격",
            Attack = 220,
            DecreaseDamage = 100,
            HealHP = 0,
            Provocation = false,
            NotAction = false,
            HealEnerge = 0,
            blood = 0,
            TurnCount = 1,
            Rare = 1
        }
        );
    }    
    /*
    public static List<SkillInfo> skillList = new List<SkillInfo>
    {
        new SkillInfo(8,150,"1-1단계"),
        new SkillInfo(17,250,"1-2단계"),
        new SkillInfo(21,300,"1-3단계"),
        new SkillInfo(35,550,"1-4단계"),
        new SkillInfo(9,160,"2-1단계"),
        new SkillInfo(17,250,"2-2단계"),
        new SkillInfo(21,300,"2-3단계"),
        new SkillInfo(35,550,"2-4단계"),
        new SkillInfo(10,170,"3-1단계"),
        new SkillInfo(17,250,"3-2단계"),
        new SkillInfo(21,300,"3-3단계"),
        new SkillInfo(35,550,"3-4단계"),
        new SkillInfo(11,180,"4-1단계"),
        new SkillInfo(17,250,"4-2단계"),
        new SkillInfo(21,300,"4-3단계"),
        new SkillInfo(35,550,"4-4단계"),
        new SkillInfo(12,190,"5-1단계"),
        new SkillInfo(17,250,"5-2단계"),
        new SkillInfo(21,300,"5-3단계"),
        new SkillInfo(35,550,"5-4단계"),
        new SkillInfo(13,200,"6-1단계"),
        new SkillInfo(17,250,"6-2단계"),
        new SkillInfo(21,300,"6-3단계"),
        new SkillInfo(35,550,"6-4단계"),
        new SkillInfo(14,210,"7-1단계"),
        new SkillInfo(17,250,"7-2단계"),
        new SkillInfo(21,300,"7-3단계"),
        new SkillInfo(35,550,"7-4단계"),
        new SkillInfo(15,220,"8-1단계"),
        new SkillInfo(17,250,"8-2단계"),
        new SkillInfo(21,300,"8-3단계"),
        new SkillInfo(35,550,"8-4단계")
    };
    */

    // Start is called before the first frame update
    void Start()
    {
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int skillAttackDamage(int number)//스킬 번호만 받는다.
    {
        if (saveDataClass.skillList[number].NotAction == true) return 0;//행동 불능의 경우 스킬 사용 불가

        //에너지 조건
        int requireEnerge = saveDataClass.skillList[number].Energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        //HP회복
        HealCharacterHP(saveDataClass.skillList[number].HealHP);
        //출혈은 지속데미지 인가?

        int playerAttribute = CharacterMgr.characterList[number/4].characterAttribute;//플레이어 속성
        int monsterAttribute = MonsterMgr.monsterList[0].monsterAttribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        playerAttack = CharacterMgr.characterList[number/4].characterAttack;//캐릭터 초기 공격력
        int skillPercentDamage = saveDataClass.skillList[number].Attack;//스킬 퍼센테이지

        //턴 기반 버프
        TurnBuff(SkillCharacterTurnMatrix[number, number / 4], number);//남은 턴 수를 넣는다

        return playerAttack * skillPercentDamage * attributeDamage / 10000;//최종 데미지
    }

    //플레이어 HP회복
    void HealCharacterHP(int amountHP)
    {
        Mathf.Min(characterMgr.playerHP + amountHP,characterMgr.playerFullHP);
    }
    //턴 초기화
    public void InitTurn(int number)
    {
        //턴 초기화
        if (SkillCharacterTurnMatrix[number, number / 4] == 0)
        {
            SkillCharacterTurnMatrix[number, number / 4] = saveDataClass.skillList[number].TurnCount;
        }
    }
    //턴 기반 버프
    void TurnBuff(int turnCount,int number)
    {
        if (turnCount < 1) return;//턴 수를 모두 소모
        SkillCharacterTurnMatrix[number, number / 4] -= 1;//턴 횟수 소모

        //공격력
        playerAttack = playerAttack * saveDataClass.skillList[number].Attack/100;
        //피격데미지
        monsterMgr.monsterAttackDamage = monsterMgr.monsterAttackDamage * saveDataClass.skillList[number].DecreaseDamage / 100;
        //출혈(도트 데미지)
        monsterMgr.monsterHP = Mathf.Min(0, monsterMgr.monsterHP - saveDataClass.skillList[number].blood);
        //도발
        if(saveDataClass.skillList[number].Provocation == true)
        {
            //우선 피격 몬스터 설정
        }
    }
}

//턴지속
public class CustomIEnumeratorType1 : CustomYieldInstruction{ 
    public override bool keepWaiting { 
        get { 
            return !Input.GetKeyDown(KeyCode.Space);
        } 
    } 
}


