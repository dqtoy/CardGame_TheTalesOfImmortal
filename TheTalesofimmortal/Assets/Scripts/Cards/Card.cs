using UnityEngine;
using UnityEngine.UI;

public class Card : DragDropItem {
	public CardData data;
    public Player owner;
	public void Init(int cardId){
		data = LoadConfigs.ReadCardData (cardId);
        GetComponent<Image>().sprite = Resources.Load("CardImage/" + data.Name, typeof(Sprite)) as Sprite;
	}

    public override void Action(string param)
    {
        BattleManager manager = GetComponentInParent<BattleManager>();
        if (!manager.CanPlay(owner, data))
        {
            Debug.Log("Can not play card : " + data.Name);
            return;
        }
        //出牌
        switch(param){
            case "Enemy":
                break;
            case "Player":
                break;
            case "Puppet":
                break;
            default:
                break;
        }

    }
}
