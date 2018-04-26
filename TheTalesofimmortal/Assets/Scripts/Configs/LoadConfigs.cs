using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadConfigs  {

	
    public static CardData ReadCardData(int id){
        string[][] strs = ReadTxt.ReadText("Configs/cards");
        CardData c = ExcuteReadCardData(id, strs);
        return c;
    }

    static CardData ExcuteReadCardData(int id,string[][] strs){
        CardData c = new CardData();
        for (int i = 0; i < strs.Length-1; i++)
        {

            c.Id = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 0));
            if (c.Id != id)
                continue;
            c.Name = ReadTxt.GetDataByRowAndCol(strs, i + 1, 1);
            c.Description = ReadTxt.GetDataByRowAndCol (strs, i + 1, 2);
            c.Price = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 3));
            c.Level = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 4));
            c.MaxLevel = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 5));
            c.MpCost = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 5));
            c.Type = (CardType)int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 7));
            c.Condition = (CardPlayCondition)int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 8));
            int[] effectId = ReadString.GetInts(ReadTxt.GetDataByRowAndCol(strs, i + 1, 9));
            c.Effects = ReadCardEffect(effectId);
            return c;
        }
        Debug.Log("Cannot find CardData where id = " + id);
        return c;
    }

//    public CardEffect ReadCardEffect(int id){
//        string[][] strs = ReadTxt.ReadText("cardeffects");
//        CardEffect ce = ExcuteReadCardEffect(id, strs);
//        return ce;
//    }

    public static CardEffect[] ReadCardEffect(int[] id){
        string[][] strs = ReadTxt.ReadText("Configs/cardeffect");
        CardEffect[] ces = new CardEffect[id.Length];
        for (int i = 0; i < ces.Length; i++)
        {
            ces[i] = ExcuteReadCardEffect(id[i], strs);
        }
        return ces;
    }

    static CardEffect ExcuteReadCardEffect(int id,string[][] strs){
        CardEffect ce = new CardEffect();
        for (int i = 0; i < strs.Length; i++)
        {
            ce.Id = int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 0));
            if (ce.Id != id)
                continue;
            ce.Type = (CardEffectType)int.Parse(ReadTxt.GetDataByRowAndCol(strs, i + 1, 1));
            ce.Param = ReadTxt.GetDataByRowAndCol(strs, i + 1, 2);
            return ce;
        }
        Debug.Log("Cannot find CardEffect where id = " + id);
        return ce;
    }

//    void LoadMonsters(){
//        Debug.Log("Loading Monsters...");
//        string[][] strs = ReadTxt.ReadText("monsters");
//        for (int i = 0; i < strs.Length-1; i++)
//        {
//            Monster m = new Monster();
//            m.Id = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 0));
//            m.Name = ReadTxt.GetDataByRowAndCol (strs, i + 1, 1);
//            m.Description = ReadTxt.GetDataByRowAndCol (strs, i + 1, 2);
//            m.Gold = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 3));
//            m.Exp = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 4));
//            m.Action = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 5));
//            m.Mana = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 6));
//            m.Hp = float.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 7)); 
//            m.Level = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 8));
//            m.IsBoss = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 9));
//            m.Deck = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 10));
//
//            MonsterDictionary.Add(m.Id, m);
//        }
//    }
}
