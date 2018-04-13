using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player:Target{
    
    public int Card = 3;

    public List<Card> Library;
    public List<Card> Hands;
    public List<Card> Graveyard;
    public List<Puppet> Puppets;

    public Player(int hp,int hpMax,int initMana,List<Card> library){
        HP = hp;
        HpMax = hpMax;
        MP = initMana;
        Buffs = new List<CardBuff>();
        Library = library;
        Hands = new List<Card>();
        Graveyard = new List<Card>();
        Puppets = new List<Puppet>();
    }

	public void EndRound(){
		//抽卡
		if (Hands.Count < Card) {
			DrawCards (Card - Hands.Count);
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

				//清楚效果
				Buffs.RemoveAt (i);
			}
		}
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
            Hands.Add(card);
    }

    public void AddCardToLibrary(Card card,int count){
        for (int i = 0; i < count; i++)
            Hands.Add(card);
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


