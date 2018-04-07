
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

	public int[] Effects;
}

public class CardEffect{
	public int Id;
	public CardEffectType Type;
	public string EffectParam;
}

public class CardBuff{
	public int Id;
	public CardBuffType Type;
	public string Name;
	public string Description;
//	public string ImagePath;在configs里配一遍或读txt配置
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
	AddTriggerSkill,
	DrawCard,
	TakeCard,
	DoublePoison,
	InstantKill,
	Devour,
}
	

public enum CardTriggerCondition{
	None,
	EnemyPlayAttackCard,
	EnemyPlayManaCard,
	EnemyPlayTriggerCard,
	Dying,
}

public enum CardPlayCondition{
	None,
	HpNoMoreThanFive,
	HpNoMoreThanTwelve,
}

public enum CardType{
	Physical,
	Magical,
	Mana,
	Trigger,
	Summon,
}

public enum AttackType{
	Physical,
	Magical,
}
