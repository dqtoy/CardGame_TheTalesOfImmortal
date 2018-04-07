using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour {
	private Image ProfileImage;
	private Text ManaText;
	private Slider HpSlider;

	void Start () {
		ProfileImage = GetComponentInChildren<Image> ();
		ManaText = GetComponentInChildren<Text> ();
		HpSlider = GetComponentInChildren<Slider> ();
	}
	

}
