
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
	HpNoMoreThanFive,
	HpNoMoreThanTwelve,
}

public class CardEffect{
	public int Id;
	public CardSkillTarget Target;
	public CardEffectType Type;
	public string Param;
}

public enum CardSkillTarget{
	None,
	Me,
	MyPuppet,
	AllMyPuppets,
	Enemy,
	EnemyPuppet,
	AllEnemyPuppets,
	All,
}

public enum CardEffectType{
	None,
	Heal,
	PhysicalDamage,
	MagicalDamage,
	AddMana,
	ReduceMana,
	ClearMana,
	AddBuff,
	Summon,
	DrawCard,
	TakeCard,
	DoublePoison,
	InstantKill,
	Devour,
	AddTriggerSkill,
}

public class CardBuff{
	public int Id;
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
	Shield,//生命盾v
	Armor,//护甲v
	PhysicalReduction,//物理减免%
	MagicalReduction,//魔法减免%
	HealPerRound,//每回合回血v
	Poison,//中毒v
	Sunder,//破甲v
	Weak,//虚弱v
	Deceleration,//减速减少每回合抽卡数v
	Thorns,//荆棘反伤
	Dodge,//闪避
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
