using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerView : TargetView {


	public Slider HpSlider;
    public Text HpText;
    public Text HpMaxText;
	public Text MpText;
    public Text ApText;//ActionPoint
	public GameObject buffContainer;
	public Image ProfileImage;

	private Button[] BuffBtns;
	private Dictionary<CardBuffType,int> bufflist;

	void Awake(){
		bufflist = new Dictionary<CardBuffType, int> ();
		BuffBtns = buffContainer.GetComponentsInChildren<Button>();
	}

    public void UpdatePlayerView(Player player){
        UpdateProfile("HeadImage/hero1");
        UpdateHp(player.HP, player.HpMax);
        UpdateMp(player.MP);
        UpdateAp(player.ActPoint);
        UpdateBuffShow();
    }

    public override void UpdateProfile(string path){
        ProfileImage.sprite = Resources.Load(path,typeof(Sprite)) as Sprite;
    }

    public override void UpdateHp(int hp,int maxHp){
        HpSlider.value = hp / maxHp;
        HpText.text = hp.ToString();
        HpMaxText.text = "/" + maxHp;
    }

    public override void UpdateMp(int mp){
        MpText.text = mp.ToString();
    }

    public void UpdateAp(int ap){
        ApText.text = ap.ToString();
    }
		
	public override void UpdateBuffShow(){
        Debug.Log("UpdateBuffShow-->");
		int index = 0;
		foreach (CardBuffType key in bufflist.Keys) {
			if (index >= BuffBtns.Length)
                break;

			//1层不显示
			string layer = "";
			if (bufflist [key] != 1) {
				layer = bufflist [key].ToString ();
			}
			UpdateBuffContent (index, key.ToString (), layer);
            index++;
		}
		for (int i = index; i < BuffBtns.Length; i++) {
            UpdateBuffContent (i, null, null);
		}

    }

	void UpdateBuffContent(int index, string name, string layer){
		if (name == null) {
			BuffBtns [index].gameObject.SetActive (false);
			return;
		}

		BuffBtns [index].gameObject.SetActive (true);
		Image i = BuffBtns [index].GetComponent<Image> ();
		Text t = BuffBtns[index].GetComponentInChildren<Text>();
		i.sprite = Resources.Load ("HeadImage/" + name, typeof(Sprite)) as Sprite;
		t.text = layer;

	}

	//包含添加和修改
	public override void AddBuff(CardBuffType c,int layer){
		if (bufflist.ContainsKey (c)) {
			bufflist [c] += layer;		
		} else {
			bufflist.Add (c, layer);
		}
	}

	public override void RemoveBuff(CardBuffType c){
		bufflist.Remove (c);
	}
}
