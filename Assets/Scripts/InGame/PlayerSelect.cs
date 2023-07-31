using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    bool CheckTurn()
    {
        if (DataManager.singleTon.saveData.inGameData.isTurn) return true;
        else return false;
    }

    public void ChSelect(int number)
    {

    }

    public void SkillSelect()
    {

    }
}
