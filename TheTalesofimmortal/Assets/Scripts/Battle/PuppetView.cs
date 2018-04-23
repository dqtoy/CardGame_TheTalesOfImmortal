using UnityEngine;
using UnityEngine.UI;

public class PuppetView : TargetView {


    private Text HpText;
    private Text AtkText;
    private Image ProfileImage;

    void Start () {
        ProfileImage = GetComponentInChildren<Image> ();
        Text[] ts = GetComponentsInChildren<Text>();
        HpText = ts[0];
        AtkText = ts[1];
    }

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
