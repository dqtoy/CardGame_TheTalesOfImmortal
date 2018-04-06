using System.Collections.Generic;
public class Card {

    public int Id;
    public string Name;
    public string Description;
    public int Price;

    public int Level;
    public int MaxLevel;
    public int DecayTo;
    public int Tier;

	public CardEffectType Effect;
	public string EffectParam;//目标，效果参数，持续回合/叠加层数
 
	public virtual void Play(Player me,Player enemy){
		
	}

}

public class PhysicalAtkCard:Card{

}

public class MagicalAtkCard:Card{
	private int mana;
	public int ManaCost{
		get{ return mana;}
		set{ mana = value;}
	}
}

public class ManaCard:Card{
	public int AddMana;
}

public class TriggerCard:Card{
	public int TriggerId;
}

public class CardBuff{
	public CardBuffType BuffType;
	public string Name;
	public string Description;
	public string ImagePath;
	public int Plies;//层数

	public CardBuff(int id,int plies){
		BuffType = (CardBuffType)id;
		Plies = plies;
	}
}

//每个BuffType有对应的描述和图片
public enum CardBuffType{
	None,
	HpShield,//生命盾v
	Armor,//护甲v
	PhysicalReduction,//物理减免%
	MagicalReduction,//魔法减免%
	Heal,//每回合回血v
	Poison,//中毒v
	Sunder,//破甲v
	Weak,//虚弱v
	Deceleration,//减速减少每回合抽卡数v
}

public enum CardTargetType{
	None,
	Me,
	MyPuppet,
	Enemy,
	EnemyPuppet,
	All,
}

public enum CardEffectType{
	None,
	Heal,//直接治疗，用于buff
	Damage,//直接伤害，用于buff
	DrawCard,
	Fall,
	Fly,
	Back,
	AddHpShield,//生命盾
	AddArmor,//减伤盾(按点数)
	AddPhysicalReduction,//减伤盾（按比例）
	AddMagicalReduction,//减伤盾（按比例）
	AddHeal,
	AddPoison,
	DoublePoison,
	AddSunder,//破甲
	AddWeak,
	InstantKill,
	Devour,
	Revive,//复活
}

public enum CardTriggerType{
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

