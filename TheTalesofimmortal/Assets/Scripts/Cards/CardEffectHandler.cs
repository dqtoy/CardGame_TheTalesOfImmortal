using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectHandler {
	public CardEffectHandler(){
	}

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

        //增加攻击
        value += attacker.AddAtk;

        //双倍伤害
        if(attacker.DoubleAtkOnce>0){
            value *= Mathf.Pow(2, attacker.DoubleAtkOnce);

            //这部分要改成removebuff来处理
            attacker.DoubleAtkOnce = 0;
            attacker.View.UpdateBuffs(attacker as Target);
        }
        if (attacker.DoubleAtk > 0)
        {
            value *= Mathf.Pow(2, attacker.DoubleAtk);

            //这部分要改成removebuff来处理
            attacker.DoubleAtkOnce = 0;
            attacker.View.UpdateBuffs(attacker as Target);
        }
        if (attacker.Expensive)
        {
            value *= 2;
        }

		//虚弱
		value -= attacker.Weak;
		//易伤
		value += target.Sunder;
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

        //受攻击加内力
        if (target.BeAtkAddMana)
        {
            target.HealMp(1);
        }

        //累计受到的伤害加攻击，结算时机有问题。而且过于强大，如果一定要加，需要增加概率，并且修改AddAtk结算时机。
        //这个方案如果做成累计受到的所有伤害增加攻击，会被恶意刷。
//        if (target.DamageAddAtk)
//        {
//            
//            target.AddAtk++;
//        }

		return value;
	}

	//3. 内伤
    int MagicalDamage(Player attacker,string param,Target target){

		//闪避
		if (MathCalculation.IsDodge (target.Dodge)) {
			return 0;
		}

		int value = int.Parse (param);

        //增加攻击
        value += attacker.AddAtk;

        //双倍伤害
        if(attacker.DoubleAtkOnce>0){
            value *= Mathf.Pow(2, attacker.DoubleAtkOnce);

            //这部分要改成removebuff来处理
            attacker.DoubleAtkOnce = 0;
            attacker.View.UpdateBuffs(attacker as Target);
        }
        if (attacker.DoubleAtk > 0)
        {
            value *= Mathf.Pow(2, attacker.DoubleAtk);

            //这部分要改成removebuff来处理
            attacker.DoubleAtkOnce = 0;
            attacker.View.UpdateBuffs(attacker as Target);
        }

		//虚弱
		value -= attacker.Weak;
		//易伤
        value += target.Sunder;
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

        //受攻击加内力
        if (target.BeAtkAddMana)
        {
            target.HealMp(1);
        }

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
        

	//12. 清除内力
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
    //清除debuff并根据清除数量造成伤害

	//30. 召唤
    int Summon(Player attacker,string param,Target target){
        Player dealer = (Player)target;
        string[] s = SplitParam(param);
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

    //42. 将特定卡加至目标手牌
    int AddCardToHand(Player attacker,string param,Target target){
        Player dealer = (Player)target;
        
		GameObject g = Resources.Load ("card") as GameObject;
		Card card = g.GetComponent<Card> ();
        int count = 1;
        string[] s = SplitParam(param);
        card.Init (int.Parse (s[0]),(Player)target);
        if (s.Length>1) 
			count = int.Parse (s [1]);
        dealer.AddCardToHand(card,count);
        return count;
    }

    //43. 将特定卡加到目标牌库
    int AddCardToLibrary(Player attacker,string param,Target target){
        Player dealer = (Player)target;
		GameObject g = Resources.Load ("card") as GameObject;
		Card card = g.GetComponent<Card> ();
        int count = 1;

        string[] s = SplitParam(param);
        card.Init (int.Parse (s[0]),(Player)target);
        if (s.Length>1) 
            count = int.Parse (s [1]);
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

    //80. 增加行动点
    int AddActPoint(Player attacker,string param,Target target){
        int value = int.Parse(param);
        value = attacker.AddActPoint(value);
        return value;
    }
    //81. 减少行动点
    int ReduceActPoint(Player attacker,string param,Target target){
        int value = int.Parse(param);
        value = attacker.CostActPoint(value);
        return value;
    }
    //82. 清空行动点
    int ClearActPoint(Player attacker,string param,Target target){
        int value = attacker.ActPoint;
        value = attacker.CostActPoint(value);
        return value;
    }

    //200. CountDmgPhy 外功计数攻击
    int CountDmgPhy(Player attacker,string param,Target target){
        int basicAtk = 0;
        int count = 0;
        int incAtk = 1;
        string[] s = SplitParam(param);
        if(s.Length>=3)
        {
            basicAtk = int.Parse(s[0]);
            int countType = int.Parse(s[1]);

            //ToDo计数
            count = 10;
            incAtk = int.Parse(s[2]);
        }
        int dmg = basicAtk + count * incAtk;
        return PhysicalDamage(attacker, dmg.ToString(), target);
    }

    //201. DmgCostAllAct 消耗所有行动点造成伤害
    int DmgCostAllAct(Player attacker,string param,Target target){
        int basicAtk = 0;
        int incAtk = 0;
        string[] s = SplitParam(param);
        if (s.Length >= 2)
        {
            basicAtk = int.Parse(s[0]);
            incAtk = int.Parse(s[1]);
        }

        int actionPoint = attacker.ActPoint;
        int dmg = basicAtk + actionPoint * incAtk;

        ClearActPoint(attacker, param, target);
        return PhysicalDamage(attacker, dmg.ToString(), target);
    }

    //202. DmgCostAllShield 消耗所有格挡造成伤害
    int DmgCostAllShield(Player attacker,string param,Target target){
        int basicAtk = 0;
        int incAtk = 0;
        string[] s = SplitParam(param);
        if (s.Length >= 2)
        {
            basicAtk = int.Parse(s[0]);
            incAtk = int.Parse(s[1]);
        }

        //Todo 计数shield
        int shieldCount = 5;
        int dmg = basicAtk + shieldCount * incAtk;
        //Todo 清除格挡 ClearSheild
        return PhysicalDamage(attacker, dmg.ToString(), target);
    }

    //300. DmgMagAndHeal 内功伤害并治疗自己
    int DmgMagAndHeal(Player attacker,string param,Target target){
        int atk = 0;
        int heal = 0;
        string[] s = SplitParam(param);
        if (s.Length >= 2)
        {
            atk = int.Parse(s[0]);
            heal = int.Parse(s[1]);
        }
        MagicalDamage(attacker,atk.ToString(),target);
        Heal(attacker,heal.ToString(),target);
        return 0;
    }

    //301. DmgMagAndDraw 内功伤害并抽卡
    int DmgMagAndDrao(Player attacker,string param,Target target){
        int atk = 0;
        int drawCount = 0;
        string[] s = SplitParam(param);
        if (s.Length >= 2)
        {
            atk = int.Parse(s[0]);
            drawCount = int.Parse(s[1]);
        }
        MagicalDamage(attacker,atk.ToString(),target);
        DrawCard(attacker, drawCount.ToString(), target);
        return 0;
    }

    //13. AddMpAndDmgMag 增加内力并内功伤害
    int AddMpAndDmgMag(Player attacker,string param,Target target){
        int addMp = 0;
        int atk = 0;
        string[] s = SplitParam(param);
        if (s.Length >= 2)
        {
            addMp = int.Parse(s[0]);
            atk = int.Parse(s[1]);
        }
        AddMp(attacker, addMp.ToString(), target);
        MagicalDamage(attacker,atk.ToString(),target);
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
            target.View.UpdateBuffs();
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
		int layer = 1;
        if (param.Contains("|"))
        {
            string[] s = param.Split('|');
            buff = (CardBuffType)(int.Parse(s[0]));
            layer = int.Parse(s[1]);
        }
        else
        {
            buff = (CardBuffType)(int.Parse(param));
        }

        bool isBuffExist = false;
        switch (buff)
        {
			case CardBuffType.None:
				break;
			case CardBuffType.Shield:
				target.Shield += layer;
				break;
			case CardBuffType.Armor:
				target.Armor += layer;
                break;
			case CardBuffType.PhysicalReduction:
				target.PhysicalReduction = MathCalculation.PropDecayIncrease (target.PhysicalReduction, layer);
                break;
            case CardBuffType.MagicalReduction:
				target.MagicalReduction = MathCalculation.PropDecayIncrease (target.MagicalReduction, layer);
                break;
			case CardBuffType.HealPerRound:
				target.HealPerRound += layer;
				break;
			case CardBuffType.Poison:
				target.Poison += layer;
				break;
			case CardBuffType.Sunder:
				target.Sunder += layer;
				break;
			case CardBuffType.Weak:
				target.Weak += layer;
				break;
//			case CardBuffType.Acceleration:
//				target.Acceleration += layer;
//				target.Buffs.Add (new CardBuff (CardBuffType.Acceleration, layer, duration));
//				break;
//			case CardBuffType.Deceleration:
//				target.Deceleration += layer;
//				target.Buffs.Add (new CardBuff (CardBuffType.Deceleration, layer, duration));
//				break;
			case CardBuffType.Rebound:
				target.Rebound += layer;
				break;
			case CardBuffType.Dodge:
				target.Dodge = MathCalculation.PropDecayIncrease (target.Dodge, layer);
				break;
            case CardBuffType.DamageToMana:
                isBuffExist = target.DamageToMana;
                if (!isBuffExist)
                    target.DamageToMana = true;
				break;
            case CardBuffType.DamageAddMana:
                isBuffExist = target.BeAtkAddMana;
                if (!isBuffExist)
                    target.BeAtkAddMana = true;
                break;
            case CardBuffType.DamageAddAtk:
                isBuffExist = target.DamageAddAtk;
                if (!isBuffExist)
                    target.DamageAddAtk = true;
                break;
            case CardBuffType.CostNoMana:
                isBuffExist = target.CostNoMana;
                if (!isBuffExist)
                    target.CostNoMana = true;
                break;
            case CardBuffType.DoubleAtkOnce:
                target.DoubleAtkOnce++;
                break;
            case CardBuffType.DoubleAtk:
                target.DoubleAtk++;
                break;
            case CardBuffType.AddAtk:
                target.AddAtk += layer;
                break;
            case CardBuffType.Expensive:
                isBuffExist = target.Expensive;
                if (!isBuffExist)
                    target.Expensive = true;
                break;
            default:
                Debug.Log("Can not find CardBuffType : " + buff);
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
			case CardBuffType.Rebound:
				target.Rebound -= buff.Layers;
				break;
			case CardBuffType.Dodge:
				target.Dodge = MathCalculation.PropDecayDecrease (target.Dodge, buff.Layers);
				break;
			case CardBuffType.DamageToMana:
                target.DamageToMana = false;
				break;
            case CardBuffType.DamageAddMana:
                target.BeAtkAddMana = false;
                break;
            case CardBuffType.DamageAddAtk:
                target.DamageAddAtk = false;
                break;
            case CardBuffType.CostNoMana:
                target.CostNoMana = false;
                break;
            case CardBuffType.DoubleAtkOnce:
                target.DoubleAtkOnce--;
                break;
            case CardBuffType.DoubleAtk:
                target.DoubleAtk--;
                break;
            case CardBuffType.AddAtk:
                target.AddAtk -= buff.Layers;
                break;
            case CardBuffType.Expensive:
                target.Expensive = false;
                break;
			default:
				break;
		}
	}
        

    string[] SplitParam(string param){
        if (param.Contains("|"))
            return param.Split('|');
        else
            return new string[]{param};
    }

}
