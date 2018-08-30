using System;


public class CardBuff
{
    public CardBuffType Type;
    public int Layers;
    public int Duration;

    public CardBuff(CardBuffType type,int layers,int duration){
        Type = type;
        Layers = layers;
        Duration = duration;
    }
}

public enum CardBuffType{
    None,//下列格式：类型|参数（int）
    Shield,//生命盾 类型|层数
    Armor,//护甲 类型|层数|持续回合
	PhysicalReduction,//外伤减免 类型|层数|持续回合
	MagicalReduction,//内伤减免 类型|层数|持续回合
	HealPerRound,//每回合结束回血 类型|层数|持续回合
	Poison,//中毒 类型|层数
	Sunder,//易伤 类型|层数|持续回合
	Weak,//虚弱，降低攻击 类型|层数|持续回合
	Rebound,//荆棘,反伤 类型|层数|持续回合
	Dodge,//闪避，闪避内伤和外伤 类型|层数|持续回合
    DamageToMana,//内力护盾，到下回合受伤由内力承担，直到内力耗尽
    DamageAddMana,//受到伤害转换为内力
    DamageAddAtk,//受到伤害转换为攻
    CostNoMana,//本回合不消耗内力
    DoubleAtkOnce,//下次攻击翻倍
    DoubleAtk,//本回合攻击翻倍
    AddAtk,//伤害增强，持续到本场战斗结束，类型|值
    Expensive,//所有内力攻击消耗+2，效果翻倍
}
