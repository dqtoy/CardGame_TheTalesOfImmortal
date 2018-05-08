using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : MonoBehaviour {

    public PlayerView playerView;
    public PlayerView enemyView;

    public CardsContainer PlayerHandContainer;
    public CardsContainer PlayedContainer;
    public PuppetContainer PlayerPuppets;
    public PuppetContainer EnemyPuppets;
    public Text EnemyHand;
	public Button EndRound;

    //下面这一堆，好像用不着
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
            PlayerHandContainer.AddCard(CardsInPlayerHand, card, new Vector3(-733, -365, 0));
        }else if(info == PlayerInfo.Enemy){
            CardsInEnemyLibrary.Remove(card);
            CardsInEnemyHand.Add(card);
            UpdateEnemyHandShow();
        }    
    }

    public void PlayCard(PlayerInfo info,GameObject card){
//        CardsPlayed.Add(card);
        Vector3 startPos = new Vector3(733, 400, 0);
        if (info == PlayerInfo.Player)
        {
            CardsInPlayerHand.Remove(card);
            startPos = new Vector3(card.transform.localPosition.x + 105f,
                card.transform.localPosition.y - 375.4f, 
                card.transform.localPosition.x);
        }
        else if (info == PlayerInfo.Enemy)
        {
            CardsInEnemyHand.Remove(card);
            UpdateEnemyHandShow();
            card.SetActive(true);
        }
        PlayedContainer.AddCard(CardsPlayed, card, Vector3.zero);
    }

    public void DiscardCard(PlayerInfo info,GameObject card){
        if (info == PlayerInfo.Player)
        {
            CardsInPlayerHand.Remove(card);
            PlayerHandContainer.MoveAway(CardsInPlayerTomb, card, new Vector3(-733, -175, 0));
        }
        else if (info == PlayerInfo.Enemy)
        {
            CardsInEnemyTomb.Add(card);
            CardsInEnemyHand.Remove(card);
            UpdateEnemyHandShow();
        }
    }

    public void ClearPlayedArea(PlayerInfo info){

        while (CardsPlayed.Count > 0)
        {
            if (info == PlayerInfo.Player)
            {
                PlayedContainer.MoveAway(CardsInPlayerTomb, CardsPlayed[0], new Vector3(-733, -175, 0));    
            }
            else if (info == PlayerInfo.Enemy)
            {
                PlayedContainer.MoveAway(CardsInEnemyTomb, CardsPlayed[0], new Vector3(-733, 365, 0));
            }
            CardsPlayed.RemoveAt(0);
        }
    }

    public void PlayerDiscardAll(Player player){
        while (player.Hands.Count > 0)
        {
            PlayerHandContainer.MoveAway(CardsInPlayerTomb, player.Hands[0].gameObject, new Vector3(-733, -175, 0));
            player.Graveyard.Add(player.Hands[0]);

            player.Hands.RemoveAt(0);
            CardsInPlayerHand.RemoveAt(0);
        }
    }

    //To Do 展示效果
    public void EnemyDiscardAll(Player enemy){
        while (enemy.Hands.Count > 0)
        {
            enemy.Graveyard.Add(enemy.Hands[0]);
            CardsInEnemyTomb.Add(enemy.Hands[0].gameObject);
            CardsInEnemyHand.RemoveAt(0);
            enemy.Hands.RemoveAt(0);
        }
    }

    public void Shuffle(PlayerInfo info){
        if (info == PlayerInfo.Player)
        {
            Shuffle(CardsInPlayerTomb, CardsInPlayerLibrary);
        }
        else if (info == PlayerInfo.Enemy)
        {
            Shuffle(CardsInEnemyTomb, CardsInEnemyLibrary);
        }
    }

    void Shuffle(List<GameObject> org,List<GameObject> target){
        for (int i = 0; i < org.Count; i++)
            target.Add(org[i]);
        org.Clear();
    }

    void UpdateEnemyHandShow(){
        EnemyHand.text = CardsInEnemyHand.Count.ToString();
    }


	public void ExitBattle(){

	}

	public void UpdateEndRound(bool isOn){
		EndRound.interactable = isOn;
	}

	void DestroyList(List<GameObject> l){
		while (l.Count > 0) {
			GameObject g = l [0] as GameObject;
            l.RemoveAt (0);
			DestroyImmediate (g);
		}
	}



}
