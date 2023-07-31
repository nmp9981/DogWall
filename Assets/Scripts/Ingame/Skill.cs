using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    Effect effect;
    [SerializeField]
    UISetting UISetting;

    public void PlayerSkill()
    {
        effect.EffectSetting();
        StartCoroutine(effect.StartEffect());
    }

    public void MonsterSkill()
    {

    }

    public void SkillEnd()
    {

    }
}
