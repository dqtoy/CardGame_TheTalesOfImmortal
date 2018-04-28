using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class BattleManager : MonoBehaviour {


    private BattleView battleView;
	private Player _player;
	private Player _enemy;
	private BattleView _view;
	private PlayerView _playerView;
	private PlayerView _enemyView;
    private Target target;

	void Start () {
        battleView = GetComponent<BattleView>();
        InitBattleField();
	}


	void InitBattleField(){
        List<Card> lib = new List<Card>();
        Transform canvas = GetComponentInParent<Canvas>().transform;
        //从地牢获取角色数据
        //从configs读取Monster数据
        for (int i = 0; i < 10; i++)
        {
            GameObject g = Instantiate(Resources.Load("Prefab/card")) as GameObject;
            Card c = g.GetComponent<Card>();
            c.Init(100);
            lib.Add(c);
            g.transform.SetParent(canvas);
            g.SetActive(false);
        }

        //初始化角色
        _player = new Player(PlayerInfo.Player, 100, 100, 5, lib, battleView.playerView,battleView);
        _enemy = new Player(PlayerInfo.Enemy, 100, 100, 5, lib, battleView.enemyView, battleView);

        //初始化战场
        battleView.Init(_player,_enemy);
        _player.Init();
        _enemy.Init();

        //开始第一回合
    }

    void BattleStart(){
        //判断是否是玩家先手
        if (true)
        {
            _player.StartRound();
        }
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

    //这里的defender要改为target
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
		
    public void DrawCardTest(int count){
        _player.DrawCards(count);
    }

}
