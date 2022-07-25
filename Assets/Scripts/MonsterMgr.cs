using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMgr : MonoBehaviour
{
    public int monsterHP = 1000000;//몬스터 체력
    bool isMonsterBlood = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //출혈 데미지 계산
    public void MonsterBloodDamage()
    {
        if (isMonsterBlood)
        {
            monsterHP = Mathf.Max(monsterHP - 10000, 0);
        }
    } 
}
