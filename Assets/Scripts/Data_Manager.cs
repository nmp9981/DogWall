using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    public int Character_Count = 0;
    public int Monster_Count = 0;
    public List<CharacterDataClass> Characterlist = new List<CharacterDataClass>();
    public List<SkillDataClass> SkillList = new List<SkillDataClass>();
    public List<MonsterDataClass> MonsterList = new List<MonsterDataClass>();
    public List<QuestDataClass> QuestList = new List<QuestDataClass>();

    void Start()
    {

    }

    public void Add_Character(string path,Sprite img,string name,int hp,int energe,int attribute,int attack,string type,int appear,int star)
    {
        Character_Count++;
        Characterlist.Add(new CharacterDataClass(path, img, name, hp, energe, attribute, attack, type, appear, star));
    }
    public int Get_Character_Count()
    {
        return Character_Count;
    }

    public void Add_Monster(int world,int attribute, string name,int stage, int hp, int atk)
    {
        Monster_Count++;
        MonsterList.Add(new MonsterDataClass(world,attribute, name, stage, hp, atk));
    }
    public void Add_Skill(string name, int energe, string explain,int attack,int decrease,int healHP,bool provocation,bool action,int healE,int blood,int trunCount,int rare)
    {
        SkillList.Add(new SkillDataClass(name,energe,explain,attack,decrease,healHP,provocation,action,healE,blood,trunCount,rare));
    }
    public void Add_Quest(int episode,int world,int bigStage,int middleStage,int smallStage,int mobIndex,int mobAction)
    {
        QuestList.Add(new QuestDataClass(episode, world, bigStage, middleStage, smallStage, mobIndex, mobAction));
    }
}
