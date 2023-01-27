using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SepcialMonster : MonoBehaviour
{
    Data_Manager dataManager;
    private DataManager Data;
    TeamSelect teamSelect;
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
    Skill skill;
    TextUI textUI;

    public int[,] MonsterSpecialSkillList = new int[4, 12];//몬스터 특수 스킬 목록
    // Start is called before the first frame update
    void Start()
    {
        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
        skill = GameObject.FindWithTag("Skill").GetComponent<Skill>();//Skill 스크립트에서 변수 가져오기
        textUI = GameObject.FindWithTag("DamageText").GetComponent<TextUI>();//TextUI 스크립트에서 변수 가져오
    }

    //몬스터 특수 스킬 세팅(턴수 저장 때문에 만들었다)
    public void monsterSpecialSkillSet()
    {
        for (int i = 0; i < monsterMgr.MonsterNum.Count; i++)//각 몬스터의 스킬을 배열에 넣기
        {
            int MobSkillIndex01 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn0_special1"];
            int MobSkillIndex02 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn0_special2"];
            int MobSkillIndex11 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn1_special1"];
            int MobSkillIndex12 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn1_special2"];
            int MobSkillIndex21 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn2_special1"];
            int MobSkillIndex22 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn2_special2"];
            int MobSkillIndex31 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn3_special1"];
            int MobSkillIndex32 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn3_special2"];
            //int MobSkillIndex41 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn4_special1"];
            //int MobSkillIndex42 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn4_special2"];
            //int MobSkillIndex51 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn5_special1"];
            //int MobSkillIndex52 = (int)monsterMgr.MonstersData[monsterMgr.MonsterNum[i]]["turn5_special2"];
            MonsterSpecialSkillList[i, 0] = MobSkillIndex01; MonsterSpecialSkillList[i, 1] = MobSkillIndex02;
            MonsterSpecialSkillList[i, 2] = MobSkillIndex11; MonsterSpecialSkillList[i, 3] = MobSkillIndex12;
            MonsterSpecialSkillList[i, 4] = MobSkillIndex21; MonsterSpecialSkillList[i, 5] = MobSkillIndex22;
            MonsterSpecialSkillList[i, 6] = MobSkillIndex31; MonsterSpecialSkillList[i, 7] = MobSkillIndex32;
            //MonsterSpecialSkillList[i, 8] = MobSkillIndex41; MonsterSpecialSkillList[i, 9] = MobSkillIndex42;
            //MonsterSpecialSkillList[i, 10] = MobSkillIndex51; MonsterSpecialSkillList[i, 11] = MobSkillIndex52;
        }
    }
    //특수 스킬
    public int SpecialSkill(int specialSkillNum)
    {
        int specialHitDamage = 0;//총 데미지
        
        return specialHitDamage;
    }
}
