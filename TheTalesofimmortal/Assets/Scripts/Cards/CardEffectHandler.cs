using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectHandler {
	//1.对自己治疗
	int Heal(Player attacker,Player defender, string param){
		int value = int.Parse (param);
		attacker.Hp += value;
		return value;
	}

	//2.对敌人外伤
	int PhysicalDamage(Player attacker,Player defender,string param){
		//闪避
		if (MathCalculation.IsDodge (defender.Dodge)) {
			return 0;
		}

		int value = int.Parse (param);
		//虚弱
		value -= attacker.Weak;
		//易伤
		value+=defender.Sunder;
		//减伤盾
		value -= defender.Armor;
		//物理减免
		value -= value * defender.PhysicalReduction / 100;

		//生命盾
        if (value > 0 && defender.Shield>0)
			DamageAfterShield (defender, ref value);

		value = Mathf.Max (0, value);

		//内力盾
		if (defender.DamageToMana) {
			int m = value > defender.Mp ? defender.Mp : value;
			value -= m;
			ExcuteMana (defender, -m);
		}

		//造成伤害
		if (value > 0)
			ExcuteDamage (defender, value);

		//物理反伤
		if(defender.Rebound>0){
			int damage = value * defender.Rebound / 100;
			ExcuteReboundDamage (attacker, value);
		}

		return value;
	}

	//3. 对敌人内伤
	int MagicalDamage(Player attacker,Player defender,string param){

		//闪避
		if (MathCalculation.IsDodge (defender.Dodge)) {
			return 0;
		}

		int value = int.Parse (param);
		defender.Hp -= value;
		//虚弱
		value -= attacker.Weak;
		//易伤
		value+=defender.Sunder;
		//减伤盾
		value -= defender.Armor;
		//魔法减免
		value -= value * defender.MagicalReduction / 100;

		//生命盾
		if (value > 0 && defender.Shield>0)
			DamageAfterShield (defender, ref value);

		value = Mathf.Max (0, value);

		//内力盾
		if (defender.DamageToMana) {
			int m = value > defender.Mp ? defender.Mp : value;
			value -= m;
			ExcuteMana (defender, -m);
		}

		//造成伤害
		if (value > 0)
			ExcuteDamage (defender, value);

		return value;
	}

    //4. 对自己造成真实伤害
    int SelfDamage(Player attacker,Player defender,string param){
        int value = int.Parse (param);
        ExcuteDamage (attacker, value);
        return value;
    }

	//10. 增加自己内力
	int AddMana(Player attacker,Player defender,string param){
		int value = int.Parse(param);
		ExcuteMana(defender, value);
		return value;
	}

	//11. 扣除敌人魔法
	int ReduceMana(Player attacker,Player defender,string param){
		int value = int.Parse(param);
		ExcuteMana(defender, -value);
		return value;
	}

	//12. 吸敌人魔法
	int AbsorbMana(Player attacker,Player defender,string param){
		int value = int.Parse(param);
		ExcuteMana(attacker, value);
		ExcuteMana(defender, -value);
		return value;
	}

	//13. 清除敌人魔法
	int ClearMana(Player attacker,Player defender,string param){
		int value = defender.Mp;
		ExcuteDamage(defender, -value);
		return value;
	}

	//20. 给敌人添加buff，如果buff增加属性，则将属性加上
	int AddBuff(Player attacker,Player defender,string param){
        ExcuteAddBuff(defender, param);
		return 0;
	}

    //21. 给自己添加buff，如果buff增加属性，则将属性加上
    int SelfAddBuff(Player attacker,Player defender,string param){
        ExcuteAddBuff(attacker, param);
        return 0;
    }

	//22. 加倍中毒效果
	int DoublePoison(Player attacker,Player defender,string param){
        defender.Poison *= 2;
		return 0;
	}

	//30. 召唤
	int Summon(Player attacker,Player defender,string param){
		string[] s;
		if (param.Contains ("|")) {
			s = param.Split ('|');
		} else {
			s = new string[]{ param };
		}
		for (int i = 0; i < s.Length; i++) {
			Puppet p = new Puppet (int.Parse (s [i]));
			attacker.Puppets.Add (p);
		}
		return 1;
	}

	//40. 抽卡
	//To Do
	int DrawCard(Player attacker,Player defender,string param){
		int count = int.Parse(param);

		return count;
	}

	//41. 移除卡
	//To Do
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
		for (int i = 0; i < defender.Puppets.Count; i++) {
//			ExcuteDamageToPuppet(
			//这个地方有待优化，传过去puppet，在这里判断是不是死了。
		}


		return 0;
	}

	//503. 伤害我方召唤物
	int DamageMyPuppet(Player attacker,Player defender,string param){
		string[] s = param.Split ('|');
		int count = int.Parse (s [0]);
		int damage = int.Parse (s [1]);



		return 0;
	}

    //504. 给我方召唤物增加攻击
    int AddAtkToMyPuppets(Player attacker,Player defender,string param){
        return 0;
    }

    //505. 给我方召唤物增加攻击
    int ReduceAtkToEnemyPuppets(Player attacker,Player defender,string param){
        return 0;
    }

	//计算扣除护盾后的伤害
	void DamageAfterShield(Player player, ref int damage){
		if (damage < player.Shield) {
			damage = 0;
            player.Shield -= damage;
		} else {
			damage -= player.Shield;
			//Remove ShieldView
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

	void ExcuteReboundDamage(Player target,int value){
		//易伤
		value+=target.Sunder;
		//减伤盾
		value -= target.Armor;

		//生命盾
		if (value > 0 && target.Shield>0)
			DamageAfterShield (target, ref value);

		value = Mathf.Max (0, value);

		if (target.DamageToMana) {
			int m = Mathf.Min (value, target.Mp);
			value -= m;
			ExcuteMana (target, -m);
		}

		if (value > 0)
			ExcuteDamage (target, value);
	}

	void ExcuteMana(Player target, int value){
		target.Mp += value;
	}

    void ExcuteAddBuff(Player target,string param){
        CardBuffType buff;
		int layer = 0;
		int duration = 0;
        if (param.Contains("|"))
        {
            string[] s = param.Split('|');
            buff = (CardBuffType)(int.Parse(s[0]));
            layer = int.Parse(s[1]);
			if (s.Length > 2)
				duration = int.Parse (s [2]);
        }
        else
        {
            buff = (CardBuffType)(int.Parse(param));
        }

        switch (buff)
        {
			case CardBuffType.None:
				break;
			case CardBuffType.Shield:
				target.Shield += layer;
				break;
			case CardBuffType.Armor:
				target.Armor += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Armor, layer, duration));
                break;
			case CardBuffType.PhysicalReduction:
				target.PhysicalReduction = MathCalculation.PropDecayIncrease (target.PhysicalReduction, layer);
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.PhysicalReduction, layer, duration));
                break;
            case CardBuffType.MagicalReduction:
				target.MagicalReduction = MathCalculation.PropDecayIncrease (target.MagicalReduction, layer);
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.MagicalReduction, layer, duration));
                break;
			case CardBuffType.HealPerRound:
				target.HealPerRound += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.HealPerRound, layer, duration));
				break;
			case CardBuffType.Poison:
				target.Poison += layer;
				break;
			case CardBuffType.Sunder:
				target.Sunder += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Sunder, layer, duration));
				break;
			case CardBuffType.Weak:
				target.Weak += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Weak, layer, duration));
				break;
			case CardBuffType.Acceleration:
				target.Acceleration += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Acceleration, layer, duration));
				break;
			case CardBuffType.Deceleration:
				target.Deceleration += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Deceleration, layer, duration));
				break;
			case CardBuffType.Rebound:
				target.Rebound += layer;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Rebound, layer, duration));
				break;
			case CardBuffType.Dodge:
				target.Dodge = MathCalculation.PropDecayIncrease (target.Dodge, layer);
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.Dodge, layer, duration));
				break;
			case CardBuffType.DamageToMana:
				target.DamageToMana = true;
				target.PlayerBuffs.Add (new CardBuff (CardBuffType.DamageToMana, layer, duration));
				break;
			default:
				Debug.Log ("Can not find CardBuffType!");
				break;
        }
    }



	//某个buff被移除，如果buff加属性，则要将属性也移除
	void BuffRemoved(Player target, CardBuff buff){
		switch (buff.Type) {
			case CardBuffType.Armor:
				target.Armor -= buff.Layers;
				break;
			case CardBuffType.PhysicalReduction:
				target.PhysicalReduction = MathCalculation.PropDecayDecrease (target.PhysicalReduction, buff.Layers);
				break;
			case CardBuffType.MagicalReduction:
				target.MagicalReduction = MathCalculation.PropDecayDecrease (target.MagicalReduction, buff.Layers);
				break;
			case CardBuffType.HealPerRound:
				target.HealPerRound -= buff.Layers;
				break;
			case CardBuffType.Sunder:
				target.Sunder -= buff.Layers;
				break;
			case CardBuffType.Weak:
				target.Weak -= buff.Layers;
				break;
			case CardBuffType.Acceleration:
				target.Acceleration -= buff.Layers;
				break;
			case CardBuffType.Deceleration:
				target.Deceleration -= buff.Layers;
				break;
			case CardBuffType.Rebound:
				target.Rebound -= buff.Layers;
				break;
			case CardBuffType.Dodge:
				target.Dodge = MathCalculation.PropDecayDecrease (target.Dodge, buff.Layers);
				break;
			case CardBuffType.DamageToMana:
				target.DamageToMana = IsDamageToMana (target);
				break;
			default:
				break;
		}
	}

	bool IsDamageToMana(Player target){
		foreach(CardBuff buff in target.PlayerBuffs){
			if (buff.Type == CardBuffType.DamageToMana)
				return true;
		}
		return false;
	}

}
