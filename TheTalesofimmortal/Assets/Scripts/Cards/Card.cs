using UnityEngine;
using UnityEngine.UI;

public class Card : DragDropItem {
	public CardData data;
	public Image profile;
    public Text nameText;
    public Text descText;
    public Text actionCostText;
    public Text mpCostText;

	public void Init(int cardId,Player player){
		data = LoadConfigs.ReadCardData (cardId);
		owner = player;


        //Todo 修改卡牌图片 data.Name
        profile.sprite = Resources.Load("CardImage/" + "测试卡牌", typeof(Sprite)) as Sprite;
        nameText.text = data.Name;
        descText.text = data.Description;
        actionCostText.text = data.ActionCost.ToString();
        mpCostText.text = data.MpCost.ToString();
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
            
        if (param == "DropZone")
        {
            manager.Discard(owner, this);
            Debug.Log("Drop Card : " + data.Name);
        }
        else
        {
            Target target = manager.GetTarget(param);
            if (target == null)
            {
                Debug.Log("Can not find Target with param " + param);
                return;
            }
            manager.PlayCard(owner, target, this);
        }
    }
}
