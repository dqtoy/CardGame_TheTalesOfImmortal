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


    public Player(PlayerInfo info, int hp,int hpMax,int initMana,int initActPoint,List<Card> library,PlayerView view,BattleView battleview){
        Info = info;
        HP = hp;
        HpMax = hpMax;
        MP = initMana;
        ActPoint = initActPoint;
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
		//清除手牌
        if(Info == PlayerInfo.Player){
            //清除无效牌（眩晕等）
        }else{
            BattleField.EnemyDiscardAll(this);
        }

		//抽卡
        Debug.Log("endround...handcount = "+Hands.Count);
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

		//结算只有1回合的buff
        ClearRoundBuff();

		//打开发牌状态
        TurnOn = true;
	}

	public void DrawCards(int count){
        Debug.Log(Info + " 抽卡 " + count + " 张，牌库数量：" + Library.Count+"，坟场数量："+Graveyard.Count);
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

    public bool CanPlay(CardData card){
        if (card.ActionCost > this.ActPoint)
            return false;
        if (!CostNoMana && Expensive && card.MpCost * 2 > this.MP)
            return false;
        return true;
    }

    public void PlayCard(Card card){
        if (card.data.ActionCost > 0)
            CostActPoint(card.data.ActionCost);
        if (!CostNoMana)
        {
            if (Expensive)
                DamageMp(card.data.MpCost * 2);
            else
                DamageMp(card.data.MpCost);
        }

        Hands.Remove(card);

        //这里只是逻辑上放到了坟场，但展示上需要另行处理或者不处理
        Graveyard.Add(card);
    }

	void RemoveHand(int index){
		if (Hands.Count <= index)
			return;
		Hands.RemoveAt (index);
	}

	void Shuffle(){
        Debug.Log(Info + " Shuffling");
		for (int i = 0; i < Graveyard.Count; i++) {
			Library.Add (Graveyard [i]);
		}
		Graveyard.Clear ();
	}
        
}


