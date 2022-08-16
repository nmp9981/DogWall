using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CharacterDataClass//캐릭터 DB
{
    public string img_path = "";//이미지 경로,캐릭터의 영문명으로 요청하기
    public Sprite Img = null;//이미지
    public string Name = "";//캐릭터명
    public int HP = 0;//HP
    public int Energe = 0;//에너지
    public int Attribute = 0;//속성
    public int ATK = 0;//공격력
    public string Type = "";//유형
    public int Appear = 1;//출현 
    public int Upgrade = 0;//강화 횟수
    public int Star = 1;//별
    public int Same = 0; //뽑기에서 같은 캐릭터 뽑은 경우 +1

    public CharacterDataClass(string path = "",Sprite img = null, string n = "무명", int h = 0,int e = 0,int A = 1, int a = 0, string t = "없음", int ap = 0, int Up = 0, int S = 1, int Pair = 0)
    {
        this.img_path = path;
        this.Img = img;
        this.Name = n;
        this.HP = h;
        this.Energe = e;
        this.Attribute = A;
        this.ATK = a;
        this.Type = t;
        this.Appear = ap;
        this.Upgrade = Up;
        this.Star = S;
        this.Same = Pair; 
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
public class UI
{
    public List<int> money;//재화
    public string home_img_path;
    public float bgm, sfx;
    public UI()
    {
        money = new List<int>();
        home_img_path = "";
    }
    public UI(List<int> m, string h_p)
    {
        this.money = m;
        this.home_img_path = h_p;
    }

}
[System.Serializable]
public class SaveDataClass
{
    public List<CharacterDataClass> list;
    public List<CharacterDataClass> my_characterList;//내가 가지고 있는 캐릭
    public List<SkillDataClass> skillList;//스킬 리스트
    public List<MonsterDataClass> monsterList;//몬스터 리스트
    //스테이지 진행정도
    public List<CharacterDataClass> my_team;
    public UI ui;
    public SaveDataClass()
    {
        //ㅇㅇ
        list = new List<CharacterDataClass>();
        my_characterList= new List<CharacterDataClass>();
        my_team = new List<CharacterDataClass>();
        for(int i = 0; i < 12; i++)
            my_team.Add(new CharacterDataClass());
        skillList = new List<SkillDataClass>();
        monsterList = new List<MonsterDataClass>();
        ui =  new UI();
    }
    public void SetImg()
    {
        foreach(CharacterDataClass a in list)
            if(a.img_path != "")
                a.Img = Resources.Load<Sprite>(a.img_path);
        foreach(CharacterDataClass a in my_characterList)
            if(a.img_path != "")
                a.Img = Resources.Load<Sprite>(a.img_path);
        foreach(CharacterDataClass a in my_team)
            if(a.img_path != "")
                a.Img = Resources.Load<Sprite>(a.img_path);
    }
}
