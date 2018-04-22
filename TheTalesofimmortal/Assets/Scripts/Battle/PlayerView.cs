using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {


	private Slider HpSlider;
	private Text MpText;
	private Button[] Buffs;
	private Image ProfileImage;

	void Start () {
		ProfileImage = GetComponentInChildren<Image> ();
		MpText = GetComponentInChildren<Text> ();
		HpSlider = GetComponentInChildren<Slider> ();
		Buffs = GetComponentsInChildren<Button> ();
	}
	

}
