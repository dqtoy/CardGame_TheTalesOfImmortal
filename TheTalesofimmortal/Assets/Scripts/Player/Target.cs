using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target
{
    public PlayerInfo Info;
	public string Name = "";
    public int HP;
    public int HpMax;

    public int MP = 1;
    public int ActPoint = 5;

    //buff相关的状态
    //持续到效果结束的buff
    public int Shield = 0;
    public int HealPerRound = 0;
    public int Poison = 0;
    //持续1回合的buff
    public int Armor = 0;////物理魔法都受
    public int PhysicalReduction = 0;
    public int MagicalReduction = 0;
    public int Sunder = 0;//可能会出现持续到战斗结束的易伤//物理魔法都受
    public int Weak = 0;//可能会出现持续到战斗结束的虚弱//物理魔法都受
    public int Rebound = 0;//可能会出现持续到战斗结束的反伤//只有物理反伤
    public int Dodge = 0;//可能会出现持续到战斗结束的闪避，两种类型的闪避不要合并显示//物理魔法都受
    public bool DamageToMana = false;
    public bool BeAtkAddMana = false;
    public bool DamageAddAtk = false;//ToDo 结算有问题
    public bool CostNoMana = false;
    public int DoubleAtkOnce = 0;
    public int DoubleAtk = 0;
    public int AddAtk = 0;
    public bool Expensive = false;

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
        

    //这部分还没有展示
    public int AddActPoint(int value){
        ActPoint += value;
        return value;
    }

    public int CostActPoint(int value){
        int v = Mathf.Min(value, ActPoint);
        ActPoint -= value;
        return v;
    }

    public void InitBattleBuff(){
        Shield = 0;
        HealPerRound = 0;
        Poison = 0;
        ClearRoundBuff();
    }

    public void ClearRoundBuff(){
        Armor = 0;
        PhysicalReduction = 0;
        MagicalReduction = 0;
        Sunder = 0;
        Weak = 0;
        Rebound = 0;
        Dodge = 0;
        DamageToMana = false;
        BeAtkAddMana = false;
        DamageAddAtk = false;
        CostNoMana = false;
        DoubleAtkOnce = 0;
        DoubleAtk = 0;
        AddAtk = 0;

        if (Poison > 0)
        {
            Damage(Poison);
            Poison--;
        }
        if (HealPerRound > 0)
        {
            Heal(HealPerRound);
        }


        View.UpdateBuffShow();
    }

}

//暂无引用
public enum PlayerInfo{
    Player,
    Enemy,
    Puppet
}


