using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Text messages;
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    //데미지 출력
    public void AllDamageMassage(int total, int monsterCount)
    {
        gameObject.SetActive(true);//메세지가 보이게
        messages.text = "총 "+total+"데미지! " + monsterCount + "명 공격!";
        Invoke("OffDamageText", 2.0f);//2초후 끄기
    }
    public void DamageMassage(int total, string mobName)
    {
        gameObject.SetActive(true);//메세지가 보이게
        messages.text = "총 " + total + "데미지! " + mobName+ " 공격!";
        Invoke("OffDamageText", 2.0f);//2초후 끄기
    }
    //텍스트 끄기
    void OffDamageText()
    {
        this.gameObject.SetActive(false);
    }
}
