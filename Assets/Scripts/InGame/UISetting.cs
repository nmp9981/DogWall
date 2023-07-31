using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [Header("PlayerUI")]
    [SerializeField]
    List<Image> playerImage;
    [SerializeField]
    Text attackText;
    [SerializeField]
    Text hpText;
    [SerializeField]
    Text energyText;
    [SerializeField]
    Text playerName;
    [SerializeField]
    List<Text> skillName;
    [SerializeField]
    List<Text> skillEnergy;

    [Space(5)]

    [Header("MonsterUI")]
    [SerializeField]
    List<Image> monsterImage;


    public void PlayerUISetting() // 플레이어 관련 UI를 세팅한다
    {
        for(int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData.Count; i++)
        {
            playerImage[i].sprite = DataManager.singleTon.saveData.inGameData.playerData[i].Img; // 플레이어 초상화 세팅
        }

        for(int i = 0; i < 4; i++)
        {
            skillName[i].text = DataManager.singleTon.saveData.CharacterSkill[DataManager.singleTon.saveData.inGameData.playerData[DataManager.singleTon.saveData.inGameData.selectPlayer].idx * 4 + i].SkillName;
            // 에너지는 데이터가 없음 추가 필요
        }

        playerName.text = "★★★ " + DataManager.singleTon.saveData.inGameData.playerData[DataManager.singleTon.saveData.inGameData.selectPlayer].Name;

    }

    public void StageUISetting() // 스테이지 관련 UI를 세팅한다
    {

    }

    public void MonsterUISetting() // 몬스터 관련 UI를 세팅한다
    {

    }
}
