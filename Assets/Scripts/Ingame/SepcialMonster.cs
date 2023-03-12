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
    int specialHitRatio = 100;//총 데미지 비율
    public int[,] MonsterSpecialSkillList = new int[CharacterNum, 12];//몬스터 특수 스킬 목록
    int[,] MobSpecialSkillTurn = new int[CharacterNum, 6];//턴 (행동불가,도발, 데스링크, 공격감소, 방어)

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
            int MobSkillIndex01 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn0_special1].TurnCount;
            int MobSkillIndex02 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn0_special2].TurnCount;
            int MobSkillIndex11 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn1_special1].TurnCount;
            int MobSkillIndex12 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn1_special2].TurnCount;
            int MobSkillIndex21 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn2_special1].TurnCount;
            int MobSkillIndex22 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn2_special2].TurnCount;
            int MobSkillIndex31 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn3_special1].TurnCount;
            int MobSkillIndex32 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn3_special2].TurnCount;
            int MobSkillIndex41 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn4_special1].TurnCount;
            int MobSkillIndex42 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn4_special2].TurnCount;
            int MobSkillIndex51 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn5_special1].TurnCount;
            int MobSkillIndex52 = Data.saveData.MonsterSpecialSkill[Data.saveData.MonsterData[i].turn5_special2].TurnCount;
            MonsterSpecialSkillList[i, 0] = MobSkillIndex01; MonsterSpecialSkillList[i, 1] = MobSkillIndex02;
            MonsterSpecialSkillList[i, 2] = MobSkillIndex11; MonsterSpecialSkillList[i, 3] = MobSkillIndex12;
            MonsterSpecialSkillList[i, 4] = MobSkillIndex21; MonsterSpecialSkillList[i, 5] = MobSkillIndex22;
            MonsterSpecialSkillList[i, 6] = MobSkillIndex31; MonsterSpecialSkillList[i, 7] = MobSkillIndex32;
            MonsterSpecialSkillList[i, 8] = MobSkillIndex41; MonsterSpecialSkillList[i, 9] = MobSkillIndex42;
            MonsterSpecialSkillList[i, 10] = MobSkillIndex51; MonsterSpecialSkillList[i, 11] = MobSkillIndex52;
        }
    }
    //특수 스킬
    public int SpecialSkill(int mobIndex,int specialSkillNum)
    {
        int targets = Data.saveData.MonsterSpecialSkill[specialSkillNum].Targets;//타겟 수
        int turnCounts = Data.saveData.MonsterSpecialSkill[specialSkillNum].TurnCount;//턴 수
        //자신에게 씀
        if (Data.saveData.MonsterSpecialSkill[specialSkillNum].Self == true)
        {
            SpecialSkillSelf(mobIndex,specialSkillNum,turnCounts);
            turnDcrease(mobIndex);//턴 감소
            return specialHitRatio;
        }

        //플레이어 에너지
        characterMgr.playerEnerge = Mathf.Max(Mathf.Min(characterMgr.playerFullEnerge, characterMgr.playerEnerge + Data.saveData.MonsterSpecialSkill[specialSkillNum].Energy),0);

        //행동불가
        if(Data.saveData.MonsterSpecialSkill[specialSkillNum].NotAction == 1)
        {
            int selecCharac = 0;//선택한 캐럭터 수
            bool[] isSelec = { false, false, false, false, false };//캐릭터 선택 여부
            while (selecCharac < targets)
            {
                int i = Random.Range(0, CharacterNum);
                if (isSelec[i] == false && MobSpecialSkillTurn[i,0]==0)//미선택 & 행동불가가 아닐때
                {
                    isSelec[i] = true;
                    selecCharac++;
                    Data.saveData.CharacterSkillIndex[i].NotAction = true;//해당 캐릭터는 행동불가(코드 수정 예정)
                    MobSpecialSkillTurn[i,0] = turnCounts;//해당 턴 만큼 행동불가
                }
            }
        }
        
        //공격력
        if (Data.saveData.MonsterSpecialSkill[specialSkillNum].CharacterAttack !=100)
        {
            int selecCharac = 0;//선택한 캐럭터 수
            bool[] isSelec = { false, false, false, false, false };//캐릭터 선택 여부
            while (selecCharac < targets)
            {
                int i = Random.Range(0, CharacterNum);
                if (isSelec[i] == false && MobSpecialSkillTurn[i, 4] == 0)//미선택 & 비활성화
                {
                    isSelec[i] = true;
                    selecCharac++;
                    characterMgr.playerAttack[i] = (characterMgr.playerAttack[i] * Data.saveData.MonsterSpecialSkill[specialSkillNum] .CharacterAttack)/ 100;
                    MobSpecialSkillTurn[i, 4] = turnCounts;//해당 턴 만큼 공격력 증감
                }
            }
        }
        //방어
        if (Data.saveData.MonsterSpecialSkill[specialSkillNum].DecreaseDamage != 100)
        {
            for (int i = 0; i < CharacterNum; i++)
            {
                if (MobSpecialSkillTurn[i, 5] == 0)//미선택 & 비활성화
                {
                    specialHitRatio = Data.saveData.MonsterSpecialSkill[specialSkillNum].DecreaseDamage;
                    MobSpecialSkillTurn[i, 5] = turnCounts;//해당 턴 만큼 방어력 증감
                }
            }
        }
        //데스링크
        if(Data.saveData.MonsterSpecialSkill[specialSkillNum].Deathlink > 0)
        {
            int DeathLinkDamage = Data.saveData.MonsterSpecialSkill[specialSkillNum].Deathlink;
            int targerCount = Data.saveData.MonsterSpecialSkill[specialSkillNum].Targets;
            CharacterDeathLink(DeathLinkDamage,targerCount);//캐릭터 데스링크
        }
        turnDcrease(mobIndex);//턴 감소
        return specialHitRatio;
    }

    //자신에게 씀
    public void SpecialSkillSelf(int mobIndex,int specialSkillNum, int turnCounts)
    {
        if (monsterMgr.currentMonsterHP[mobIndex] <= 0) return; //남은 몬스터 HP가 0보다 클 경우만
        //도발
        if (Data.saveData.MonsterSpecialSkill[specialSkillNum].Provocation == true)
        {
            MobSpecialSkillTurn[mobIndex, 1] = turnCounts;//해당 턴 만큼 도발
            skill.mobProvocation[mobIndex] = true;
        }
        //자해
        if (Data.saveData.MonsterSpecialSkill[specialSkillNum].Self == true)
        {
            int selfDiss = Data.saveData.MonsterData[mobIndex].Atk;//자해량
            monsterMgr.currentMonsterHP[mobIndex] = Mathf.Max(0, monsterMgr.currentMonsterHP[mobIndex] - monsterMgr.monsterFullHP[mobIndex] * selfDiss/100);
        }
    }
    //남은 턴 감소
    void turnDcrease(int mobIndex)
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
                        Data.saveData.CharacterSkillIndex[i].NotAction = false;//해당 캐릭터는 행동가능(코드 수정 예정)
                    }
                    else if (j == 1)
                    {
                        skill.mobProvocation[mobIndex] = false;//도발 해제
                    }
                    else if (j == 2)
                    {
                        //타겟 이미지 해제
                    }
                    else if (j == 3)
                    {
                        characterMgr.playerAttack[i] = Data.saveData.CharacterData[i].ATK;//원래 공격력
                    }
                    else if (j == 4)
                    {
                        specialHitRatio = 100;
                    }
                }
            }
        }
    }
    //캐릭터 데스링크
    void CharacterDeathLink(int damage,int targetCount)
    {
        //타겟 지정
        int selectedCount = 0;//총 선택 개수
        bool[] selectTarget = new bool[CharacterNum];

        while (selectedCount < targetCount)
        {
            int num = Random.Range(0, CharacterNum);//번호 결정
            if (selectTarget[num] == false)//아직 미선택
            {
                selectTarget[num] = true;//선택 체크
                selectedCount++;
            }
        }
        //타켓이미지 띄우기
        for(int i = 0; i < CharacterNum; i++)
        {
            if (selectTarget[i] == true)
            {
                //이미지 띄우기
            }
        }
        //데미지
        characterMgr.playerHP = Mathf.Max(0, characterMgr.playerHP - damage);
    }
}

