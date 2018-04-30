using UnityEngine;
using UnityEngine.UI;

public class Card : DragDropItem {
	public CardData data;
    public Player owner;
	public Image profile;
	public void Init(int cardId,Player player){
		data = LoadConfigs.ReadCardData (cardId);
		owner = player;
        profile.sprite = Resources.Load("CardImage/" + data.Name, typeof(Sprite)) as Sprite;
	}

    public override void Action(string param)
    {
		Debug.Log ("Card Action!");
        BattleManager manager = GetComponentInParent<BattleManager>();
        if (!manager.CanPlay(owner, data))
        {
            Debug.Log("Can not play card : " + data.Name);
            return;
        }
        //出牌
		Target target = manager.GetTarget(param);
		if (target == null) {
			Debug.Log ("Can not find Target with param " + param);
			return;
		}
		manager.PlayCard (owner, target, this);
    }
}
