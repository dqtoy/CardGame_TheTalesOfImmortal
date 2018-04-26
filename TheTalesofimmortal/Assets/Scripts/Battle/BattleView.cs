using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : MonoBehaviour {

    public PlayerView playerView;
    public PlayerView enemyView;
    public CardsContainer PlayerHand;
    public CardsContainer Played;
    public PuppetContainer PlayerPuppets;
    public PuppetContainer EnemyPuppets;
    public Text EnemyHand;

    private List<GameObject> CardsInPlayerHand = new List<GameObject>();
    private List<GameObject> CardsInPlayerLibrary = new List<GameObject>();
    private List<GameObject> CardsInPlayerTomb = new List<GameObject>();

    private List<GameObject> CardsInEnemyHand = new List<GameObject>();
    private List<GameObject> CardsInEnemyLibrary = new List<GameObject>();
    private List<GameObject> CardsInEnemyTomb = new List<GameObject>();
    private List<GameObject> CardsPlayed = new List<GameObject>();


    public void Init(Player player,Player enemy){
        playerView.Init(player);
        enemyView.Init(enemy);
	}


    public void DrawCard(PlayerInfo info,GameObject card){
        if (info == PlayerInfo.Player)
        {
            CardsInPlayerLibrary.Remove(card);
            card.SetActive(true);
            PlayerHand.AddCard(CardsInPlayerHand, card, Vector3.zero);
        }else if(info == PlayerInfo.Enemy){
            CardsInEnemyLibrary.Remove(card);
            CardsInEnemyHand.Add(card);
            EnemyHand.text = CardsInEnemyHand.Count.ToString();
        }    
    }

    public void DiscardCard(PlayerInfo info,GameObject card){
        
    }




	public void ExitBattle(){

	}

	void DestroyList(List<GameObject> l){
		while (l.Count > 0) {
			GameObject g = l [0] as GameObject;
			DestroyImmediate (g);
			l.RemoveAt (0);
		}
	}



}
