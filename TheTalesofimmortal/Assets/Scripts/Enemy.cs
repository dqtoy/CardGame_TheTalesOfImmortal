using System.Collections.Generic;
using UnityEngine;
public class Enemy {
    public int MonsterId;
    public string Name;
    public int Gold;
    public int Exp;
    public int MaxAction;
    public int Action;
    public int MaxMana;
    public int Mana;
    public int MaxHp;
    public int Hp;
    public int Level;
    public List<CardData> Deck;
    public Sprite HeadImage;
}

public class Monster{
    public int Id;
    public string Name;
    public string Description;
    public int Gold;
    public int Exp;
    public int Action;
    public int Mana;
    public float Hp;
    public int Level;//这个属性应该是最低等级
    public int IsBoss;
    public int Deck;
    public string Image = "hero1";
}