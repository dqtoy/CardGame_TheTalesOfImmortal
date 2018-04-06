using System.Collections;
using System.Collections.Generic;

public class Player{
	public int Hp;
	public int HpMax;
	public int InitMana;
	private int PhysicalReduction = 0;
	private int MagicalReduction = 0;

	public Dictionary<CardBuffType,int> PlayerBuff;//id,层数
	public List<Card> CardPool;
	public List<Card> Hands;
	public List<Card> UsedCards;
	public List<Card> CardsInPreparation;//待发动区的卡

	public Player(int hp,int hpMax,int initMana,List<Card> cardPool){
		Hp = hp;
		HpMax = hpMax;
		InitMana = initMana;
		PlayerBuff = new Dictionary<CardBuffType,int> ();
		CardPool = cardPool;
		Hands = new List<Card> ();
		UsedCards = new List<Card> ();
		CardsInPreparation = new List<Card> ();
	}

	public void Attack(int orgDamage, AttackType atkType){
		int realDamage = orgDamage;
		//1. Reduction
		realDamage = DamageAfterReduction (realDamage, atkType);
		//2. Armor
		if (PlayerBuff.ContainsKey (CardBuffType.Armor)) {
			realDamage -= PlayerBuff [CardBuffType.Armor];
		}
		//3. Shield
		realDamage=DamageAfterShield(realDamage);

		ExecuteDamage (realDamage,atkType);
	}

	public void ExecuteDamage(int dam,AttackType atkType){
		Hp -= dam;
		//根据atkType造成反制效果
	}

	public int DamageAfterReduction(int dam,AttackType type){
		int rdc = (type == AttackType.Physical ? PhysicalReduction : MagicalReduction);
		return dam - PhysicalReduction * dam / 100;
	}

	public int DamageAfterShield(int dam){
		if (PlayerBuff.ContainsKey (CardBuffType.Shield)) {
			if (dam <= 0) {
				dam = 0;
			} else if (dam <= PlayerBuff [CardBuffType.Shield]) {
				dam = 0;
				PlayerBuff [CardBuffType.Shield] -= dam;
			} else {
				dam -= PlayerBuff [CardBuffType.Shield];
				RemoveBuff (CardBuffType.Shield);
			}
		}
		return dam;
	}

	public void RemoveBuff(CardBuffType buff){
		if (PlayerBuff.ContainsKey (buff))
			PlayerBuff.Remove (buff);
	}

}


