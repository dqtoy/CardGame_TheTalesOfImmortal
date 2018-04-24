//这个脚本用于自动调整手牌和已发牌区域的框大小
using UnityEngine;
using System.Collections.Generic;

public class CardsContainer : MonoBehaviour {
    
    private List<GameObject> Cards = new List<GameObject>();

    public void AddCard(Card card){
        Cards.Add(card.gameObject);    
    }

    public void MoveCard(int index){
        Cards.RemoveAt(index);
    }

    void UpdateShow(){
        int count = Cards.Count;
        float x = Mathf.Min(1120f, count * 150f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(x, 256.5f);
    }

}
