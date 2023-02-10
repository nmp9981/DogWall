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

    const int CharacterNum = 4;
    public int[,] MonsterSpecialSkillList = new int[CharacterNum, 12];//몬스터 특수 스킬 목록
    int[,] MobSpecialSkillTurn = new int[CharacterNum, 5];//턴 (행동불가,도발, 데스링크, 공격감소, 방어)

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
        //자신에게 씀
        if ((int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["Self"] == 1)
        {
            SpecialSkillSelf(specialSkillNum);
            return 0;
        }

        int specialHitDamage = 0;//총 데미지
        int targets = (int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["Targets"];//타겟 수
        int turnCounts = (int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["TurnCount"];//턴 수
        //플레이어 에너지
        characterMgr.playerEnerge = Mathf.Max(Mathf.Min(characterMgr.playerFullEnerge, characterMgr.playerEnerge + (int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["Energy"]),0);

        //행동불가
        if((int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["NotAction"] == 1)
        {
            int selecCharac = 0;//선택한 캐럭터 수
            bool[] isSelec = { false, false, false, false, false };//캐릭터 선택 여부
            while (selecCharac < targets)
            {
                int i = Random.Range(0, CharacterNum);
                if (isSelec[i] == false && MobSpecialSkillTurn[0, i]==0)//미선택 & 행동불가가 아닐때
                {
                    isSelec[i] = true;
                    selecCharac++;
                    Data.saveData.SkillData[i].NotAction = true;//해당 캐릭터는 행동불가(코드 수정 예정)
                    MobSpecialSkillTurn[0, i] = turnCounts;//해당 턴 만큼 행동불가
                }
            }
        }
        //공격력
        if ((int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["CharacterAttack"] !=100)
        {
            int selecCharac = 0;//선택한 캐럭터 수
            bool[] isSelec = { false, false, false, false, false };//캐릭터 선택 여부
            while (selecCharac < targets)
            {
                int i = Random.Range(0, CharacterNum);
                if (isSelec[i] == false && MobSpecialSkillTurn[0, 4] == 0)//미선택 & 비활성화
                {
                    isSelec[i] = true;
                    selecCharac++;
                    Data.saveData.my_characterlist[i].ATK = Data.saveData.CharacterData[i].Attack*(int)monsterMgr.MonsterSkillSpecial[specialSkillNum]["CharacterAttack"]/100;//해당 캐릭터는 행동불가(코드 수정 예정)
                    MobSpecialSkillTurn[4, i] = turnCounts;//해당 턴 만큼 공격력 증감
                }
            }
        }
        //방어

        turnDcrease();//턴 감소
        return specialHitDamage;
    }

    //자신에게 씀
    public void SpecialSkillSelf(int specialSkillNum)
    {

    }
    //남은 턴 감소
    void turnDcrease()
    {
        for(int i=0;i< CharacterNum; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                MobSpecialSkillTurn[i, j] = Mathf.Max(0, MobSpecialSkillTurn[i, j] - 1);//1턴씩 감소
                if(MobSpecialSkillTurn[i, j] == 0)//상태 해제
                {
                    if (j == 0)
                    {
                        Data.saveData.SkillData[i].NotAction = false;//해당 캐릭터는 행동가능(코드 수정 예정)
                    }
                    if (j == 4)
                    {
                        Data.saveData.my_characterlist[i].ATK = Data.saveData.CharacterData[i].Attack;//원래 공격력
                    }
                }
            }
        }
    }
}

