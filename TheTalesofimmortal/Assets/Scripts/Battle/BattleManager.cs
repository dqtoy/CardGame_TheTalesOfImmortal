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

	void Start () {
        battleView = GetComponent<BattleView>();
        InitBattleField();
	}

	void InitBattleField(){
		List<Card> PlayerLib = new List<Card>();
		List<Card> EnemyLib = new List<Card>();
        Transform canvas = GetComponentInParent<Canvas>().transform;
        //从地牢获取角色数据
        //从configs读取Monster数据
        for (int i = 0; i < 10; i++)
        {
            GameObject g = Instantiate(Resources.Load("Prefab/card")) as GameObject;
            Card c = g.GetComponent<Card>();
            PlayerLib.Add(c);
            g.transform.SetParent(canvas);
            g.SetActive(false);
        }
		for (int i = 0; i < 10; i++)
		{
			GameObject g = Instantiate(Resources.Load("Prefab/card")) as GameObject;
			Card c = g.GetComponent<Card>();
			EnemyLib.Add(c);
			g.transform.SetParent(canvas);
			g.SetActive(false);
		}

        //初始化角色
        _player = new Player(PlayerInfo.Player, 100, 100, 5,5, PlayerLib, battleView.playerView,battleView);
        _enemy = new Player(PlayerInfo.Enemy, 100, 100, 5,5, EnemyLib, battleView.enemyView, battleView);

        foreach (Card c in PlayerLib)
        {
            int r = UnityEngine.Random.Range(0, TempConfigs.CardList.Length - 1);
            c.Init (TempConfigs.CardList[r], _player);
        }
			
        foreach (Card c in EnemyLib)
        {
            int r = UnityEngine.Random.Range(0, TempConfigs.CardList.Length - 1);
            c.Init (TempConfigs.CardList[r], _enemy);
        }
			

        //初始化战场
        battleView.Init(_player,_enemy);
        _player.Init();
        _enemy.Init();

        //开始第一回合
		BattleStart();
    }

    void BattleStart(){
        //判断是否是玩家先手
		if (_enemy.Name!="先手") {
			_player.StartRound ();
			battleView.UpdateEndRound (true);
		} else {
			_enemy.StartRound ();
			battleView.UpdateEndRound (false);
            StartEnemyAI();
		}
    }

	//下面两段可以合并
	public void PlayerEndRound(){
		battleView.ClearPlayedArea (PlayerInfo.Player);
		_enemy.StartRound ();
		battleView.UpdateEndRound (false);
        StartEnemyAI();
	}

	public void EnemyEndRound(){
        battleView.EnemyDiscardAll(_enemy);
		battleView.ClearPlayedArea (PlayerInfo.Enemy);
		_player.StartRound ();
		battleView.UpdateEndRound (true);
	}


    void StartEnemyAI(){
        AIPlay ai = new AIPlay(_enemy, _player, this);
        ai.PlayInOrder();
    }

	public bool CanPlay(Player dealer, CardData data){
		return true;
	}
        
	public void PlayCard(Player attacker,Target defender,Card card){
		battleView.PlayCard (card.owner.Info, card.gameObject);

        attacker.PlayCard();

		foreach (CardEffect effect in card.data.Effects) {
			ExcuteCardEffect (attacker, defender, effect);
            //CheckIsPlayerDead
            //CheckIsTargetDead
		}
	}
        
	int ExcuteCardEffect(Player attacker,Target defender,CardEffect effect){
		if (effect.Type == CardEffectType.None)
			return 0;
		CardEffectHandler handler = new CardEffectHandler ();
		string methodName = Enum.GetName (typeof(CardEffectType), effect.Type);
		MethodInfo method = handler.GetType ().GetMethod (methodName, BindingFlags.NonPublic|BindingFlags.Instance);
		if (method == null) {
			Debug.Log ("Can not find method by " + methodName);
			return 0;
		}
		object[] parameters = new object[]{ attacker, effect.Param, defender };
		int result = (int)method.Invoke (handler, parameters);

		return result;
	}
        
    /// <summary>
    /// 弃牌
    /// </summary>
    /// <param name="player">Player.</param>
    /// <param name="c">C.</param>
    public void Discard(Player player,Card card){
        battleView.DiscardCard(player.Info, card.gameObject);
    }
		
    public void DrawCardTest(int count){
        _player.DrawCards(count);
    }

	public Target GetTarget(string param){
        switch (param) {
			case "PlayZone":
				return _enemy as Target;
			case "Player":
				return _player as Target;
			case "Enemy":
				return _enemy as Target;
			default:
				return GetPuppet (param);
		}
	}

	Target GetPuppet(string param){
		if (!param.Contains ("P")) {
            return _enemy;
		}
		byte[] b = System.Text.Encoding.Default.GetBytes (param);
		int index = (int)b [2];
		if (b [0] == 'E') {
			if (_enemy.Puppets.Count > index)
				return _enemy.Puppets [index] as Target;
		} else if (b [0] == 'P') {
			if (_player.Puppets.Count > index)
				return _player.Puppets [index] as Target;
		} 
		return _enemy;
	}


    public void AIPlay(Player attacker,Player defender){
        
    }


}
