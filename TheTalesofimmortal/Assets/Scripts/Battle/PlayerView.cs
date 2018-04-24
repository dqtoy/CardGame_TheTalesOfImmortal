using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerView : TargetView {


	public Slider HpSlider;
    public Text HpText;
    public Text HpMaxText;
	public Text MpText;
	public Button[] Buffs;
	public Image ProfileImage;

//	void Start () {
//        Debug.Log("PlayerView Start");
//		ProfileImage = GetComponentInChildren<Image> ();
//        Text[] t = GetComponentsInChildren<Text>();
//        MpText = t[0];
//        HpText = t[1];
//        HpMaxText = t[2];
//		HpSlider = GetComponentInChildren<Slider> ();
//		Buffs = GetComponentsInChildren<Button> ();
//	}
	


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

    public override void UpdateBuffs(Target t){


    }
}
