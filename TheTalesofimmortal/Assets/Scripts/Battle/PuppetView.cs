using UnityEngine;
using UnityEngine.UI;

public class PuppetView : TargetView {


    public Text HpText;
    public Text AtkText;
    public Image ProfileImage;

    public override void UpdateProfile(string path){
        ProfileImage.sprite = Resources.Load(path) as Sprite;
    }

    public override void UpdateHp(int hp,int maxHp){
        HpText.text = hp.ToString();
    }

    public override void UpdateAtk(int atk){
        AtkText.text = atk.ToString();
    }


}
