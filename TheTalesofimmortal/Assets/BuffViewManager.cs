
using UnityEngine;
using UnityEngine.UI;

public class BuffViewManager : MonoBehaviour {

	private Button[] _buffs;
	void Start () {
		_buffs = GetComponentsInChildren<Button> ();
	}
	
	
}
