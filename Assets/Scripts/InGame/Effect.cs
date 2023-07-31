using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    int maxNum;

    Action checkAction;

    //1 AttackOne
    //2 AttackAll
    //3 Buff
    //4 Debuff
    //5 Heal
    //+ 특수 이펙트들

    public void EffectSetting() // 이펙트 시작전 기본 세팅
    {
        maxNum = DataManager.singleTon.saveData.inGameData.playerData.Count;
    }

    public IEnumerator StartEffect()
    {
        for (int i = 0; i < maxNum; i++)
        {
            PlayerEffect(1);
            yield return new WaitForSeconds(2f);
        }
    }


    public void PlayerEffect(int num)
    {
        switch(num)
        {
            case 1:
                animator.SetTrigger("AttackAll");
                return;
            case 2:
                animator.SetTrigger("AttackOne");
                return;
            case 3:
                animator.SetTrigger("Buff");
                return;
            case 4:
                animator.SetTrigger("Debuff");
                return;
            case 5:
                animator.SetTrigger("Heal");
                return;
        }
    }
}
