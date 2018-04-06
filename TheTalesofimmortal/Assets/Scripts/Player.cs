using System.Collections;
using System.Collections.Generic;

public class Player{
	public int Hp;
	public int HpMax;
	public int InitMana;
	public List<CardBuff> BuffList;
	public List<Card> CardPool;
	public List<Card> Hands;
	public List<Card> UsedCards;
	public List<Card> CardsInPreparation;//待发动区的卡

	public Player(int hp,int hpMax,int initMana,List<Card> cardPool){
		Hp = hp;
		HpMax = hpMax;
		InitMana = initMana;
		BuffList = new List<CardBuff> ();
		CardPool = cardPool;
		Hands = new List<Card> ();
		UsedCards = new List<Card> ();
		CardsInPreparation = new List<Card> ();
	}
}
