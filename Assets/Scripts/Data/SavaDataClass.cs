using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
[System.Serializable]
public class Character//캐릭터 DB
{
    public string img_path = "",img_for_dialog_path = "";//이미지 경로,캐릭터의 영문명으로 요청하기
    public Sprite Img = null,Img_for_dialog = null;//이미지
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
    public int idx = -1;//인덱스 리스트에서 빠르게 찾기 위해 사용하는 정보, 지정 안되었을 경우 -1, 리스트에서 찾으면 오류나올거임!
    public bool heartLink = false;//하트 링크 상태인가?
    public bool deathLink = false;//데스 링크 상태인가?
    public Character(string path = "",Sprite img = null, string n = "무명", int h = 0,int e = 0,int A = 1, int a = 0, string t = "없음", int ap = 0, int Up = 0, int S = 1, int Pair = 0,int idx = -1,bool heart = false,bool death = false)
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
        this.idx = idx;
        this.heartLink = heart;
        this.deathLink = death;
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
public class CharacterSkillDataClass//스킬DB
{
    public string Character = "";//캐릭터 명
    public string SkillName = "";//스킬 명
    public int line = 1;//스킬 순서
    public int Skill1 = 0;//스킬 인덱스
    public int Skill2 = 0;//스킬 인덱스
    public int Skill3 = 0;//스킬 인덱스
    public int Skill4 = 0;//스킬 인덱스
    public string Descrition = "";//스킬 설명
    public int Type = 1;//타입
    public int Appear = 0;//등장

    public CharacterSkillDataClass(string cn = "로그",string sn = "럭키세븐",int l=0,int s1=0,int s2=0,int s3=0,int s4=0,string des = "",int t=0,int a=0)
    {
        this.Character = cn;
        this.SkillName = sn;
        this.line = l;
        this.Skill1 = s1;
        this.Skill2 = s2;
        this.Skill3 = s3;
        this.Skill4 = s4;
        this.Descrition = des;
        this.Type = t;
        this.Appear = a;
    }
}
[System.Serializable]
public class CharacterSkillIndexDataClass//스킬DB
{
    public string Name = "";//이름
    public int Attack = 0;//공격
    public int CharacterAttack = 0;//캐릭터 공격력 증가
    public int DecreaseDamage = 0;//데미지감소
    public int HealHP = 0;//HP회복
    public bool Provocation = false;//도발
    public bool NotAction = false;//행동불능
    public int HealEnerge = 0;//에너지 회복
    public int Blood = 0;//출혈
    public bool AllTargets = false;//전체 공격여부
    public int TurnCount = 1;//지속 턴수
    public bool HeartLink = false;//하트 링크 여부
    public bool DeathLink = false;//데스 링크 여부
    public int Energy = 1;//에너지 사용량

    public CharacterSkillIndexDataClass(string sn = "럭키세븐", int a = 0, int ca = 0, int dd = 0, int hh = 0, bool p = false, bool na = false, int he = 0, int b = 0,bool allt=false, int tc = 1,bool hli = false,bool dli = false, int e = 1)
    {
        this.Name = sn;
        this.Attack = a;
        this.CharacterAttack = ca;
        this.DecreaseDamage = dd;
        this.HealHP = hh;
        this.Provocation = p;
        this.NotAction = na;
        this.HealEnerge = he;
        this.Blood = b;
        this.AllTargets = allt;
        this.TurnCount = tc;
        this.HeartLink = hli;
        this.DeathLink = dli;
        this.Energy = e;
    }
}
//조만간 지울거
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
public class MonstersDataClass//몬스터 DB
{
    public string World = "월령";//1:월령, 2:엠피레오, 3: 제한 구역, 4: 전체
    public string Name = "";//이름
    public int Difficulty = 1;//속성
    public int HP = 0;//HP
    public int Attack = 0;//공격력
    //각 턴에 뭐를 쓸건가
    public int turn0_Ger1 = 0;public int turn0_Ger2 = 0;public int turn0_Spe1 = 0;public int turn0_Spe2 = 0;
    public int turn1_Ger1 = 0;public int turn1_Ger2 = 0;public int turn1_Spe1 = 0;public int turn1_Spe2 = 0;
    public int turn2_Ger1 = 0;public int turn2_Ger2 = 0;public int turn2_Spe1 = 0;public int turn2_Spe2 = 0;
    public int turn3_Ger1 = 0;public int turn3_Ger2 = 0;public int turn3_Spe1 = 0;public int turn3_Spe2 = 0;
    public int turn4_Ger1 = 0;public int turn4_Ger2 = 0;public int turn4_Spe1 = 0;public int turn4_Spe2 = 0;
    public int turn5_Ger1 = 0;public int turn5_Ger2 = 0;public int turn5_Spe1 = 0;public int turn5_Spe2 = 0;

