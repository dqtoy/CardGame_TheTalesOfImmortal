using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:Target{
    
    public int RoundCard = 3;
	public bool TurnOn = false;

    public List<Card> Library;
    public List<Card> Hands;
    public List<Card> Graveyard;
    public List<Puppet> Puppets;
    private BattleView BattleField;


    public Player(PlayerInfo info, int hp,int hpMax,int initMana,List<Card> library,PlayerView view,BattleView battleview){
        Info = info;
        HP = hp;
        HpMax = hpMax;
        MP = initMana;
        Buffs = new List<CardBuff>();
        Library = library;
        Hands = new List<Card>();
        Graveyard = new List<Card>();
        Puppets = new List<Puppet>();
        View = view as TargetView;
        BattleField = battleview;
    }

    public void Init(){
        DrawCards(RoundCard);
    }

	public void EndRound(){
		//清除手牌中的无效卡

		//抽卡
		if (Hands.Count < RoundCard) {
			DrawCards (RoundCard - Hands.Count);
		}
	}


	public void StartRound(){
		//结算buff效果
        if (HealPerRound > 0)
            Heal(HealPerRound);
		if (Poison > 0) {
            Damage(Poison);
            Poison--;
		}

		//结算Duration
		for(int i=0;i<Buffs.Count;i++){
			Buffs [i].Duration--;
			if (Buffs [i].Duration<=0) {

				//清除效果
				Buffs.RemoveAt (i);
                View.UpdateBuffs(this as Target);
			}
		}

		//打开发牌状态
		MyTurn();
	}

	void MyTurn(){
		TurnOn=true;
		if (Info == PlayerInfo.Enemy)
			AIPlay ();
	}

	void AIPlay(){
		Debug.Log ("AIPlaying...");
	}

	public void DrawCards(int count){
		for (int i = 0; i < count; i++) {
			if (Library.Count <= 0) {
				if (Graveyard.Count > 0)
					Shuffle ();
				else {
					return;
				}
			}
			int index = MathCalculation.GetRandomValue (Library.Count);
			AddCardToHand (Library [index],1);
			Library.RemoveAt (index);
		}
	}

    public void AddCardToHand(Card card,int count){
        for (int i = 0; i < count; i++)
        {
            Hands.Add(card);
            BattleField.DrawCard(Info, card.gameObject);
        }
    }

    public void AddCardToLibrary(Card card,int count){
        for (int i = 0; i < count; i++)
        {
            Hands.Add(card);
        }
    }

    public void RemoveRandomCard(int count){
        for (int i = 0; i < count; i++)
        {
            if (Hands.Count <= 0)
                return;
            int index = MathCalculation.GetRandomValue(Hands.Count);
            RemoveHand(index);
        }
	}

	void RemoveHand(int index){
		if (Hands.Count <= index)
			return;
		Hands.RemoveAt (index);
	}

	void Shuffle(){
		for (int i = 0; i < Graveyard.Count; i++) {
			Library.Add (Graveyard [i]);
		}
		Graveyard.Clear ();
	}
}


