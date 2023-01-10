using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Text messages;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //데미지 출력
    public void DamageMassage(int total, int attackCount)
    {
        gameObject.SetActive(true);//메세지가 보이게
        messages.text = "총 "+total+"데미지! " + attackCount + "회 공격!";
    }
}
