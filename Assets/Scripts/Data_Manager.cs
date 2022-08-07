﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    private int Character_Count = 0;
    private int Monster_Count = 0;
    public List<Character> list = new List<Character>();
    public List<SkillDataClass> SkillList = new List<SkillDataClass>();
    public List<MonsterDataClass> MonsterList = new List<MonsterDataClass>();
    void Start()
    {

    }

    public int Get_Character_Count()
    {
        return Character_Count;
    }

    public void Add_Character(string img, string name, int hp,int energe, int atk, string type)
    {
        Character_Count++;
        Sprite temp = Resources.Load<Sprite>(img);
        list.Add(new Character(temp,name,hp,energe,atk,type));
    }
    public void Add_Monster(int world,int attribute, string name,int stage, int hp, int atk)
    {
        Monster_Count++;
        MonsterList.Add(new MonsterDataClass(world,attribute, name,stage, hp, atk));
    }
    public void Add_Skill(string name, int energe, string explain,int attack,int decrease,int healHP,bool provocation,bool action,int healE,int blood,int trunCount,int rare)
    {
        SkillList.Add(new SkillDataClass(name,energe,explain,attack,decrease,healHP,provocation,action,healE,blood,trunCount,rare));
    }
}
