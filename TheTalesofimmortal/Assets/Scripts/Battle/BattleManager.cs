using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class BattleManager : MonoBehaviour {

    public PlayerView playerView;
    public PlayerView enemyView;
    public CardsContainer playerCardsContainer;
    public CardsContainer enemyCardsContainer;

	private Player _player;
	private Player _enemy;
	private BattleView _view;
	private PlayerView _playerView;
	private PlayerView _enemyView;
    private Target target;

	void Start () {
        InitBattleField();
	}


	void InitBattleField(){
        List<Card> lib = new List<Card>();

        //配置卡牌数据
//		GameObject g = Resources.Load ("card") as GameObject;
//		g.GetComponent<Card> ().Init (100);

        _player = new Player(100, 100, 5, lib, playerView, playerCardsContainer);
        _enemy = new Player(100, 100, 5, lib, enemyView, enemyCardsContainer);
    }



	public bool CanPlay(Player dealer, CardData card){
		return true;
	}

	public void PlayCard(Player attacker,Player defender,CardData card){
		foreach (CardEffect effect in card.Effects) {
			ExcuteCardEffect (attacker, defender, effect);
            //CheckIsPlayerDead
            //CheckIsTargetDead
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
		object[] parameters = new object[]{ attacker, effect,target };
		int result = (int)method.Invoke (handler, parameters);


		return result;
	}
		

}
