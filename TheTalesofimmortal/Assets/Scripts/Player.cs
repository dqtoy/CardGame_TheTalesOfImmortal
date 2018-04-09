using System.Collections;
using System.Collections.Generic;

public class Player{
	public int Hp;
	public int HpMax;
	public int InitMana;
	public int PhysicalReduction = 0;
	public int MagicalReduction = 0;
	private PlayerView _view;

	public Dictionary<CardBuffType,int> PlayerBuff;//id,层数
	public List<CardData> CardPool;
	public List<CardData> Hands;
	public List<CardData> UsedCards;

	public Player(int hp,int hpMax,int initMana,List<CardData> cardPool){
		Hp = hp;
		HpMax = hpMax;
		InitMana = initMana;
		PlayerBuff = new Dictionary<CardBuffType,int> ();
		CardPool = cardPool;
		Hands = new List<CardData> ();
		UsedCards = new List<CardData> ();
	}

}


