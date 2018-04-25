using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectHandler {
	//1.治疗,返回实际加血量
    int Heal(Player attacker, string param,Target target){
		int value = int.Parse (param);
        value = target.Heal(value);
		return value;
	}

	//2.外伤,返回伤害值
    int PhysicalDamage(Player attacker,string param,Target target){
		//闪避
        if (MathCalculation.IsDodge (target.Dodge)) {
			return 0;
		}

		int value = int.Parse (param);
		//虚弱
		value -= attacker.Weak;
		//易伤
        value+=target.Sunder;
		//减伤盾
        value -= target.Armor;
		//物理减免
        value -= value * target.PhysicalReduction / 100;

		//生命盾
        if (value > 0 && target.Shield>0)
            DamageAfterShield (target, ref value);

		value = Mathf.Max (0, value);

        //物理反伤(有可能会双方同时死亡)，注意反伤可能会跟内力盾冲突
        if(target.Rebound>0){
            int damage = value * target.Rebound / 100;
            ExcuteReboundDamage (attacker, damage);
        }

		//内力盾
        if (target.DamageToMana) {
            value -= target.DamageMp(value);
		}

		//造成伤害
        if (value > 0)
            target.Damage(value);

		return value;
	}

	//3. 对敌人内伤
    int MagicalDamage(Player attacker,string param,Target target){

		//闪避
		if (MathCalculation.IsDodge (target.Dodge)) {
			return 0;
		}

		int value = int.Parse (param);

		//虚弱
		value -= attacker.Weak;
		//易伤
        value+=target.Sunder;
		//减伤盾
        value -= target.Armor;
		//魔法减免
        value -= value * target.MagicalReduction / 100;

		//生命盾
        if (value > 0 && target.Shield>0)
            DamageAfterShield (target, ref value);

		value = Mathf.Max (0, value);

		//内力盾
        if (target.DamageToMana) {
            value -= target.DamageMp(value);
        }

		//造成伤害
		if (value > 0)
            target.Damage(value);

		return value;
	}

    //4. 对自己造成真实伤害
    int DirectDamage(Player attacker,string param,Target target){
        int value = int.Parse (param);
        target.Damage(value);
        return value;
    }

	//10. 增加内力
    int AddMp(Player attacker,string param,Target target){
		int value = int.Parse(param);
        value = target.HealMp(value);
		return value;
	}

	//11. 扣除内力
    int ReduceMana(Player attacker,string param,Target target){
		int value = int.Parse(param);
        value = target.DamageMp(value);
		return value;
	}
        

	//12. 清除敌人魔法
    int ClearMana(Player attacker,string param,Target target){
        int value = target.MP;
        target.DamageMp(value);
		return value;
	}

	//20. 添加buff，如果buff增加属性，则将属性加上
    int AddBuff(Player attacker,string param,Target target){
        ExcuteAddBuff(target, param);
		return 0;
	}
        
    //21. 加倍中毒效果，最多不超过10层
    int DoublePoison(Player attacker,string param,Target target){
        int value = Mathf.Min(10, target.Poison);
        target.Poison += value;
        return value;
    }

	//30. 召唤
    int Summon(Player attacker,string param,Target target){
        Player dealer = (Player)target;
		string[] s;
		if (param.Contains ("|")) {
			s = param.Split ('|');
		} else {
			s = new string[]{ param };
		}
		for (int i = 0; i < s.Length; i++) {
			Puppet p = new Puppet (int.Parse (s [i]));
			dealer.Puppets.Add (p);
		}
		return 1;
	}

	//40. 抽卡
	//To Do
    int DrawCard(Player attacker,string param,Target target){
        Player dealer = (Player)target;
        int count = int.Parse(param);
        dealer.DrawCards(count);
		return count;
	}

	//41. 移除卡
	//To Do
    int RemoveCard(Player attacker,string param,Target target){
        Player dealer = (Player)target;
        int count = int.Parse(param);
        dealer.RemoveRandomCard(count);
		return count;
	}

    //42. 将特定卡加至手牌
    int AddCardToHand(Player attacker,string param,Target target){
        Player dealer = (Player)target;
        
		GameObject g = Resources.Load ("card") as GameObject;
		Card card = g.GetComponent<Card> ();
        int count = 1;
		if (param.Contains ("|")) {
			string[] s = param.Split ('|');
			card.Init (int.Parse (s [0]));
			count = int.Parse (s [1]);
		} else {
			card.Init (int.Parse (param));
		}
        dealer.AddCardToHand(card,count);
        return count;
    }

    //43. 将特定卡加至牌库
    int AddCardToLibrary(Player attacker,string param,Target target){
        Player dealer = (Player)target;
		GameObject g = Resources.Load ("card") as GameObject;
		Card card = g.GetComponent<Card> ();
        int count = 1;
        if (param.Contains("|"))
        {
            string[] s = param.Split('|');
			card.Init(int.Parse(s[0]));
            count = int.Parse(s[1]);
        }
        else
            card.Init(int.Parse(param));
        dealer.AddCardToLibrary(card,count);
        return count;
    }


	//50. 直接杀死
    int InstantKill(Player attacker,string param,Target target){
        return Kill(target);
	}

	//60. 增加触发技能
    int AddTriggerSkill(Player attacker,string param,Target target){

		return 0;
	}

	//70. 击杀敌方召唤物
    int KillEnemyPuppet(Player attacker,string param,Target target){
        Player dealer = (Player)target;
		int count = int.Parse (param);
        KillPuppet(dealer, count);
		return 0;
	}

	//71. 击杀我方召唤物
    int KillMyPuppet(Player attacker,string param,Target target){
        Player dealer = (Player)target;
		int count = int.Parse (param);
        KillPuppet(dealer, count);
		return 0;
	}

    void KillPuppet(Player target,int count){
        for (int i = 0; i < count; i++) {
            if (target.Puppets.Count > 0) {
                int n = Random.Range (0, target.Puppets.Count);
                Kill(target.Puppets[n]);
            } 
        }
    }

    int Kill(Target target){
        int value = target.HP;
        target.Damage(value);
        return value;
    }


	//计算扣除护盾后的伤害
    void DamageAfterShield(Target target, ref int damage){
        if (damage < target.Shield) {
			damage = 0;
            target.Shield -= damage;
		} else {
            damage -= target.Shield;
			//Remove ShieldView
		}
	}
        

    void ExcuteReboundDamage(Target target,int value){
		//易伤
		value+=target.Sunder;
		//减伤盾
		value -= target.Armor;

		//生命盾
		if (value > 0 && target.Shield>0)
			DamageAfterShield (target, ref value);

		value = Mathf.Max (0, value);

		if (target.DamageToMana) {
            value -= target.DamageMp(value);
		}

        if (value > 0)
            target.Damage(value);
	}



    void ExcuteAddBuff(Target target,string param){
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
				target.Buffs.Add (new CardBuff (CardBuffType.Armor, layer, duration));
                break;
			case CardBuffType.PhysicalReduction:
				target.PhysicalReduction = MathCalculation.PropDecayIncrease (target.PhysicalReduction, layer);
				target.Buffs.Add (new CardBuff (CardBuffType.PhysicalReduction, layer, duration));
                break;
            case CardBuffType.MagicalReduction:
				target.MagicalReduction = MathCalculation.PropDecayIncrease (target.MagicalReduction, layer);
				target.Buffs.Add (new CardBuff (CardBuffType.MagicalReduction, layer, duration));
                break;
			case CardBuffType.HealPerRound:
				target.HealPerRound += layer;
				target.Buffs.Add (new CardBuff (CardBuffType.HealPerRound, layer, duration));
				break;
			case CardBuffType.Poison:
				target.Poison += layer;
				break;
			case CardBuffType.Sunder:
				target.Sunder += layer;
				target.Buffs.Add (new CardBuff (CardBuffType.Sunder, layer, duration));
				break;
			case CardBuffType.Weak:
				target.Weak += layer;
				target.Buffs.Add (new CardBuff (CardBuffType.Weak, layer, duration));
				break;
			case CardBuffType.Acceleration:
				target.Acceleration += layer;
				target.Buffs.Add (new CardBuff (CardBuffType.Acceleration, layer, duration));
				break;
			case CardBuffType.Deceleration:
				target.Deceleration += layer;
				target.Buffs.Add (new CardBuff (CardBuffType.Deceleration, layer, duration));
				break;
			case CardBuffType.Rebound:
				target.Rebound += layer;
				target.Buffs.Add (new CardBuff (CardBuffType.Rebound, layer, duration));
				break;
			case CardBuffType.Dodge:
				target.Dodge = MathCalculation.PropDecayIncrease (target.Dodge, layer);
				target.Buffs.Add (new CardBuff (CardBuffType.Dodge, layer, duration));
				break;
			case CardBuffType.DamageToMana:
				target.DamageToMana = true;
				target.Buffs.Add (new CardBuff (CardBuffType.DamageToMana, layer, duration));
				break;
			default:
				Debug.Log ("Can not find CardBuffType!");
				break;
        }
    }



	//某个buff被移除，如果buff加属性，则要将属性也移除
    void BuffRemoved(Target target, CardBuff buff){
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

    bool IsDamageToMana(Target target){
		foreach(CardBuff buff in target.Buffs){
			if (buff.Type == CardBuffType.DamageToMana)
				return true;
		}
		return false;
	}

}
