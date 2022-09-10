using System.Collections;
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

    public int[,] SkillCharacterTurnMatrix = new int[160, 40];//스킬-캐릭터간 남은 턴 수 배열 
    public int playerAttack;//플레이어 공격력
    public Queue mobProvocation = new Queue();//몬스터 도발

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

    // Update is called once per frame
    void Update()
    {

    }
    //플레이어->몬스터 데미지
    public int skillAttackDamage(int number,int mobIndex)//스킬 번호만 받는다.
    {
        if (Data.saveData.SkillData[number].NotAction == true) return 0;//행동 불능의 경우 스킬 사용 불가

        //에너지 조건
        int requireEnerge = Data.saveData.SkillData[number].Energe;
        if (characterMgr.playerEnerge < requireEnerge) return 0;//스킬 사용 불가

        //HP회복
        HealCharacterHP(Data.saveData.SkillData[number].HealHP);

        int playerNumber = number / 5;//플레이어 인덱스
        int playerAttribute = Data.saveData.my_characterlist[playerNumber].Attribute;//플레이어 속성
        int monsterAttribute = Data.saveData.MonsterData[0].Attribute;//몬스터 속성
        int attributeDamage = characterMgr.CheckAttribute(playerAttribute, monsterAttribute);//속성 데미지

        playerAttack = Data.saveData.my_characterlist[playerNumber].ATK;//캐릭터 초기 공격력
        int skillPercentDamage = Data.saveData.SkillData[number].Attack;//스킬 퍼센테이지

        //턴 기반 버프
        TurnBuff(SkillCharacterTurnMatrix[number, playerNumber], number,mobIndex);//남은 턴 수를 넣는다

        int baseSkillDamage = playerAttack * skillPercentDamage / 100;//기본 스킬데미지
        int attributeSkillDamage = baseSkillDamage * attributeDamage / 100;//속성까지 
        return attributeSkillDamage*monsterDefense / 100;//최종 데미지
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
            SkillCharacterTurnMatrix[number, number / 4] = Data.saveData.SkillData[number].TurnCount;
        }
    }
    //턴 기반 버프
    void TurnBuff(int turnCount,int number,int mobIndex)//남은 턴 수, 위치
    {
        if (turnCount < 1) return;//턴 수를 모두 소모
        
        SkillCharacterTurnMatrix[number, number / 4] -= 1;//턴 횟수 소모

        //공격력
        playerAttack = playerAttack * Data.saveData.SkillData[number].Attack/100;
        //피격데미지
        monsterMgr.monsterAttackDamage = monsterMgr.monsterAttackDamage * Data.saveData.SkillData[number].DecreaseDamage / 100;
        //출혈(도트 데미지)
        monsterMgr.currentMonsterHP[mobIndex] = Mathf.Min(0, monsterMgr.currentMonsterHP[mobIndex] - Data.saveData.SkillData[number].blood);
        //도발
        if(Data.saveData.SkillData[number].Provocation == true)
        {
            //우선 피격 몬스터 설정
            mobProvocation.Enqueue(mobIndex);
        }
    }
    //남은 턴 수 나타내기
    public void TurnCountText(int number,int index)
    {
        switch (index)
        {
            case 0:
                if(SkillCharacterTurnMatrix[number, number / 5] > 0)
                {
                    characterMgr.characterCondition1.SetActive(true);
                    firstCharacterTurn.text = SkillCharacterTurnMatrix[number, number / 5].ToString();
                }
                else
                {
                    characterMgr.characterCondition1.SetActive(false);
                }
                break;
            case 1:
                if (SkillCharacterTurnMatrix[number, number / 5] > 0)
                {
                    characterMgr.characterCondition2.SetActive(true);
                    secondCharacterTurn.text = SkillCharacterTurnMatrix[number, number / 5].ToString();
                }
                else
                {
                    characterMgr.characterCondition2.SetActive(false);
                }
                break;
            case 2:
                if (SkillCharacterTurnMatrix[number, number / 5] > 0)
                {
                    characterMgr.characterCondition3.SetActive(true);
                    thirdCharacterTurn.text = SkillCharacterTurnMatrix[number, number / 5].ToString();
                }
                else
                {
                    characterMgr.characterCondition3.SetActive(false);
                }
                break;
            case 3:
                if (SkillCharacterTurnMatrix[number, number / 5] > 0)
                {
                    characterMgr.characterCondition4.SetActive(true);
                    fourthCharacterTurn.text = SkillCharacterTurnMatrix[number, number / 5].ToString();
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


