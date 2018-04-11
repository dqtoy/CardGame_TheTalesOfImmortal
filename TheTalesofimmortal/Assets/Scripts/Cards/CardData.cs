
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
