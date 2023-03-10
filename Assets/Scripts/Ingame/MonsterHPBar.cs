using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    MonsterMgr monsterMgr;
    Image mobHealthBar;//체력바 이미지
    public float mobHPRate;//hp 비율
    public int monsterHPIndex;//몬스터 HP인덱스

    // Start is called before the first frame update
    void Start()
    {
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        mobHealthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ViewMonsterHP();//몬스터 HP 시각화
    }
    //몬스터 HP 시각화
    void ViewMonsterHP()
    {
        mobHPRate = (float)monsterMgr.currentMonsterHP[monsterHPIndex] / (float)monsterMgr.monsterFullHP[monsterHPIndex];//실제 사용
        mobHealthBar.fillAmount = mobHPRate;
    }
}