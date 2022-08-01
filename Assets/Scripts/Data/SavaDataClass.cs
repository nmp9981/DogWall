using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TeamSet
{
    public Character[,] team_set = new Character[3,4];
}
[System.Serializable]
public class Character
{
    public Sprite Img = null;
    public string Name = "";
    public int HP = 0;
    public int ATK = 0;
    public string Type = "";
    public int Stone = 0;
    public int Star = 1;

    public Character(Sprite i = null, string n = "무명", int h = 0, int a = 0, string t = "없음", int s = 0, int S = 1)
    {
        this.Img = i;
        this.Name = n;
        this.HP = h;
        this.ATK = a;
        this.Type = t;
        this.Stone = s;
        this.Star = S;
    }
}
[System.Serializable]
public class SaveDataClass
{
    public List<Character> list;
    public List<Character> my_list;
    public TeamSet my_team;
    public SaveDataClass()
    {
        //ㅇㅇ
        list = new List<Character>();
        my_list= new List<Character>();
        my_team = new TeamSet();
    }

}
