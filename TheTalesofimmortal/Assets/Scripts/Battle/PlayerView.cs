using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerView : TargetView {


	private Slider HpSlider;
    private Text HpText;
    private Text HpMaxText;
	private Text MpText;
	private Button[] Buffs;
	private Image ProfileImage;

	void Start () {
		ProfileImage = GetComponentInChildren<Image> ();
        Text[] t = GetComponentsInChildren<Text>();
        MpText = t[0];
        HpText = t[1];
        HpMaxText = t[2];
		HpSlider = GetComponentInChildren<Slider> ();
		Buffs = GetComponentsInChildren<Button> ();
	}
	
    public override void UpdateProfile(string path){
        ProfileImage.sprite = Resources.Load(path) as Sprite;
    }


    public override void UpdateHp(int hp,int maxHp){
        HpSlider.value = hp / maxHp;
        HpText.text = hp.ToString();
        HpMaxText.text = "/" + maxHp;
    }

    public override void UpdateMp(int mp){
        MpText.text = mp.ToString();
    }

    public override void UpdateBuffs(Target t){


    }
}
