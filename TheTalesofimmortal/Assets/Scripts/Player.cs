using System.Collections;
using System.Collections.Generic;

public class Player{
    public int Hp = 1;
    public int HpMax = 1;
    public int Mp = 1;
    public int Card = 3;

    public int Shield;
    public int Armor = 0;
    public int PhysicalReduction = 0;
    public int MagicalReduction = 0;
    public int HealPerRound;
    public int Poison = 0;
    public int Sunder = 0;
    public int Weak = 0;
    public int Acceleration = 0;
    public int Deceleration = 0;
    public int Rebound = 0;
    public int Dodge = 0;
    public bool DamageToMana = false;

	public Dictionary<CardBuffType,int> PlayerBuff;//id,层数&效果
    public List<CardBuff> PlayerBuffs;
	public List<CardData> CardPool;
	public List<CardData> Hands;
	public List<CardData> UsedCards;
    public List<Puppet> Puppets;

	public Player(int hp,int hpMax,int initMana,List<CardData> cardPool){
        Hp = hp;
        HpMax = hpMax;
        Mp = initMana;
        PlayerBuffs = new List<CardBuff>();
        CardPool = cardPool;
        Hands = new List<CardData>();
        UsedCards = new List<CardData>();
        Puppets = new List<Puppet>();
    }

}


