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

    public void UpdatePlayerView(Player player){
        UpdateProfile("HeadImage/hero1");
        UpdateHp(player.HP, player.HpMax);
        UpdateMp(player.MP);
        UpdateBuffs(player as Target);
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

    public override void UpdateBuffs(Target t){


    }
}
