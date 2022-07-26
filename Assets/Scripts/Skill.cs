using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 구조체 
public struct SkillInfo
{
    public int energe,attack, skillAttribute;//에너지, 데미지, 속성
    string skillExplain;//속성,설명

    public SkillInfo(int energe,int attack,int skillAttribute,string skillExplain)
    {
        this.energe = energe;
        this.attack = attack;
        this.skillAttribute = skillAttribute;
        this.skillExplain = skillExplain;
    }
}

public class Skill : MonoBehaviour
{
    //스킬 배열 선언 
    static List<SkillInfo> skillList = new List<SkillInfo>
    {
        new SkillInfo(8,150,1,"1단계"),
        new SkillInfo(17,250,2,"2단계"),
        new SkillInfo(21,300,3,"3단계"),
        new SkillInfo(35,550,1,"4단계")
    };

    public int consumeEnerge;//소모 에너지
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
