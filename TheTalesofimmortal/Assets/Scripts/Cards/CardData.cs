
using System.Collections.Generic;
public class CardData {

    public int Id;
    public string Name;
    public string Description;
    public int Price;

    public int Level;
    public int MaxLevel;
    public int DecayTo;
    public int Tier;
	public CardType Type;
	public CardPlayCondition Condition;
	public CardEffect[] Effects;
}

public enum CardType{
	Physical,
	Magical,
	Mana,
	Trigger,
}

public enum CardPlayCondition{
	None,
	EnemyHpNoMoreThanFive,
	EnemyHpNoMoreThanTwelve,
}



public class CardBuff{
	public CardBuffType Type;
	public string Name;
	public string Description;
//	public string ImagePath;在configs里配一遍或读txt配置
	public int StackType;
	public int Plies;
	public int Duration;
}
	
public enum CardBuffType{
	None,
	Shield,//生命盾v,叠加层数
	Armor1,//护甲v,
    Armor2,
    Armor3,
	PhysicalReduction,//外伤减免%
	MagicalReduction,//内伤减免%
	HealPerRound,//每回合回血v
	Poison,//中毒v
	Sunder,//破甲v
	Weak,//虚弱v
	Deceleration,//减速减少每回合抽卡数v
	Thorns,//荆棘反伤
	Dodge,//闪避
    DamageChangeToMana,//内力护盾
}


//Trigger类型的可以保留
public enum SkillTriggerType{
	None,
	Dead,
	EnemyPlayPhysicalCard,
	EnemyPlayMagicalCard,
	EnemyPlayManaCard,
	EnemyPlayTriggerCard,
}


public enum AttackType{
	Physical,
	Magical,
}
