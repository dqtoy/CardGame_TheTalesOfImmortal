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
    Heal,//治疗类
	PhysicalDamage,//外功伤害
	MagicalDamage,//内功伤害
    DirectDamage,//精神伤害

    AddMp=10,//增加内力
	ReduceMana,//减少内力
	ClearMana,//清除内力
    AddMpAndDmgMag,//增加内力并内功伤害 内力|伤害

	AddBuff=20,//添加buff
	DoublePoison,//加倍中毒，最多不超过10层
	
	Summon=30,//召唤，1|2 puppetId|puppetId
	
	DrawCard=40,//抽n张卡
	RemoveCard,//移除n张卡牌
    AddCardToHand,//添加卡牌到手牌，id|数量
    AddCardToLibrary,//添加卡牌到牌库
	
	InstantKill=50,//直接杀死
	
	AddTriggerSkill=60,//添加触发类技能
	
	KillEnemyPuppet=70,//击杀n个敌方召唤物，参数 1，2，3 99所有
	KillMyPuppet,//击杀n个我方召唤物 参数 1，2，3 99所有

    AddActPoint=80,//添加行动点
    ClearActPoint,//清除行动点

    CountDmgPhy=200,//外功计数攻击，参数：基础伤害|计数类型|每个值的伤害，0拳1剑2格挡
    DmgCostAllAct,//消耗所有行动点造成伤害，参数：基础伤害|每行动点伤害
    DmgCostAllShield,//消耗所有格挡造成伤害，参数：基础伤害|每点格挡造成的伤害

    DmgMagAndHeal=300,//内功伤害并治疗自己 伤害|治疗
    DmgMagAndDraw,//内功伤害并抽卡

}
