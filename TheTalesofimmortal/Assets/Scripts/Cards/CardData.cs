
using System.Collections.Generic;
public class CardData {

    public int Id;
    public string Name;
    public string Description;
    public int Price;

    public int Level;
    public int MaxLevel;
    public int ActionCost;
    public int MpCost;
	public CardType Type;
	public CardPlayCondition Condition;
	public CardEffect[] Effects;

	public int DefaultTarget;//默认目标 0敌人 1自己 2敌方召唤物	3我方召唤物  (召唤物默认选第1个)
	public int PRI;//优先级 数字越大越优先 不满足条件直接置0
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
    
    

