using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

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

	public void PlayCard(Player attacker,Player defender,CardData card){
		foreach (CardEffect effect in card.Effects) {
			ExcuteCardEffect (attacker, defender, effect);
		}
	}

	int ExcuteCardEffect(Player attacker,Player defender,CardEffect effect){
		if (effect.Type == CardEffectType.None)
			return 0;
		CardEffectHandler handler = new CardEffectHandler ();
		string methodName = Enum.GetName (typeof(CardEffectType), effect.Type);
		MethodInfo method = handler.GetType ().GetMethod (methodName);
		if (method == null) {
			Debug.Log ("Can not find method by " + methodName);
			return 0;
		}
		object[] parameters = new object[]{ attacker, defender, effect };
		int result = (int)method.Invoke (handler, parameters);

		return result;
	}

	void StartRound(Player player){
		if (player.Hands.Count < player.Card) {
			player.DrawCards (player.Card - player.Hands.Count);
		}
			
	}
		

}
