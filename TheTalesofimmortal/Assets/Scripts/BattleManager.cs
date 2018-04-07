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
		_player = new Player (100, 100, 5, new List<CardData> (),_playerView);
		_enemy = new Player (100, 100, 5, new List<CardData> (),_enemyView);
	}

	public bool CanPlay(Player dealer, CardData card){
		return true;
	}

	public void PlayCard(Player dealer,CardData card){
	
	}
}