    public MonstersDataClass(string w = "월령", string n = "1", int d = 1 , int h = 0, int at = 0,
        int t0G1=0,int t0G2=0,int t0S1=0,int t0S2=0,
        int t1G1 = 0, int t1G2 = 0, int t1S1 = 0, int t1S2 = 0,
        int t2G1 = 0, int t2G2 = 0, int t2S1 = 0, int t2S2 = 0,
        int t3G1 = 0, int t3G2 = 0, int t3S1 = 0, int t3S2 = 0,
        int t4G1 = 0, int t4G2 = 0, int t4S1 = 0, int t4S2 = 0,
        int t5G1 = 0, int t5G2 = 0, int t5S1 = 0, int t5S2 = 0)
    {
        this.World = w;
        this.Name = n;
        this.Difficulty = d;
        this.HP = h;
        this.Attack = at;
        //각 턴 스킬 번호
        this.turn0_Ger1 = t0G1;this.turn0_Ger2 = t0G2;this.turn0_Spe1 = t0S1;this.turn0_Spe2 = t0S2;
        this.turn1_Ger1 = t1G1;this.turn1_Ger2 = t1G2;this.turn1_Spe1 = t1S1;this.turn1_Spe2 = t1S2;
        this.turn2_Ger1 = t2G1;this.turn2_Ger2 = t2G2;this.turn2_Spe1 = t2S1;this.turn2_Spe2 = t2S2;
        this.turn3_Ger1 = t3G1;this.turn3_Ger2 = t3G2;this.turn3_Spe1 = t3S1;this.turn3_Spe2 = t3S2;
        this.turn4_Ger1 = t4G1;this.turn4_Ger2 = t4G2;this.turn4_Spe1 = t4S1;this.turn4_Spe2 = t4S2;
        this.turn5_Ger1 = t5G1;this.turn5_Ger2 = t5G2;this.turn5_Spe1 = t5S1;this.turn5_Spe2 = t5S2;
    }
}
[System.Serializable]
public class MonsterDataClass//몬스터 DB, 지울 예정
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
    public string name = "";//스킬 설명
    public int Damage = 0;//데미지
    public int AttackCount = 0;//공격 횟수
    public int Targets = 0;//타겟 수
    public int Blood = 0;//출혈 데미지
    public int TurnCount = 0;//스킬 턴수
    public int Attack = 0;//몬스터 공격력
    public int DecreaseDamage = 0;//피격 데미지 감소량
    public bool Self = false;//자기자신한테 공격하는 것인가?
    public int HealHP = 0;//hp회복량
    public int HeartLink = 3000;//하트링크 데미지
    public int DeathLink = 3000;//데스링크 데미지

