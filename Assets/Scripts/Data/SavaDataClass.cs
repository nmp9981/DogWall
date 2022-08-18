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
public class PlayerDataClass//캐릭터 DB
{
    public string Name = "";//캐릭터명
    public int HP = 0;//HP
    public int Energe = 0;//에너지
    public int Attack = 0;//공격력
    public int Attribute = 0;//속성
    public int Star = 1;
    public int Appear = 1;//출현 

    public PlayerDataClass( string n = "무명", int h = 0, int e = 0, int a = 0, int A = 1, int S = 1, int ap = 0)
    {
        this.Name = n;
        this.HP = h;
        this.Energe = e;
        this.Attack = a;
        this.Attribute = A;
        this.Star = S;
        this.Appear = ap;
    }
}
[System.Serializable]
public class SkillDataClass//스킬DB
{
    public string SkillName = "";//이름
    public int Energe = 0;//에너지
    public string Explain = "";//설명
    public int Attack = 0;//공격
    public int CharacterAttack = 0;//캐릭터 공격력 증가
    public int DecreaseDamage = 0;//데미지감소
    public int HealHP = 0;//HP회복
    public bool Provocation = false;//도발
    public bool NotAction = false;//행동불능
    public int HealEnerge = 0;//에너지 회복
    public int blood = 0;//출혈
    public int TurnCount = 1;//지속 턴수
    public int Rare = 1;//레어도

    public SkillDataClass(string sn = "럭키세븐",int e = 0,string ex = "?",int a = 0,int ca = 0,int dd = 0,int hh = 0,bool p=false,bool na = false,int he = 0,int b=0,int tc=1,int r = 1)
    {
        this.SkillName = sn;
        this.Energe = e;
        this.Explain = ex;
        this.Attack = a;
        this.CharacterAttack = ca;
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
    public string World = "월령";//1:월령, 2:엠피레오, 3: 제한 구역, 4: 전체
    public int Attribute = 1;//속성
    public string Name = "";//이름
    public int Stage = 1;//스테이지
    public int HP = 0;//HP
    public int Attack = 0;//공격력

    public MonsterDataClass(string w="월령",int a=1,string n="1",int s=1,int h=0,int at = 0)
    {
        this.World = w;
        this.Attribute = a;
        this.Name = n;
        this.Stage = s;
        this.HP = h;
        this.Attack = at;
    }
}
public class BossMonsterDataClass//보스몬스터 DB
{
    public string World = "월령";//1:월령, 2:엠피레오, 3: 제한 구역, 4: 전체
    public int Attribute = 1;//속성
    public string Name = "";//이름
    public int Stage = 1;//스테이지
    public int HP = 0;//HP
    public int Attack = 0;//공격력

    public BossMonsterDataClass(string w = "월령", int a = 1, string n = "1", int s = 1, int h = 0, int at = 0)
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
public class MonsterSkillDataClass
{
    public int Index = 1;//몬스터 스킬 인덱스
    public int Damage = 0;//데미지
    public int Blood = 0;//출혈 데미지
    public int Attack = 0;//몬스터 공격력
    public int Defense = 0;//방어력
    public int HealHP = 0;//hp회복량
    public int TurnCount = 0;//스킬 턴수
    public int Enemy = 1;//공격하는 적의 수

    public MonsterSkillDataClass(int i=1,int d=1,int b=1,int a=1,int de=1,int hh=1,int tc=1,int e = 1)
    {
        this.Index = i;
        this.Damage = d;
        this.Blood = b;
        this.Attack = a;
        this.Defense = de;
        this.HealHP = hh;
        this.TurnCount = tc;
        this.Enemy = e;
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
public class Dialog
{
    public string character_name;//
    public CharacterDataClass character;
    public int pos;//화자 위치 - 왼  = 0, 오 = 1, 나레이션 = 2;
    public string dialog;//대화 내용
    public Dialog(string name = "", int position = 2, string text = "")
    {
        character_name = name;
        character = null;
        pos = position;
        dialog = text;
    }
}
[System.Serializable]
public class Story
{
    public string world;
    public List<Dialog> story;

    public Story()
    {
        world = "";
        story = new List<Dialog>();
    }
    public Story(string world, List<Dialog> story)
    {
        this.world = world;
        this.story = story;
    }
}
[System.Serializable]
public class SaveDataClass
{
    public List<CharacterDataClass> list;
    public List<CharacterDataClass> CharacterList;//캐릭터 리스트
    public List<PlayerDataClass> CharacterData;//캐릭터 리스트
    public List<SkillDataClass> SkillData;//스킬 리스트
    public List<MonsterDataClass> MonsterData;//몬스터 리스트
    public List<BossMonsterDataClass> BossData;//보스몬스터 리스트
    public List<MonsterSkillDataClass> MonsterSkillData;//몬스터 스킬 리스트
    //스테이지 진행정도
    public List<CharacterDataClass> my_team;
    public UI ui;
    public List<Story> Story;
    public SaveDataClass()
    {
        //리스트 불러오기
        list = new List<CharacterDataClass>();
        CharacterList= new List<CharacterDataClass>();
        my_team = new List<CharacterDataClass>();
        for(int i = 0; i < 12; i++)
            my_team.Add(new CharacterDataClass());
        SkillData = new List<SkillDataClass>();
        MonsterData = new List<MonsterDataClass>();
        BossData = new List<BossMonsterDataClass>();
        MonsterSkillData = new List<MonsterSkillDataClass>();
        ui =  new UI();
        Story = new List<Story>();
    }
    public void SetImg()
    {
        
        foreach(CharacterDataClass a in list)
            if(a.img_path != "")
                a.Img = Resources.Load<Sprite>(a.img_path);
        foreach(CharacterDataClass a in CharacterList)
            if(a.img_path != "")
                a.Img = Resources.Load<Sprite>(a.img_path);
        foreach(CharacterDataClass a in my_team)
            if(a.img_path != "")
                a.Img = Resources.Load<Sprite>(a.img_path); 
    }

    public void Find_Teller()
    {
        List<CharacterDataClass> temp = new List<CharacterDataClass>();
        for(int i = 0; i < Story.Count; i++)
        {
            for(int j = 0; j < Story[i].story.Count; j++)
            {
                bool find = false;
                foreach(CharacterDataClass a in temp)//temp에서 먼저 찾아보고
                {
                    if(a.Name == Story[i].story[j].character_name)
                    {
                        Story[i].story[j].character = a;
                        find = true;
                        break;
                    }
                }
                if(find)//있으면 탈출
                    break;
                else//아니면 list에서 찾아보기
                {
                    foreach(CharacterDataClass a in list)
                    {
                        if(a.Name == Story[i].story[j].character_name)
                        {
                            Story[i].story[j].character = a;
                            temp.Add(a);
                            break;
                        }
                    }
                }
            }
            temp.Clear();//다음 다이얼로그로 넘어가기전에 클리어해주기
        }
    }
}
