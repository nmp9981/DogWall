using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    CharacterMgr characterMgr;
    Image HealthBar;//체력바 이미지
    public float HPRate;//hp 비율

    // Start is called before the first frame update
    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        HealthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HPRate = (float)characterMgr.playerHP / (float)characterMgr.playerFullHP;
        HealthBar.fillAmount = HPRate;
    }
}