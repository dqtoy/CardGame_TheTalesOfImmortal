using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect{
	public int Id;
	public CardEffectType Type;
	public string Param;
}
	
public enum CardEffectType{
	None,
	//治疗类
	Heal,
	//伤害类
	PhysicalDamage,
	MagicalDamage,
    DirectDamage,//直接伤害
	//内力类
    AddMp=10,
	ReduceMana,
	ClearMana,
	//Buff类
	AddBuff=20,
	DoublePoison,//加倍中毒，最多不超过10层
	//召唤
	Summon=30,//1|2 puppetId|puppetId
	//卡牌类
	DrawCard=40,
	RemoveCard,
    AddCardToHand,//id|数量
    AddCardToLibrary,
	//直接灭杀
	InstantKill=50,
	//技能
	AddTriggerSkill=60,
	//针对召唤物
	KillEnemyPuppet=500,//参数 1，2，3 99所有
	KillMyPuppet,//参数 1，2，3 99所有
}
