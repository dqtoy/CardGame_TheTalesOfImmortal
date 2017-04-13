using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Welcome : MonoBehaviour {
	
    public MovieTexture welcomeMov;
    public Image welcomeLogo;
    public Text welcomeText;
    public AudioSource soundLogo;
    private bool isDrawMovie = true;
    private bool isShowMessage = false;
    private bool isShowLogo = false;
    private GUIStyle _labelStyle;
    private float _startShowLabelTime;
    private float _startTwinkleTime;

    void Start () {
        welcomeLogo.gameObject.transform.localScale = new Vector3(0, 0, 1);
        _labelStyle = new GUIStyle();
        _labelStyle.fontSize = 60;
        _labelStyle.normal.textColor = new Color(1, 1, 1,1);
        welcomeMov.loop = false;
        welcomeMov.Play();
    }
	
	void Update () {
        if (isDrawMovie)
        {
            if (Input.GetMouseButtonDown(0) && !isShowMessage)
            {
                isShowMessage = true;
                _startShowLabelTime = Time.time;
            }
            else if (Input.GetMouseButtonDown(0) && isShowMessage)
            {
                isShowMessage = false;
                StopMovie();
            }
        }
        if (isDrawMovie != welcomeMov.isPlaying)
            StopMovie();
        
        if (isShowMessage)
        {
            if (Time.time - _startShowLabelTime >= 3)
                isShowMessage = false;
        }

        if (isShowLogo && (Time.time - _startTwinkleTime > 4))
        {
            TextTwinkle();
            _startTwinkleTime = Time.time;
        }

        if (Input.GetMouseButtonDown(0) && isShowLogo)
        {
            Debug.Log("GoToPanel");
            isShowMessage = false;
            isShowLogo = false;
        }
	}

	void OnGUI(){
		if (isDrawMovie) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), welcomeMov);
		}
        if (isShowMessage )
        {            
            GUI.Label(new Rect(Screen.width / 2 - 300, 1000, 800, 200), "再次点击退出视频播放", _labelStyle);
        }
	}
   

//    IEnumerator WaitAndDisappear(){
//        yield return new WaitForSeconds(3);
//        Destroy(_label);
//        isShowMessage = false;
//    }

    void StopMovie(){
        welcomeMov.Stop();
        isDrawMovie = false;
        CallInLogo();
    }

    void CallInLogo(){
        isShowLogo = true;
        soundLogo.Play();
        welcomeLogo.transform.DOBlendableScaleBy(new Vector3(1.3f,1.3f,0f),0.6f);
        StartCoroutine(WaitAndShrink());
    }

    IEnumerator WaitAndShrink(){
        yield return new WaitForSeconds(0.5f);
        welcomeLogo.transform.DOBlendableScaleBy(new Vector3(-0.3f, -0.3f, 0f), 0.25f);
    }

    void TextTwinkle(){
        welcomeText.DOFade(0, 1.5f);
        StartCoroutine(WaitAndFade());
    }

    IEnumerator WaitAndFade(){
        yield return new WaitForSeconds(2f);
        welcomeText.DOFade(1, 1.5f);
    }
}
