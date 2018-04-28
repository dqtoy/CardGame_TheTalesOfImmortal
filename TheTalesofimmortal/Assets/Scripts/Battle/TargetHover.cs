
using UnityEngine;
using UnityEngine.UI;

public class TargetHover : MonoBehaviour {

    private Color org;

    void Start(){
        org = GetComponent<Image>().color;
    }

	public void HighLight(){
		Image i	= GetComponent<Image> ();
		i.color = Color.black;
	}
	public void Recover(){
		Image i = GetComponent<Image> ();
        i.color = org;
	}
}
