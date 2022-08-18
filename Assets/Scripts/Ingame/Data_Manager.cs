using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    public int Character_Count = 0;
    public int Monster_Count = 0;
    public List<Character> Characterlist = new List<Character>();
    public List<SkillDataClass> SkillList = new List<SkillDataClass>();
    public List<MonsterDataClass> MonsterList = new List<MonsterDataClass>();
    void Start()
    {

    }

    public void Add_Character(string path, Sprite img,string name,int hp,int energe,  int attribute, int attack,string type, int star, int appear)
    {
        Character_Count++;
        Characterlist.Add(new Character(null,null,name, hp, energe, attribute,attack,type, star, appear));
    }
    public int Get_Character_Count()
    {
        return Character_Count;
    }

    public void Add_Monster(string world,int attribute, string name,int stage, int hp, int atk)
    {
        Monster_Count++;
        MonsterList.Add(new MonsterDataClass(world,attribute, name, stage, hp, atk));
    }
    public void Add_Skill(string name, int energe, string explain,int attack,int characterAttack,int decrease,int healHP,bool provocation,bool action,int healE,int blood,int trunCount,int rare)
    {
        SkillList.Add(new SkillDataClass(name,energe,explain,attack,characterAttack,decrease,healHP,provocation,action,healE,blood,trunCount,rare));
    }
}
