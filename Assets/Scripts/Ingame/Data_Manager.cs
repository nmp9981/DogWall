﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    public int Character_Count = 0;
    public int Monster_Count = 0;
    public List<Character> Characterlist = new List<Character>();
    public List<SkillDataClass> SkillList = new List<SkillDataClass>();//조만간 지울거
    public List<CharacterSkillDataClass> CharacterSkillList = new List<CharacterSkillDataClass>();
    public List<CharacterSkillIndexDataClass> CharacterSkillIndexList = new List<CharacterSkillIndexDataClass>();
    public List<MonsterDataClass> MonsterList = new List<MonsterDataClass>();
    public List<MonstersDataClass> MonstersList = new List<MonstersDataClass>();
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
    public void Add_Monsters(string world, string name, int difficulty, int hp, int atk,
        int t0G1, int t0G2, int t0S1, int t0S2,
        int t1G1, int t1G2, int t1S1, int t1S2,
        int t2G1, int t2G2, int t2S1, int t2S2,
        int t3G1, int t3G2, int t3S1, int t3S2,
        int t4G1, int t4G2, int t4S1, int t4S2,
        int t5G1, int t5G2, int t5S1, int t5S2)
    {
        Monster_Count++;
        MonstersList.Add(new MonstersDataClass(world, name, difficulty, hp, atk,
            0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
    }
    //조만간 지울거
    public void Add_Monster(string world,int attribute, string name,int stage, int hp, int atk)
    {
        Monster_Count++;
        MonsterList.Add(new MonsterDataClass(world,attribute, name, stage, hp, atk));
    }
    //조만간 지울거
    public void Add_Skill(string name, int energe, string explain,int attack,int characterAttack,int decrease,int healHP,bool provocation,bool action,int healE,int blood,int trunCount,int rare)
    {
        SkillList.Add(new SkillDataClass(name,energe,explain,attack,characterAttack,decrease,healHP,provocation,action,healE,blood,trunCount,rare));
    }
    public void Add_CharacterSkill(string character, string name, int line,int skill1,int skill2,int skill3,int skill4, string description,int type,int appear)
    {
        CharacterSkillList.Add(new CharacterSkillDataClass(character, name, line, skill1, skill2, skill3, skill4, description, type, appear));
    }

}
