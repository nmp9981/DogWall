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

    [Space]

    [Header("MonsterUI")]
    [SerializeField]
    List<Image> monsterImage;


    public void PlayerUISetting() // 플레이어 관련 UI를 세팅한다
    {
        
    }

    public void StageUISetting() // 스테이지 관련 UI를 세팅한다
    {

    }

    public void MonsterUISetting() // 몬스터 관련 UI를 세팅한다
    {

    }
}
