using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurn : MonoBehaviour
{
    [SerializeField]
    Skill skill;
    [SerializeField]
    Stage stage;
    [SerializeField]
    PlayerSelect playerSelect;
    [SerializeField]
    UISetting UISetting;

    void Start()
    {
        if (DataManager.singleTon.saveData.inGameData.playerData.Count == 0)
        {
            DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[0].idx]);
            DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[1].idx]);
            DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[2].idx]);
            DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[3].idx]);
        } // 데이터가 없으면 임시 데이터 추가 (테스트용)

        DataManager.singleTon.saveData.inGameData.monsterTaunt = new Buff[4];
        DataManager.singleTon.saveData.inGameData.stage = 1;
        DataManager.singleTon.saveData.inGameData.turn = 1;

        StageSetting();
        UISetting.MonsterLocationUISetting();
        PlayerTurnStartSetting();
    }

    public void PlayerTurnStartSetting() // 플레이어 턴 시작시 기본 세팅
    {
        DataManager.singleTon.saveData.inGameData.isTurn = true; // 플레이어 턴이 끝났을때 클릭되는 것을 방지
        DataManager.singleTon.saveData.inGameData.isEffect = false; // 이펙트가 표시중인가를 확인
        DataManager.singleTon.saveData.inGameData.selectPlayer = 0; // 선택된 플레이어 초기화 (디폴트값 1번째)

        DataManager.singleTon.saveData.inGameData.selectSkill.Clear();
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4);
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4);
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4);
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4); // 스킬 선택 초기화 (디폴트값 스킬 안씀)

        DataManager.singleTon.saveData.inGameData.targetMonster.Clear();
        DataManager.singleTon.saveData.inGameData.targetMonster = new List<int> { 0, 0, 0, 0};

        for(int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData.Count; i++)
        {
            DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i] = new List<Buff>();
            DataManager.singleTon.saveData.inGameData.playerDamageBuff[i] = new List<Buff>();
            DataManager.singleTon.saveData.inGameData.playerBlood[i] = new List<Buff>();
        }
        for (int i = 0; i < DataManager.singleTon.saveData.inGameData.crruentMonster.Count; i++)
        {
            DataManager.singleTon.saveData.inGameData.monsterDefenceBuff[i] = new List<Buff>();
            DataManager.singleTon.saveData.inGameData.monsterDamageBuff[i] = new List<Buff>();
            DataManager.singleTon.saveData.inGameData.monsterBlood[i] = new List<Buff>();
        }

        /*
        for(int i = 0; i < 4; i++)
        if (DataManager.singleTon.saveData.inGameData.monsterTaunt.Count == 1)
        {
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]);
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]);
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]);
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]); // 도발한 몬스터가 한명이라면 그 몬스터로 고정
        }
        else if (DataManager.singleTon.saveData.inGameData.monsterTaunt.Count > 1)
        {
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]);
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]);
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]);
            //DataManager.singleTon.saveData.inGameData.targetMonster.Add(DataManager.singleTon.saveData.inGameData.monsterTaunt[0]); // 도발한 몬스터가 두명 이상이라면 일단 다음 몬스터로 고정
        }
        else
        {
            DataManager.singleTon.saveData.inGameData.targetMonster.Add(0);
            DataManager.singleTon.saveData.inGameData.targetMonster.Add(0);
            DataManager.singleTon.saveData.inGameData.targetMonster.Add(0);
            DataManager.singleTon.saveData.inGameData.targetMonster.Add(0); // 적 선택 초기화 (디폴트값 첫번째 몬스터)
        }
        */

        playerSelect.UISetting.PlayerUISetting();
        playerSelect.UISetting.MonsterUISetting();
        playerSelect.UISetting.StageUISetting();
    }

    public void PlayerTurnEnd() // 턴 종료 버튼을 누를시 시작
    {
        if (playerSelect.CheckTurn()) return;
        DataManager.singleTon.saveData.inGameData.isTurn = false;
        StartCoroutine(skill.PlayerSkill());
    }

    public void StageSetting()
    {
        DataManager.singleTon.saveData.inGameData.crruentMonster.Clear();
        DataManager.singleTon.saveData.inGameData.monsterCount = 0;
        foreach (MonstersDataClass monster in DataManager.singleTon.saveData.inGameData.monsterData[DataManager.singleTon.saveData.inGameData.stage-1])
        {
            MonstersDataClass temp = MonsterDataDeepcopy(monster);
            DataManager.singleTon.saveData.inGameData.crruentMonster.Add(temp);
            DataManager.singleTon.saveData.inGameData.monsterCount++;
        }

        for (int i = 0; i < DataManager.singleTon.saveData.inGameData.crruentMonster.Count; i++)
        {
            DataManager.singleTon.saveData.inGameData.monsterDefenceBuff[i] = new List<Buff>();
            DataManager.singleTon.saveData.inGameData.monsterDamageBuff[i] = new List<Buff>();
            DataManager.singleTon.saveData.inGameData.monsterBlood[i] = new List<Buff>();
        }
    }

    MonstersDataClass MonsterDataDeepcopy(MonstersDataClass ori)
    {
        MonstersDataClass copy = new MonstersDataClass();

        copy.Character = ori.Character;
        copy.Difficulty = ori.Difficulty;
        copy.Type = ori.Type;
        copy.Hp = ori.Hp;
        copy.Atk = ori.Atk;
        copy.turn_general = ori.turn_general;
        copy.turn_special = ori.turn_special;
        copy.Img_Path = ori.Img_Path;

        return copy;
    }
}