    public MonsterSkillDataClass(int i = 1, string name = null, int d = 1, int ac = 1,int b=1,int tc = 1,int a=1,int de=1,bool s = false,int hh=1,int hl = 1,int dl=1)
    {
        this.Index = i;
        this.name = name;
        this.Damage = d;
        this.AttackCount = ac;
        this.Blood = b;
        this.TurnCount = tc;
        this.Attack = a;
        this.DecreaseDamage = de;
        this.Self = s;
        this.HealHP = hh;
        this.HeartLink = hl;
        this.DeathLink = dl;
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
    public List<Character> list;
    public List<Character> my_characterlist;//캐릭터 리스트
    public List<PlayerDataClass> CharacterData;//캐릭터 리스트
    public List<SkillDataClass> SkillData;//스킬 리스트
    public List<CharacterSkillDataClass> CharacterSkillData;//캐릭터 스킬 리스트
    public List<CharacterSkillIndexDataClass> CharacterSkillIndexData;//캐릭터 스킬 Index 리스트
    public List<MonsterDataClass> MonsterData;//몬스터 리스트(지울 예정)
    public List<MonstersDataClass> MonstersData;//몬스터 리스트
    public List<BossMonsterDataClass> BossData;//보스몬스터 리스트
    public List<MonsterSkillDataClass> MonsterSkillData;//몬스터 스킬 리스트
    //스테이지 진행정도
    public List<Character> my_team;
    public UI ui;
    public SaveDataClass()
    {
        //리스트 불러오기
        list = new List<Character>();
        my_characterlist= new List<Character>();
        my_team = new List<Character>();
        for(int i = 0; i < 12; i++)
            my_team.Add(new Character());
        SkillData = new List<SkillDataClass>();
        CharacterSkillData = new List<CharacterSkillDataClass>();
        CharacterSkillIndexData = new List<CharacterSkillIndexDataClass>();
        MonsterData = new List<MonsterDataClass>();
        BossData = new List<BossMonsterDataClass>();
        MonsterSkillData = new List<MonsterSkillDataClass>();
        MonstersData = new List<MonstersDataClass>();
        ui =  new UI();
    }
    public void SetImg()//캐릭터에 저장된 경로를 통해 이미지를 지정
    {      
        foreach(Character a in list)
            if(a.img_path != "")
            {
                a.Img = Resources.Load<Sprite>(a.img_path);
                if(a.img_for_dialog_path != "")
                    a.Img_for_dialog = Resources.Load<Sprite>(a.img_for_dialog_path);
            }
        foreach(Character a in my_characterlist)
            if(a.img_path != "")
            {
                a.Img = Resources.Load<Sprite>(a.img_path);
                if(a.img_for_dialog_path != "")
                    a.Img_for_dialog = Resources.Load<Sprite>(a.img_for_dialog_path);
            }
        foreach(Character a in my_team)
            if(a.img_path != "")
            {
                a.Img = Resources.Load<Sprite>(a.img_path);
                if(a.img_for_dialog_path != "")
                    a.Img_for_dialog = Resources.Load<Sprite>(a.img_for_dialog_path);
            }
    }

    public void SetImgforOnce(string folder)//특정 폴더에 있는 이미지들을 캐릭터에 맞게 배치하기(list용)
    {     
        Sprite[] imgs;
        imgs = Resources.LoadAll<Sprite>(folder);//리소스 폴더에 있는 이미지를 모두 가져옴
        List<string> names = new List<string>();
        foreach(Sprite i in imgs)//모든 이미지들의 경로를 가져옴
        {
            string path = AssetDatabase.GetAssetPath(i);
            path = path.Replace("Assets/Resources/" + folder,string.Empty);//불필요한 부분제거
            path = path.Replace(".png",string.Empty);//불필요한 부분 제거
            names.Add(path);//불필요한 부분을 제거한 이름을 리스트로 저장
        }
        foreach(Character a in list)//리스트의 모든 캐릭터들을 대상으로 동작
            if(a.img_path != "")//경로가 있다면
            {
                foreach(string name in names)//앞서 저장한 이름들과 비교
                {
                    if(name.Contains(a.img_path))//이름이 캐릭터의 영문명을 포함한다면
                    {
                        if(name == a.img_path)//똑같은 거라면 Img에 저장
                        {
                            int temp = names.IndexOf(name);
                            a.Img = imgs[temp];
                        }
                        else//아니면 Img_for_dialog에 저장
                        {
                            int temp = names.IndexOf(name);
                            a.Img_for_dialog = imgs[temp];
                            a.img_for_dialog_path = name;//경로도 추가해줌
                        }
                    }
                    if(a.Img != null && a.Img_for_dialog != null)//모두 찾으면 탈출해야지
                        break;
                }    
            }
        foreach(Character a in my_characterlist)//list에 모든 정보가 저장되어있으므로 idx를 통해서 찾기만 하면 됨
            if(a.img_path != "")
            {
                a.Img = list[a.idx].Img;
                a.Img_for_dialog = list[a.idx].Img;
            }
        foreach(Character a in my_team)//list에 모든 정보가 저장되어있으므로 idx를 통해서 찾기만 하면 됨
            if(a.img_path != "")
            {
                a.Img = list[a.idx].Img;
                a.Img_for_dialog = list[a.idx].Img;
            }
    }
}

public class StageInfo{
    public int appear;
    public int episode;
    public int quest;
    public int stage;
    public int difficulty;
    public int type;
    public string character;
}