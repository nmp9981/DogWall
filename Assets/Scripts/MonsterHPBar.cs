using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    MonsterMgr monsterMgr;
    Image mobHealthBar;//체력바 이미지
    public float mobHPRate;//hp 비율

    // Start is called before the first frame update
    void Start()
    {
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        mobHealthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        mobHPRate = (float)monsterMgr.monsterHP / (float)monsterMgr.monsterFullHP;
        mobHealthBar.fillAmount = mobHPRate;
    }
}