using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI 관련 게임 오브젝트")]
    public List<GameObject> playerImg = new List<GameObject>(); // 플레이어 초상화
    public List<Text> skillText = new List<Text>(); // 스킬 버튼들
    public Text name;
    public Text atk;

    DataManager data;

    void Start()
    {
        data = DataManager.singleTon;
        for(int i = 0; i < playerImg.Count; i++)
        {
            playerImg[i].GetComponent<Image>().sprite = data.saveData.my_team[i].Img; 
        } // 플레이어 초상화 이미지 세팅
        // 초상화는 바뀌는 경우가 없으니까 처음 한번만~
    }
    public void NameSet(int number)
    {
        Debug.Log("캐릭터 번호 : "+data.playerCharaterNumber[number - 1]);
        name.text = data.saveData.CharacterData[data.playerCharaterNumber[number-1]].Name;
    }

    public void AtkSet(int number)
    {
        atk.text = data.saveData.CharacterData[data.playerCharaterNumber[number-1]].ATK + "ATK";
    }

    public void SkillSet(int number)
    {
        int skillIndex;
        for (int i=0; i<4; i++)
        {
            skillIndex = data.playerCharaterNumber[number-1] * 4 + i;
            skillText[i].text = data.saveData.CharacterSkill[skillIndex].SkillName;
        }
    }
}
