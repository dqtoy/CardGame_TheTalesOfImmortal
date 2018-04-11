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
    Shield,//生命盾v,叠加层数
    Armor,//护甲v,
    PhysicalReduction,//外伤减免50%
    MagicalReduction,//内伤减免50%
    HealPerRound,//每回合结束回血v
    Poison,//中毒v
    Sunder,//破甲v
    Weak,//虚弱v
    Acceleration,
    Deceleration,//减速减少每回合抽卡数v
    Rebound,//荆棘反伤百分比50%
    Dodge,//闪避百分比50%
    DamageToMana,//内力护盾
}
