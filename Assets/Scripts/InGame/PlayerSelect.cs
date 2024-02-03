using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    [SerializeField]
    public UISetting UISetting;

    public bool CheckTurn()
    {
        if (DataManager.singleTon.saveData.inGameData.isTurn) return false;
        else return true;
    }

    public void ChSelect(int number)
    {
        if (CheckTurn()) return;
        DataManager.singleTon.saveData.inGameData.selectPlayer = number;
        UISetting.PlayerUISetting();
    }

    public void SkillSelect(int number)
    {
        if (CheckTurn()) return;
        DataManager.singleTon.saveData.inGameData.selectSkill[DataManager.singleTon.saveData.inGameData.selectPlayer] = number;
        UISetting.PlayerUISetting();
    }

    public void MonsterSelect(int number)
    {
        if (CheckTurn()) return;
        DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer] = number;
        UISetting.PlayerUISetting();
        /*
        if (DataManager.singleTon.saveData.inGameData.monsterTaunt.Count == 0)
        {
            DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer] = number;
            UISetting.PlayerUISetting();
        }
        else if(DataManager.singleTon.saveData.inGameData.monsterTaunt.Count == 1)
        {
            if (DataManager.singleTon.saveData.inGameData.monsterTaunt[0] != number)
            {
                // 몬스터 선택 불가 이펙트
            
            }
            UISetting.PlayerUISetting();
            return;
        }
        else if(!DataManager.singleTon.saveData.inGameData.monsterTaunt.Contains(number))
        {
            // 몬스터 선택 불가 이펙트
            UISetting.PlayerUISetting();
            return;
        }
        */
    }
}
