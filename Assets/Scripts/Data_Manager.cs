using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Manager : MonoBehaviour
{
    private int Character_Count = 0;
    public List<Character> list = new List<Character>();
    public List<SkillDataClass> SkillList = new List<SkillDataClass>();
    void Start()
    {

    }

    public int Get_Character_Count()
    {
        return Character_Count;
    }

    public void Add_Character(string img, string name, int hp, int atk, string type)
    {
        Character_Count++;
        Sprite temp = Resources.Load<Sprite>(img);
        list.Add(new Character(temp,name,hp,atk,type));
    }
}
