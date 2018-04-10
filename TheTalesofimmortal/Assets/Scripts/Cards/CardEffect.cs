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
	//内力类
	AddMana=10,
	ReduceMana,
	AbsorbMana,
	ClearMana,
	//Buff类
	AddBuff=20,
	DoublePoison,
	//召唤
	Summon=30,
	//卡牌类
	DrawCard=40,
	RemoveCard,

	//直接灭杀
	InstantKill=50,
	Devour,
	//技能
	AddTriggerSkill=60,
	//针对召唤物
	KillEnemyPuppet=500,//参数 1，2，3 99所有
	KillMyPuppet,//参数 1，2，3 99所有
	DamageEnemyPuppets,//参数 数量|值
	DamageMyPuppets,//参数 数量|值
	AddAtkToMyPuppets,//参数 数量|值
	ReduceAtkToEnemyPuppets,//参数 数量|值
}
