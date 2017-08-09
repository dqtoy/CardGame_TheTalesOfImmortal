using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

    public GameObject LoadingRotate;
    private Vector3 r;
    private bool isLoading = false;
    private float t;
    private float t0;
    private int value;
    private float loadingTime;

    void Start(){
        this.gameObject.SetActive(false);
    }

    public void StartLoading(float t1){
        ResetLoading();
        loadingTime = t1;
    }

    void ResetLoading(){
        this.gameObject.SetActive(true);
        r = Vector3.zero;
        isLoading = true;
        t0 = Time.time;
        value = 0;
    }

	void Update () {
        if (isLoading)
        {
            t = Time.time - t0;

            r = new Vector3(0, 0, t * 90);
            LoadingRotate.transform.localEulerAngles = r; 
            value = (int)((t / loadingTime) * 100);
//            Debug.Log("t = " + t + "/t v = " + value);

            if (value >= 100)
            {
                isLoading = false;
                StopLoading();
            }
        }
	}

    void StopLoading(){
        this.gameObject.SetActive(false);
    }
        
}
