using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI 관련 게임 오브젝트")]
    public List<GameObject> playerImg = new List<GameObject>(); // 플레이어 초상화
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
    public void nameSet(int number)
    {
        name.text = data.saveData.my_team[number].Name;
    }

    public void atkSet(int number)
    {
        atk.text = data.saveData.my_team[number].ATK + "ATK";
    }

    public void skillSet(int number)
    {

    }
}
