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

    void Start()
    {
        PlayerTurnStartSetting();
    }

    public void PlayerTurnStartSetting() // 플레이어 턴 시작시 기본 세팅
    {
        // 테스트용 데이터
        DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[0].idx]);
        DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[1].idx]);
        DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[2].idx]);
        DataManager.singleTon.saveData.inGameData.playerData.Add(DataManager.singleTon.saveData.CharacterData[DataManager.singleTon.saveData.my_team[3].idx]);


        DataManager.singleTon.saveData.inGameData.isTurn = true; // 플레이어 턴이 끝났을때 클릭되는 것을 방지
        DataManager.singleTon.saveData.inGameData.isEffect = false; // 이펙트가 표시중인가를 확인
        DataManager.singleTon.saveData.inGameData.selectPlayer = 0; // 선택된 플레이어 초기화 (디폴트값 1번째)
        DataManager.singleTon.saveData.inGameData.selectSkill.Clear();
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4);
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4);
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4);
        DataManager.singleTon.saveData.inGameData.selectSkill.Add(4); // 스킬 선택 초기화 (디폴트값 스킬 안씀)

        playerSelect.UISetting.PlayerUISetting();
    }

    public void PlayerTurnEnd() // 턴 종료 버튼을 누를시 시작
    {
        if (playerSelect.CheckTurn()) return;
        DataManager.singleTon.saveData.inGameData.isTurn = false;
        skill.PlayerSkill();
    }
}