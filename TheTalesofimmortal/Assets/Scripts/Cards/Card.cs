using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
	public CardData data;
	public void Init(int cardId){
		data = LoadConfigs.ReadCardData (cardId);
//		this.gameObject.GetComponent<Image> ().sprite = Resources.Load (data.Name) as Sprite;
	}
}
