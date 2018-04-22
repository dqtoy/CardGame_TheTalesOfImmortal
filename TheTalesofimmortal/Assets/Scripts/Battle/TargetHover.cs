
using UnityEngine;
using UnityEngine.UI;

public class TargetHover : MonoBehaviour {
	public void In(){
		Debug.Log ("Hovering " + this.gameObject.name);
		Image i	= GetComponent<Image> ();
		i.color = Color.black;
	}
	public void Leave(){
		Debug.Log ("Leaving " + this.gameObject.name);
		Image i = GetComponent<Image> ();
		i.color = Color.white;
	}
}
