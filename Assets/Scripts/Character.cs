using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character
{
    public Sprite Img = null;
    public string Name = "";
    public int HP = 0;
    public int ATK = 0;
    public string Type = "";

    public Character(Sprite i = null, string n = "무명", int h = 0, int a = 0, string t = "없음")
    {
        this.Img = i;
        this.Name = n;
        this.HP = h;
        this.ATK = a;
        this.Type = t;
    }
}
