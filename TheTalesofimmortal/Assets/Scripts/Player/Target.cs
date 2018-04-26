using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target
{
    public PlayerInfo Info;
    public int HP;
    public int HpMax;

    public int MP = 1;
    public int Shield;
    public int Armor = 0;
    public int PhysicalReduction = 0;
    public int MagicalReduction = 0;
    public int HealPerRound;
    public int Poison = 0;
    public int Sunder = 0;
    public int Weak = 0;
    public int Acceleration = 0;
    public int Deceleration = 0;
    public int Rebound = 0;
    public int Dodge = 0;
    public bool DamageToMana = false;

    public List<CardBuff> Buffs;
    public TargetView View;

    public int Heal(int value){
        int v = Mathf.Min(value, HpMax - HP);
        HP += v;
        View.UpdateHp(HP, HpMax);
        return v;
    }

    public int Damage(int value){
        int v = Mathf.Min(value, HP);
        HP -= v;
        View.UpdateHp(HP, HpMax);
        return v;
    }

    public int HealMp(int value){
        MP += value;
        View.UpdateMp(MP);
        return value;
    }

    public int DamageMp(int value){
        int v = Mathf.Min(value, HP);
        MP -= v;
        View.UpdateMp(MP);
        return v;
    }
        

}

//暂无引用
public enum PlayerInfo{
    Player,
    Enemy,
    Puppet
}


