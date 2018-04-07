using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadConfigs : MonoBehaviour {

    public static Dictionary<int,CardData> CardDictionary;
    public static Dictionary<int,Monster> MonsterDictionary;

	void Awake () {
        CardDictionary = new Dictionary<int, CardData>();
        LoadCards();

        MonsterDictionary = new Dictionary<int, Monster>();
        LoadMonsters();
	}
	
    void LoadCards(){
        Debug.Log("Loading Cards...");
        string[][] strs = ReadTxt.ReadText("cards");
        for (int i = 0; i < strs.Length-1; i++)
        {
            CardData c = new CardData();
            c.Id = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 0));
            c.Name = ReadTxt.GetDataByRowAndCol (strs, i + 1, 1);
            c.Description = ReadTxt.GetDataByRowAndCol (strs, i + 1, 2);
//            c.ActionCost = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 3));
            c.Price = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 4));
//            c.ManaCost = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 5));
            c.Level = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 6));
            c.MaxLevel = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 7));
            c.Tier = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 8));
            c.DecayTo = 0;

            CardDictionary.Add(c.Id, c);
        }
    }

    void LoadMonsters(){
        Debug.Log("Loading Monsters...");
        string[][] strs = ReadTxt.ReadText("monsters");
        for (int i = 0; i < strs.Length-1; i++)
        {
            Monster m = new Monster();
            m.Id = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 0));
            m.Name = ReadTxt.GetDataByRowAndCol (strs, i + 1, 1);
            m.Description = ReadTxt.GetDataByRowAndCol (strs, i + 1, 2);
            m.Gold = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 3));
            m.Exp = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 4));
            m.Action = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 5));
            m.Mana = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 6));
            m.Hp = float.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 7)); 
            m.Level = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 8));
            m.IsBoss = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 9));
            m.Deck = int.Parse (ReadTxt.GetDataByRowAndCol (strs, i + 1, 10));

            MonsterDictionary.Add(m.Id, m);
        }
    }
}
