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
	Weak,//虚弱 类型|层数|持续回合
	Acceleration,//加速 类型|层数|持续回合
	Deceleration,//减速 类型|层数|持续回合
	Rebound,//荆棘,物理反伤 类型|层数|持续回合
	Dodge,//闪避，闪避内伤和外伤 类型|层数|持续回合
    DamageToMana,//内力护盾
    //伤转攻
}
