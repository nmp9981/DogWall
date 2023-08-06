using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    [SerializeField]
    List<GameObject> playerTarget;

    [Space(5)]

    [Header("MonsterUI")]
    [SerializeField]
    List<GameObject> monster;


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

        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData[DataManager.singleTon.saveData.inGameData.selectPlayer].Star; i++)
        {
            sb.Append('★');
        }
        sb.Append(" ");
        sb.Append(DataManager.singleTon.saveData.inGameData.playerData[DataManager.singleTon.saveData.inGameData.selectPlayer].Name);
        playerName.text = sb.ToString();

        for(int i = 0; i < 3; i++)
        {
            playerTarget[i].SetActive(false);
        }
        playerTarget[DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer]].SetActive(true);
    }

    public void StageUISetting() // 스테이지 관련 UI를 세팅한다
    {

    }

    public void MonsterUISetting() // 몬스터 관련 UI를 세팅한다
    {
        for(int i = 0; i < DataManager.singleTon.saveData.inGameData.crruentMonster.Count; i++)
        {
            monster[i].transform.GetChild(2).GetComponent<Image>().fillAmount = (float)DataManager.singleTon.saveData.inGameData.crruentMonster[i].Hp / (float)DataManager.singleTon.saveData.inGameData.monsterData[DataManager.singleTon.saveData.inGameData.stage - 1][i].Hp;
        }
    }

    public void MonsterFadeOut(int num)
    {
        InGameUtil.instance.Util(monster[num], "FadeOut");
    }

    public void MonsterLocationUISetting()
    {
        if (DataManager.singleTon.saveData.inGameData.crruentMonster.Count == 0)
        {
            foreach(GameObject monster in this.monster)
            {
                monster.SetActive(false);
            }
        }
        else if (DataManager.singleTon.saveData.inGameData.crruentMonster.Count == 1)
        {
            monster[0].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            monster[0].GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            monster[0].SetActive(true);
            monster[1].SetActive(false);
            monster[2].SetActive(false);
        }
        else if (DataManager.singleTon.saveData.inGameData.crruentMonster.Count == 2)
        {
            monster[0].GetComponent<RectTransform>().localScale = new Vector3(0.45f, 0.45f, 1f);
            monster[0].GetComponent<RectTransform>().localPosition = new Vector3(-260f, 180f, 0f);
            monster[0].SetActive(true);
            monster[1].GetComponent<RectTransform>().localScale = new Vector3(0.45f, 0.45f, 1f);
            monster[1].GetComponent<RectTransform>().localPosition = new Vector3(260f, 180f, 0f);
            monster[1].SetActive(true);
            monster[2].SetActive(false);
        }
        else if (DataManager.singleTon.saveData.inGameData.crruentMonster.Count == 3)
        {
            monster[0].GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 1f);
            monster[0].GetComponent<RectTransform>().localPosition = new Vector3(-345f, 170f, 0f);
            monster[0].SetActive(true);
            monster[1].GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 1f);
            monster[1].GetComponent<RectTransform>().localPosition = new Vector3(0f, 170f, 0f);
            monster[1].SetActive(true);
            monster[2].GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 1f);
            monster[2].GetComponent<RectTransform>().localPosition = new Vector3(345f, 170f, 0f);
            monster[2].SetActive(true);
        }
    }
}
