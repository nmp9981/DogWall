using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Skill : MonoBehaviour
{
    Turn turn;
    CharacterMgr characterMgr;
    MonsterMgr monsterMgr;
   
    //스킬 배열 선언 
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
    public int skill1(int number)
    {
        int requireEnerge = skillList[4 * number].energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        characterMgr.playerEnerge = Mathf.Max(0, characterMgr.playerEnerge - requireEnerge);//에너지 소비

        int playerAttribute = CharacterMgr.characterList[number].characterAttribute;//플레이어 속성
        int monsterAttribute = MonsterMgr.monsterList[0].monsterAttribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        int playerAttack = CharacterMgr.characterList[number].characterAttack;//캐릭터 공격력
        int skillPercentDamage = skillList[4*number].attack;//스킬 퍼센테이지
        return playerAttack * skillPercentDamage * attributeDamage / 10000;//최종 데미지
    }
    public int skill2(int number)
    {
        int requireEnerge = skillList[4 * number+1].energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        characterMgr.playerEnerge = Mathf.Max(0, characterMgr.playerEnerge - skillList[4 * number+1].energe);//에너지 소비

        int playerAttribute = CharacterMgr.characterList[number].characterAttribute;//플레이어 속성
        int monsterAttribute = MonsterMgr.monsterList[0].monsterAttribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        int playerAttack = CharacterMgr.characterList[number].characterAttack;//캐릭터 공격력
        int skillPercentDamage = skillList[4 * number+1].attack;//스킬 퍼센테이지
        return playerAttack * skillPercentDamage * attributeDamage / 10000;//최종 데미지
    }
    public int skill3(int number)
    {
        int requireEnerge = skillList[4 * number+2].energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        characterMgr.playerEnerge = Mathf.Max(0, characterMgr.playerEnerge - skillList[4 * number+2].energe);//에너지 소비

        int playerAttribute = CharacterMgr.characterList[number].characterAttribute;//플레이어 속성
        int monsterAttribute = MonsterMgr.monsterList[0].monsterAttribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        int playerAttack = CharacterMgr.characterList[number].characterAttack;//캐릭터 공격력
        int skillPercentDamage = skillList[4 * number+2].attack;//스킬 퍼센테이지
        return playerAttack * skillPercentDamage * attributeDamage/10000;//최종 데미지
    }
    public int skill4(int number)
    {
        int requireEnerge = skillList[4 * number+3].energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        characterMgr.playerEnerge = Mathf.Max(0, characterMgr.playerEnerge - skillList[4 * number+3].energe);//에너지 소비

        int playerAttribute = CharacterMgr.characterList[number].characterAttribute;//플레이어 속성
        int monsterAttribute = MonsterMgr.monsterList[0].monsterAttribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        int playerAttack = CharacterMgr.characterList[number].characterAttack;//캐릭터 공격력
        int skillPercentDamage = skillList[4 * number+3].attack;//스킬 퍼센테이지
        return playerAttack * skillPercentDamage * attributeDamage / 10000;//최종 데미지
    }
}
