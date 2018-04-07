using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleHandler : MonoBehaviour {

    //角色属性显示
    public Text HpText_Hero;
    public Text MaxhpText_Hero;
    public Slider HpSlider_Hero;
    public Text ManaText_Hero;
    public Text MaxManaText_Hero;
    public Slider ManaSlider_Hero;
    public Text ActionText_Hero;
    public Image HeadImage_Hero;

    //怪物属性显示
    public Text HpText_Enemy;
    public Text MaxhpText_Enemy;
    public Slider HpSlider_Enemy;
    public Text ManaText_Enemy;
    public Text MaxManaText_Enemy;
    public Slider ManaSlider_Enemy;
    public Text ActionText_Enemy;
    public Image HeadImage_Enemy;


    private CardsHandler _cardsHandler;
    private Enemy thisEnemy;

	void Start () {
        _cardsHandler = this.gameObject.GetComponent<CardsHandler>();

        InitBattleField();
	}
	
    void InitBattleField(){
        Monster m = GetMonster();
        Debug.Log("Monster Name: " + m.Name);

        SetEnemyProperty(m);
        UpdateShow_HeadImage();
        UpdateShow_States(true);
        UpdateShow_States(false);
        BattleState.ThisBattleTurn = 0;
        _cardsHandler.InitCardsPool(thisEnemy);

        //如果是我方先手
        if (true)
        {
            StartTurn();
        }
    }

    /// <summary>
    /// 获取怪物，测试用
    /// </summary>
    /// <returns>The monster.</returns>
    Monster GetMonster(){
        int total = LoadConfigs.MonsterDictionary.Count;
        int index = Random.Range(1, total);
        Debug.Log("Monster Index = " + index);
        return LoadConfigs.MonsterDictionary[index];
    }

    void SetEnemyProperty(Monster m){
        thisEnemy = new Enemy();
        thisEnemy.Level = 5;
        thisEnemy.MonsterId = m.Id;
        thisEnemy.Name = m.Name; //+前缀
        thisEnemy.Exp = m.Exp * thisEnemy.Level;
        thisEnemy.MaxAction = m.Action * thisEnemy.Level + 3;
        thisEnemy.Action = thisEnemy.MaxAction;
        thisEnemy.MaxMana = m.Mana * thisEnemy.Level + 5;
        thisEnemy.Mana = thisEnemy.MaxMana;
        thisEnemy.MaxHp = (int)(m.Hp * thisEnemy.Level) + 10;
        thisEnemy.Hp = thisEnemy.MaxHp;
        thisEnemy.HeadImage = Resources.Load(m.Image, typeof(Sprite)) as Sprite;
        thisEnemy.Deck = new List<CardData>();
        for (int i = 0; i < thisEnemy.Level * 5; i++)
        {
            int r = Random.Range(1, LoadConfigs.CardDictionary.Count);
            CardData c = LoadConfigs.CardDictionary[r];
            thisEnemy.Deck.Add(c);
        }
    }

   
    /// <summary>
    /// 开始回合
    /// </summary>
    public void StartTurn(){
        BattleState.ThisBattleTurn++;
        Debug.Log("第" + BattleState.ThisBattleTurn + "回合");
        //1.重置action属性
        GameData.ResetAction();
        UpdateAction(true);

        //2.根据当前发牌数获得手牌
        _cardsHandler.AddCard(GameData.thisHero.HandSize);

        //3.回合开始增加状态、触发特殊行动
    }

    /// <summary>
    /// 结束回合
    /// </summary>
    public void EndTurn(){
        Debug.Log("第" + BattleState.ThisBattleTurn + "回合结束");

        //1.将所有已发牌投入到等待位置
        _cardsHandler.SetHandToWaitingList();

        //2.检查手中的诅咒牌并弃牌

        //3.停止所有卡牌的可点击状态（将DragandDropItem设为false）

    }
       
    void UpdateShow_HeadImage(){
        HeadImage_Hero.sprite = Resources.Load("hero1", typeof(Sprite)) as Sprite;
        HeadImage_Enemy.sprite = thisEnemy.HeadImage;

    }

    public void UpdateShow_States(bool isHero){
        UpdateHp(isHero);
        UpdateMana(isHero);
        UpdateAction(isHero);
    }

    void UpdateHp(bool isHero){
        if (isHero)
        {
            HpText_Hero.text = GameData.thisHero.Hp.ToString();
            MaxhpText_Hero.text = "/" + GameData.thisHero.HpMax;
            HpSlider_Hero.value = GameData.thisHero.Hp / GameData.thisHero.HpMax;
        }
        else
        {
            HpText_Enemy.text = thisEnemy.Hp.ToString();
            MaxhpText_Enemy.text = "/" + thisEnemy.MaxHp;
            HpSlider_Enemy.value = thisEnemy.Hp / thisEnemy.MaxHp;
        }
    }

    void UpdateMana(bool isHero){
        if (isHero)
        {
            ManaText_Hero.text = GameData.thisHero.Mp.ToString();
            MaxManaText_Hero.text = (GameData.thisHero.Mp > GameData.thisHero.MpMax) ? ("/-") : ("/" + GameData.thisHero.MpMax);
            ManaSlider_Hero.value = Mathf.Min(GameData.thisHero.Mp / GameData.thisHero.MpMax, 1);
        }
        else
        {
            ManaText_Enemy.text = thisEnemy.Mana.ToString();
            MaxManaText_Enemy.text = (thisEnemy.Mana > thisEnemy.MaxMana) ? ("/-") : ("/" + thisEnemy.MaxMana);
            ManaSlider_Enemy.value = Mathf.Min(thisEnemy.Mana / thisEnemy.MaxMana, 1);
        }
    }

    void UpdateAction(bool isHero){
        if (isHero)
        {
            ActionText_Hero.text = GameData.thisHero.Action.ToString();
        }
        else
        {
            ActionText_Enemy.text = thisEnemy.Action.ToString();
        }
    }
}
