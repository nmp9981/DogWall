using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill : MonoBehaviour
{
    Data_Manager dataManager;
    private DataManager Data;
    TeamSelect teamSelect;
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;

    // Start is called before the first frame update
    void Start()
    {
        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //몬스터->플레이어(몬스터 인덱스, 스킬번호를 받아서 진행)
    public int monsterSkillDamage(int mobIndex,int mobSkillNumber,int specialSkillNum,int playerNumber)//뒤에는 선택한 캐릭터 배열을 받아야함
    {
        //특수 스킬인가?
        if (specialSkillNum > 0)
        {
            return SpecialSkill(specialSkillNum);
        }
        int monsterAttackDamage = 0;//총 데미지
        int mobNum = teamSelect.selectedTeamNumber[mobIndex];//실제 몬스터 번호
        //HP회복
        HealMonsterHP(Data.saveData.MonsterSkillData[mobSkillNumber].HealHP,mobIndex,mobNum);

        int monsterAttribute = Data.saveData.MonsterData[mobNum].Attribute;//몬스터 속성
        int playerAttribute = Data.saveData.CharacterData[playerNumber].Attribute;//플레이어 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        int targets = Data.saveData.MonsterSkillData[mobSkillNumber].Targets;//몇명을 공격하는가?
        int attackDamage = Data.saveData.MonsterData[mobNum].Attack*Data.saveData.MonsterSkillData[mobSkillNumber].Attack;//데미지
        int countSkill = Data.saveData.MonsterSkillData[mobSkillNumber].AttackCount;//공격 횟수

        //일반 공격
        for(int i=0;i<targets; i++)//targets명을
        {
            for(int j = 0; j < countSkill; j++)//countSkill번
            {
                monsterAttackDamage += attackDamage;//공격
                //공격 텍스트 UI등장
            }
        }
        //하트 링크
        if (Data.saveData.MonsterSkillData[mobSkillNumber].HeartLink > 0)
        {
            Data.saveData.my_characterlist[playerNumber].heartLink = true;
        }
        else
        {
            Data.saveData.my_characterlist[playerNumber].heartLink = false;
        }
        //데스 링크
        if (Data.saveData.MonsterSkillData[mobSkillNumber].DeathLink > 0)
        {
            Data.saveData.my_characterlist[playerNumber].deathLink = true;
        }
        else
        {
            Data.saveData.my_characterlist[playerNumber].deathLink = false;
        }
        //턴 기반 버프
        //MonsterTurnBuff(SkillMonsterTurnMatrix[number, playerNumber], number,mobIndex);//남은 턴 수를 넣는다.

        return monsterAttackDamage;
    }
    //특수 스킬
    int SpecialSkill(int specialSkillNum)
    {
        int specialHitDamage = 0;//총 데미지
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
        return specialHitDamage;
    }
    //몬스터 HP회복
    void HealMonsterHP(int AmountMobHP,int mobIndex,int mobNum)
    {
        Mathf.Max(Data.saveData.MonsterData[mobNum].HP, monsterMgr.currentMonsterHP[mobIndex] + AmountMobHP);
    }
    //몬스터 턴 버프
    void MonsterTurnBuff()
    {

    }
}
