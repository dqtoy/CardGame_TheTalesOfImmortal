using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : MonoBehaviour {

	List<GameObject> CardsInHand;
	List<GameObject> CardsPlayed;
	List<GameObject> MyPuppets;
	List<GameObject> EnemyPuppets;

	void Start () {
		InitBattle ();
	}
	

	public void InitBattle(){
		CardsInHand = new List<GameObject> ();
		CardsPlayed = new List<GameObject> ();
		MyPuppets = new List<GameObject> ();
		EnemyPuppets = new List<GameObject> ();
	}

	public void ExitBattle(){


		DestroyList (CardsInHand);
		DestroyList (CardsPlayed);
		DestroyList (MyPuppets);
		DestroyList (EnemyPuppets);
	}

	void DestroyList(List<GameObject> l){
		while (l.Count > 0) {
			GameObject g = l [0] as GameObject;
			DestroyImmediate (g);
			l.RemoveAt (0);
		}
	}



}
