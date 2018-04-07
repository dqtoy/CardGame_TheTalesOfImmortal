using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour {
    public static Hero thisHero = new Hero();

    void Awake(){
        LoadHero();
    }

    void LoadHero(){
        Debug.Log("Hero Awake");
        thisHero.Profession = 0;
        thisHero.Lv = 0;
        thisHero.Gold = 0;
        thisHero.HandSize = 3;
        thisHero.Hp = 100;
        thisHero.HpMax = 100;
        thisHero.Mp = 5;
        thisHero.MpMax = 10;
        thisHero.Action = 3;
        thisHero.ActionMax = 3;

        thisHero.Skills = new List<CardData>();
        thisHero.DugeonAbilities = new List<DungeonAbility>();

        thisHero.Cards = new List<CardData>();
        thisHero.DungeonState = new int[0];
        thisHero.EquipPositions = new int[0];

        GetTestCards();
    }
	
    /// <summary>
    /// 在测试时获取测试卡牌
    /// </summary>
    void GetTestCards(){
        for (int i = 0; i < 20; i++)
        {
            int r = Random.Range(1, LoadConfigs.CardDictionary.Count);
            thisHero.Cards.Add(LoadConfigs.CardDictionary[r]);
        }
    }

    public static void CastCost(CardData c){
//        GameData.thisHero.Mp -= c.ManaCost;
        //存储数据
//        GameData.thisHero.Action -= c.ActionCost;
        //存储数据
    }

//    public static void CastCost(CardData s){
//        
//    }

    public static void CastCost(DungeonAbility da){
    
    }

    public static bool CanCast(CardData c){
//        if (c.ManaCost > GameData.thisHero.Mp)
//        {
//            Debug.Log("Mp Insufficient");
//            return false;
//        }
//        if (c.ActionCost > GameData.thisHero.Action)
//        {
//            Debug.Log("Action Insufficient");
//            return false;
//        }
        return true;
    }

    /// <summary>
    /// 战斗过程中重置action点数
    /// </summary>
    /// <returns><c>true</c>, if action was reset, <c>false</c> otherwise.</returns>
    public static void ResetAction(){
        thisHero.Action = thisHero.ActionMax;
    }
}
