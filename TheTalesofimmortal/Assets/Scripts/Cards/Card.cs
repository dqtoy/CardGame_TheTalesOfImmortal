using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
	private CardData data;

	public void Init(CardData data){
		this.data = data;
	}

    public Card(int cardId){
        
    }
}
