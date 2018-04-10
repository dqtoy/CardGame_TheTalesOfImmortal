using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectHandler {
	//1.治疗
	int Heal(Player attacker,Player defender, string param){
		int value = int.Parse (param);
		attacker.Hp += value;
		return value;
	}

	//2.外伤
	int PhysicalDamage(Player attacker,Player defender,string param){
		int value = int.Parse (param);
		//物理减免
		if (defender.PhysicalReduction > 0)
			value -= value * defender.PhysicalReduction / 100;
		//减伤盾
		if(defender.Armor>0)
			value -= defender.Armor;
		//生命盾
		if (value > 0 && defender.PlayerBuff.ContainsKey (CardBuffType.Shield))
			DamageAfterShield (defender, ref value);

		value = (value < 0) ? 0 : value;

		//造成伤害
		if (value > 0)
			ExcuteDamage (defender, value);

		return value;
	}

	//3. 内伤
	int MagicalDamage(Player attacker,Player defender,string param){
		int value = int.Parse (param);
		defender.Hp -= value;
		//物理减免
		if (defender.MagicalReduction > 0)
			value -= value * defender.MagicalReduction / 100;
		//减伤盾
		if(defender.Armor>0)
			value -= defender.Armor;
		//生命盾
		if (value > 0 && defender.PlayerBuff.ContainsKey (CardBuffType.Shield))
			DamageAfterShield (defender, ref value);

		value = (value < 0) ? 0 : value;

		//造成伤害
		if (value > 0)
			ExcuteDamage (defender, value);

		return value;
	}

	//10. 增加内力
	int AddMana(Player attacker,Player defender,string param){
		int value = int.Parse(param);
		ExcuteMana(defender, value);
		return value;
	}

	//11. 扣除魔法
	int ReduceMana(Player attacker,Player defender,string param){
		int value = int.Parse(param);
		ExcuteMana(defender, -value);
		return value;
	}

	//12. 吸魔法
	int AbsorbMana(Player attacker,Player defender,string param){
		int value = int.Parse(param);
		ExcuteMana(attacker, value);
		ExcuteMana(defender, -value);
		return value;
	}

	//13. 清除魔法
	int ClearMana(Player attacker,Player defender,string param){
		int value = defender.Mp;
		ExcuteDamage(defender, -value);
		return value;
	}

	//20. 添加buff，如果buff增加属性，则将属性加上
	int AddBuff(Player attacker,Player defender,string param){
		CardBuffType buff = (CardBuffType)(int.Parse(param));
		return 1;
	}

	//21. 加倍中毒效果
	int DoublePoison(Player attacker,Player defender,string param){
		int count = 0;
		if (defender.PlayerBuff.ContainsKey(CardBuffType.Poison))
		{
			count = defender.PlayerBuff[CardBuffType.Poison];
			defender.PlayerBuff[CardBuffType.Poison] *= 2;
		}
		return count;
	}

	//30. 召唤
	int Summon(Player attacker,Player defender,string param){
		int puppetId = int.Parse(param);
		return 1;
	}

	//40. 抽卡
	int DrawCard(Player attacker,Player defender,string param){
		int count = int.Parse(param);

		return count;
	}

	//41. 移除卡
	int RemoveCard(Player attacker,Player defender,string param){
		int count = int.Parse(param);

		return count;
	}

	//50. 直接杀死
	int InstantKill(Player attacker,Player defender,string param){
		int value = defender.Hp;
		ExcuteDamage(defender, value);
		return value;
	}

	//51. 吞噬
	int Devour(Player attacker,Player defender,string param){
		return 0;
	}

	//60. 增加触发技能
	int AddTriggerSkill(Player attacker,Player defender,string param){

		return 0;
	}

	//500. 击杀敌方召唤物
	int KillEnemyPuppet(Player attacker,Player defender,string param){
		int count = int.Parse (param);
		for (int i = 0; i < count; i++) {
			if (defender.Puppets.Count > 0) {
				int n = Random.Range (0, defender.Puppets.Count);
				KillPuppet (defender, n);
			} else {
				return 0;
			}
		}
		return 0;
	}

	//501. 击杀我方召唤物
	int KillMyPuppet(Player attacker,Player defender,string param){
		int count = int.Parse (param);
		for (int i = 0; i < count; i++) {
			if (attacker.Puppets.Count > 0) {
				int n = Random.Range (0, attacker.Puppets.Count);
				KillPuppet (defender, n);
			} else {
				return 0;
			}
		}
		return 0;
	}

	//502. 伤害敌方召唤物
	int DamageEnemyPuppet(Player attacker,Player defender,string param){
		string[] s = param.Split ('|');
		int count = int.Parse (s [0]);
		int damage = int.Parse (s [1]);



		return 0;
	}

	//503. 伤害我方召唤物
	int DamageMyPuppet(Player attacker,Player defender,string param){
		string[] s = param.Split ('|');
		int count = int.Parse (s [0]);
		int damage = int.Parse (s [1]);



		return 0;
	}


	//计算扣除护盾后的伤害
	void DamageAfterShield(Player player, ref int damage){
		if (damage < player.PlayerBuff [CardBuffType.Shield]) {
			damage = 0;
			player.PlayerBuff [CardBuffType.Shield] -= damage;
		} else {
			damage -= player.PlayerBuff [CardBuffType.Shield];
			RemoveBuff (player, CardBuffType.Shield);
		}
	}

	void KillPuppet(Player target, int index){
		target.Puppets [index].Hp = 0;
		RemovePuppet (target, index);
	}

	void ExcuteDamageToPuppet(Player target,int index,int damage){
		target.Puppets [index].Hp -= damage;
		if (target.Puppets [index].Hp <= 0)
			RemovePuppet (target, index);
	}

	void RemovePuppet(Player target, int index){
		target.Puppets.RemoveAt (index);
	}

	void ExcuteDamage(Player target,int value){
		target.Hp -= value;
	}

	void ExcuteMana(Player target, int value){
		target.Mp += value;
	}


	//移除buff，如果buff加属性，则要将属性也移除
	void RemoveBuff(Player target, CardBuffType buff){
		if (target.PlayerBuff.ContainsKey(buff))
		{
			switch (buff)
			{
				case CardBuffType.Armor1:
					break;
				case CardBuffType.Armor2:
					break;
				case CardBuffType.Armor3:
					break;
			}
			target.PlayerBuff.Remove(buff);
		}
	}

}
