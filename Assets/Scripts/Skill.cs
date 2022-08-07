using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //SaveDataClass saveDataClass;
    Data_Manager dataManager;
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;

    public int[,] SkillCharacterTurnMatrix = new int[160, 40];//스킬-캐릭터간 남은 턴 수 배열 
    public int playerAttack;//플레이어 공격력

    //스킬데이터 추가
    void AddSkillData() {
        dataManager.SkillList.Insert(0, new SkillDataClass {
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
        dataManager.SkillList.Insert(1, new SkillDataClass
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
        dataManager.SkillList.Insert(2, new SkillDataClass
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
        dataManager.SkillList.Insert(3, new SkillDataClass
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
        dataManager.SkillList.Insert(4, new SkillDataClass
        {
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
        dataManager.SkillList.Insert(5, new SkillDataClass
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
        dataManager.SkillList.Insert(6, new SkillDataClass
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
        dataManager.SkillList.Insert(7, new SkillDataClass
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
        dataManager.SkillList.Insert(8, new SkillDataClass
        {
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
        dataManager.SkillList.Insert(9, new SkillDataClass
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
        dataManager.SkillList.Insert(10, new SkillDataClass
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
        dataManager.SkillList.Insert(11, new SkillDataClass
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
        dataManager.SkillList.Insert(12, new SkillDataClass
        {
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
        dataManager.SkillList.Insert(13, new SkillDataClass
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
        dataManager.SkillList.Insert(14, new SkillDataClass
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
        dataManager.Add_Skill("왕궁영술사", -7, "220%데미지로 공격", 220, 100, 0, false, false, 0, 0, 1, 1);
        dataManager.SkillList.Insert(16, new SkillDataClass
        {
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
        dataManager.SkillList.Insert(17, new SkillDataClass
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
        dataManager.SkillList.Insert(18, new SkillDataClass
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
        dataManager.Add_Skill("왕궁영술사", -7, "220%데미지로 공격", 220, 100, 0, false, false, 0, 0, 1, 1);
        dataManager.Add_Skill("망령", -4, "A", 130, 100, 0, false, false, 0, 0, 1, 1);
        dataManager.Add_Skill("망령", -4, "A", 130, 100, 0, false, false, 0, 0, 1, 1);
        dataManager.Add_Skill("망령", -4, "A", 130, 100, 0, false, false, 0, 0, 1, 1);
        dataManager.Add_Skill("망령", -4, "A", 130, 100, 0, false, false, 0, 0, 1, 1);
    }


// Start is called before the first frame update
void Start()
    {
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
        AddSkillData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int skillAttackDamage(int number)//스킬 번호만 받는다.
    {
        if (dataManager.SkillList[number].NotAction == true) return 0;//행동 불능의 경우 스킬 사용 불가

        //에너지 조건
        int requireEnerge = dataManager.SkillList[number].Energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        //HP회복
        HealCharacterHP(dataManager.SkillList[number].HealHP);
        //출혈은 지속데미지 인가?

        int playerAttribute = CharacterMgr.characterList[number/4].characterAttribute;//플레이어 속성
        int monsterAttribute = MonsterMgr.monsterList[0].monsterAttribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        playerAttack = CharacterMgr.characterList[number/4].characterAttack;//캐릭터 초기 공격력
        int skillPercentDamage = dataManager.SkillList[number].Attack;//스킬 퍼센테이지

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
            SkillCharacterTurnMatrix[number, number / 4] = dataManager.SkillList[number].TurnCount;
        }
    }
    //턴 기반 버프
    void TurnBuff(int turnCount,int number)
    {
        if (turnCount < 1) return;//턴 수를 모두 소모
        SkillCharacterTurnMatrix[number, number / 4] -= 1;//턴 횟수 소모

        //공격력
        playerAttack = playerAttack * dataManager.SkillList[number].Attack/100;
        //피격데미지
        monsterMgr.monsterAttackDamage = monsterMgr.monsterAttackDamage * dataManager.SkillList[number].DecreaseDamage / 100;
        //출혈(도트 데미지)
        monsterMgr.currentMonsterHP = Mathf.Min(0, monsterMgr.currentMonsterHP - dataManager.SkillList[number].blood);
        //도발
        if(dataManager.SkillList[number].Provocation == true)
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


