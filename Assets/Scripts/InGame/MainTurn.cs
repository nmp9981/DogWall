using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurn : MonoBehaviour
{
    [SerializeField]
    Skill skill;
    [SerializeField]
    Stage stage;

    public void PlayerTurnStartSetting() // 플레이어 턴 시작시 기본 세팅
    {
        DataManager.singleTon.saveData.inGameData.isTurn = true; // 플레이어 턴이 끝났을때 클릭되는 것을 방지
        DataManager.singleTon.saveData.inGameData.isEffect = false; // 이펙트가 표시중인가를 확인
    }

    public void PlayerTurnEnd() // 턴 종료 버튼을 누를시 시작
    {
        skill.PlayerSkill();
    }
}
