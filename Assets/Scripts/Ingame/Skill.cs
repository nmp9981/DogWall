﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    //SaveDataClass saveDataClass;
    Data_Manager dataManager;
    private DataManager Data;
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    MonsterSkill monsterSkill;

    public int playerAttack;//캐릭터 공격력
    public int[,] SkillCharacterTurnMatrix = new int[250, 4];//스킬-캐릭터간 남은 턴 수 배열 
    public bool[] mobProvocation = new bool[4];//몬스터 도발

    //캐릭터별 남은 턴 수
    public Text firstCharacterTurn;
    public Text secondCharacterTurn;
    public Text thirdCharacterTurn;
    public Text fourthCharacterTurn;

    //몬스터 방어력
    public int monsterDefense = 100;

// Start is called before the first frame update
void Start()
    {
        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
        monsterSkill = GameObject.FindWithTag("MonsterSkill").GetComponent<MonsterSkill>();//MonsterMgr 스크립트에서 변수 가져오기
    }

    //플레이어->몬스터 데미지
    public int skillAttackDamage(int number,int playerNumber,int playIndex, int mobIndex)//스킬 번호, 사용 캐릭터,캐릭터 인덱스, 대상 몬스터
    {
        if (Data.saveData.CharacterSkillIndex[number].NotAction == true) return 0;//행동 불능의 경우 스킬 사용 불가
       
        //에너지 조건
        int requireEnerge = Data.saveData.CharacterSkillIndex[number].Energy;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        int playerAttribute = Data.saveData.CharacterData[playerNumber].Attribute;//플레이어 속성
        int monsterAttribute = Data.saveData.MonsterData[0].Type;//몬스터 속성
        monsterDefense = 100;//몬스터 방어력
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지
        playerAttack = characterMgr.playerAttack[playIndex];//캐릭터 초기 공격력
        int skillPercentDamage = Data.saveData.CharacterSkillIndex[number].Attack;//스킬 퍼센테이지

        //전체 공격 여부
        if (Data.saveData.CharacterSkillIndex[number].AllTargets == true) turn.isAllTarget = 1;
        //턴 기반 버프
        TurnBuff(playIndex, number,mobIndex);//남은 턴 수를 넣는다
        
        int baseSkillDamage = (playerAttack * skillPercentDamage) / 100;//기본 스킬데미지
        int attributeSkillDamage = (baseSkillDamage * attributeDamage) / 100;//속성까지 
        int finalDamage = (attributeSkillDamage * monsterDefense) / 100;//최종 데미지
        return finalDamage*10;
    }

    //플레이어 HP회복
    void HealCharacterHP(int amountHP)
    {
        characterMgr.playerHP = Mathf.Max(Mathf.Min(characterMgr.playerHP + amountHP,characterMgr.playerFullHP),0);
    }
    //캐릭터 에너지 증감
    void CharacterSkillEnerge(int amountEnerge)
    {
        characterMgr.playerEnerge = Mathf.Max(Mathf.Min(characterMgr.playerEnerge + amountEnerge, characterMgr.playerFullEnerge),0);
    }
    //턴 초기화
    public void InitTurn(int number,int characterIndex)
    {
        //턴 초기화
        if (SkillCharacterTurnMatrix[number, characterIndex] == 0)
        {
            SkillCharacterTurnMatrix[number, characterIndex] = Data.saveData.CharacterSkillIndex[number].TurnCount;
        }
    }
    //턴 기반 버프
    void TurnBuff(int playIndex,int number,int mobIndex)//남은 턴 수, 위치
    {
        int turnCount = SkillCharacterTurnMatrix[number, playIndex];//남은 턴 수
        if (turnCount < 1) return;//턴 수를 모두 소모
        
        SkillCharacterTurnMatrix[number, playIndex] -= 1;//턴 횟수 소모

        //에너지 증가
        CharacterSkillEnerge(Data.saveData.CharacterSkillIndex[number].Energy);
        //HP증가
        HealCharacterHP(Data.saveData.CharacterSkillIndex[number].HealHP);
        //공격력
        playerAttack = playerAttack * Data.saveData.CharacterSkillIndex[number].CharacterAttack/100;
        //피격데미지
        monsterMgr.monsterAttackDamage = monsterMgr.monsterAttackDamage * Data.saveData.CharacterSkillIndex[number].DecreaseDamage / 100;
        //출혈(도트 데미지)
        monsterMgr.currentMonsterHP[mobIndex] = Mathf.Max(0, monsterMgr.currentMonsterHP[mobIndex] - Data.saveData.CharacterSkillIndex[number].Blood);
    }
    //남은 턴 수 나타내기
    public void TurnCountText(int number,int index)
    {
        switch (index)
        {
            case 0:
                if(SkillCharacterTurnMatrix[number, index] > 0)
                {
                    characterMgr.characterCondition1.SetActive(true);
                    firstCharacterTurn.text = SkillCharacterTurnMatrix[number, index].ToString();
                }
                else
                {
                    characterMgr.characterCondition1.SetActive(false);
                }
                break;
            case 1:
                if (SkillCharacterTurnMatrix[number, index] > 0)
                {
                    characterMgr.characterCondition2.SetActive(true);
                    secondCharacterTurn.text = SkillCharacterTurnMatrix[number, index].ToString();
                }
                else
                {
                    characterMgr.characterCondition2.SetActive(false);
                }
                break;
            case 2:
                if (SkillCharacterTurnMatrix[number, index] > 0)
                {
                    characterMgr.characterCondition3.SetActive(true);
                    thirdCharacterTurn.text = SkillCharacterTurnMatrix[number, index].ToString();
                }
                else
                {
                    characterMgr.characterCondition3.SetActive(false);
                }
                break;
            case 3:
                if (SkillCharacterTurnMatrix[number, index] > 0)
                {
                    characterMgr.characterCondition4.SetActive(true);
                    fourthCharacterTurn.text = SkillCharacterTurnMatrix[number, index].ToString();
                }
                else
                {
                    characterMgr.characterCondition4.SetActive(false);
                }
                break;
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
