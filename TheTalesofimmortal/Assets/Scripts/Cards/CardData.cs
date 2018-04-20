
using System.Collections.Generic;
public class CardData {

    public int Id;
    public string Name;
    public string Description;
    public int Price;

    public int Level;
    public int MaxLevel;
    public int MpCost;
	public CardType Type;
	public CardPlayCondition Condition;
	public CardEffect[] Effects;
}

public enum CardType{
	Physical,
	Magical,
	Mp,
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
    
    

