using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	private Player _player;
	private Player _enemy;
	private PlayerView _playerView;
	private PlayerView _enemyView;

	// Use this for initialization
	void Start () {
		
	}


	void InitBattleField(){
		_player = new Player (100, 100, 5, new List<CardData> ());
		_enemy = new Player (100, 100, 5, new List<CardData> ());
	}

	public bool CanPlay(Player dealer, CardData card){
		return true;
	}

	public void PlayCard(Player dealer,CardData card){
		foreach (CardEffect ce in card.Effects) {
			
		}
	}

	int ExcuteCardEffect(Player attacker,Player defender, CardEffect effect){
		switch (effect.Type) {
			case CardEffectType.None:
				return 0;
			case CardEffectType.Heal:
				return Heal (attacker, defender, effect.Param);
			case CardEffectType.PhysicalDamage:
				return PhysicalDamage(attacker,defender,effect.Param);
			case CardEffectType.MagicalDamage:
				return MagicalDamage(attacker,defender,effect.Param);
			default:
				return 0;
		}
	}
		
	int Heal(Player attacker,Player defender,string param){
		int value = int.Parse (param);
		attacker.Hp += value;
		return value;
	}

	int PhysicalDamage(Player attacker,Player defender,string param){
		int value = int.Parse (param);
		//物理减免
		if (defender.PhysicalReduction > 0)
			value -= value * defender.PhysicalReduction / 100;
		//减伤盾
		if(defender.PlayerBuff.ContainsKey(CardBuffType.Armor))
			value -= defender.PlayerBuff[CardBuffType.Armor];
		//生命盾
		if (value > 0 && defender.PlayerBuff.ContainsKey (CardBuffType.Shield))
			DamageAfterShield (defender, ref value);
		
		value = (value < 0) ? 0 : value;
			
		//造成伤害
		if (value > 0)
			ExcuteDamage (defender, value);
		
		return value;
	}

	int MagicalDamage(Player attacker,Player defender,string param){
		int value = int.Parse (param);
		defender.Hp -= value;
		//物理减免
		if (defender.MagicalReduction > 0)
			value -= value * defender.MagicalReduction / 100;
		//减伤盾
		if(defender.PlayerBuff.ContainsKey(CardBuffType.Armor))
			value -= defender.PlayerBuff[CardBuffType.Armor];
		//生命盾
		if (value > 0 && defender.PlayerBuff.ContainsKey (CardBuffType.Shield))
			DamageAfterShield (defender, ref value);

		value = (value < 0) ? 0 : value;

		//造成伤害
		if (value > 0)
			ExcuteDamage (defender, value);

		return value;
	}

	void DamageAfterShield(Player player, ref int damage){
		if (damage < player.PlayerBuff [CardBuffType.Shield]) {
			damage = 0;
			player.PlayerBuff [CardBuffType.Shield] -= damage;
		} else {
			damage -= player.PlayerBuff [CardBuffType.Shield];
			RemoveBuff (player, CardBuffType.Shield);
		}
	}

	void ExcuteDamage(Player player,int damage){
		player.Hp -= damage;
	}
		
	public void RemoveBuff(Player player, CardBuffType buff){
		if (player.PlayerBuff.ContainsKey (buff))
			player.PlayerBuff.Remove (buff);
	}
}
