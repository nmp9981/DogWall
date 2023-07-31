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
    }
}
