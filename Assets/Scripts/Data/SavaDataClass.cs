using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TeamSet//팀 번호 저장
{
    public Character[,] team_set = new Character[3,4];
}
[System.Serializable]
public class Character//캐릭터 DB
{
    public Sprite Img = null;
    public string Name = "";
    public int HP = 0;
    public int Energe = 0;
    public int ATK = 0;
    public string Type = "";
    public int Stone = 0;
    public int Star = 1;

    public Character(Sprite i = null, string n = "무명", int h = 0,int e = 0, int a = 0, string t = "없음", int s = 0, int S = 1)
    {
        this.Img = i;
        this.Name = n;
        this.HP = h;
        this.Energe = e;
        this.ATK = a;
        this.Type = t;
        this.Stone = s;
        this.Star = S;
    }
}
[System.Serializable]
public class SkillDataClass//스킬DB
{
    public string SkillName = "";//이름
    public int Energe = 0;//에너지
    public string Explain = "";//설명
    public int Attack = 0;//공격
    public int DecreaseDamage = 0;//데미지감소
    public int HealHP = 0;//HP회복
    public bool Provocation = false;//도발
    public bool NotAction = false;//행동불능
    public int HealEnerge = 0;//에너지 회복
    public int blood = 0;//출혈
    public int TurnCount = 1;//지속 턴수
    public int Rare = 1;//레어도

    public SkillDataClass(string sn = "럭키세븐",int e = 0,string ex = "?",int a = 0,int dd = 0,int hh = 0,bool p=false,bool na = false,int he = 0,int b=0,int tc=1,int r = 1)
    {
        this.SkillName = sn;
        this.Energe = e;
        this.Explain = ex;
        this.Attack = a;
        this.DecreaseDamage = dd;
        this.HealHP = hh;
        this.Provocation = p;
        this.NotAction = na;
        this.HealEnerge = he;
        this.blood = b;
        this.TurnCount = tc;
        this.Rare = r;
    }
}
[System.Serializable]
public class MonsterDataClass//몬스터 DB
{
    public int World = 1;//1:월령, 2:엠피레오, 3: 제한 구역, 4: 전체
    public int Attribute = 1;//속성
    public string Name = "";//이름
    public int Stage = 1;//스테이지
    public int HP = 0;//HP
    public int Attack = 0;//공격력

    public MonsterDataClass(int w=1,int a=1,string n="1",int s=1,int h=0,int at = 0)
    {
        this.World = w;
        this.Attribute = a;
        this.Name = n;
        this.Stage = s;
        this.HP = h;
        this.Attack = at;
    }
}
[System.Serializable]
public class SaveDataClass
{
    public List<Character> list;
    public List<Character> my_characterList;//내가 가지고 있는 캐릭
    public List<int> money;//재화
    public List<SkillDataClass> skillList;//스킬 리스트
    public List<MonsterDataClass> monsterList;//몬스터 리스트
    //스테이지 진행정도
    public TeamSet my_team;
    public SaveDataClass()
    {
        //ㅇㅇ
        list = new List<Character>();
        my_characterList= new List<Character>();
        my_team = new TeamSet();
        skillList = new List<SkillDataClass>();
        monsterList = new List<MonsterDataClass>();
    }
}
