using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill : MonoBehaviour
{
    Data_Manager dataManager;
    private DataManager Data;
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;

    // Start is called before the first frame update
    void Start()
    {
        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //몬스터->플레이어(스킬번호를 받아서 진행)
    public int monsterSkillDamage(int mobSkillNumber,int specialSkillNum)
    {
        //특수 스킬인가?
        if (specialSkillNum > 0)
        {
            SpecialSkill(specialSkillNum);
            return 0;
        }
        int monsterAttackDamage = 0;
        /*
        //HP회복
        HealCharacterHP(Data.saveData.SkillData[number].HealHP);

        int playerNumber = number / 5;//플레이어 인덱스
        int playerAttribute = Data.saveData.CharacterData[playerNumber].Attribute;//플레이어 속성
        int monsterAttribute = Data.saveData.MonsterData[0].Attribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        playerAttack = Data.saveData.CharacterData[playerNumber].Attack;//캐릭터 초기 공격력
        int skillPercentDamage = Data.saveData.SkillData[number].Attack;//스킬 퍼센테이지

        //턴 기반 버프
        TurnBuff(SkillCharacterTurnMatrix[number, playerNumber], number,mobIndex);//남은 턴 수를 넣는다

        return playerAttack * skillPercentDamage * attributeDamage / 10000;//최종 데미지
         */
        return monsterAttackDamage;
    }
    //특수 스킬
    void SpecialSkill(int specialSkillNum)
    {
        switch (specialSkillNum)
        {
            case 1:
                //1번스킬
                break;
            case 2:
                //2번스킬
                break;
            case 3:
                //2번스킬
                break;
            case 4:
                //2번스킬
                break;
            case 5:
                //2번스킬
                break;
        }
    }
}
