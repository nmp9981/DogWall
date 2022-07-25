using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 구조체 
struct SkillInfo
{
    public float energe,attack;//에너지, 데미지
    string skillAttribute,skillExplain;//속성,설명
}

public class Skill : MonoBehaviour
{
    SkillInfo[,] skillList = new SkillInfo[20,4];//스킬 2차원 배열 선언 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
