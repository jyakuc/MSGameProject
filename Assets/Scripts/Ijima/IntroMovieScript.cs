using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroMovieScript : MonoBehaviour {

    private GameObject ENDFLG;
    private int SceneChangeTime;     //遅延フレーム数
    private bool SceneChangeFlg;
    private int flame;
    private bool OneKeyFlag;
    private bool IntroFinishFlg;

    // Use this for initialization
    void Start () {
        SceneChangeTime = 60;
        flame = 0;
        OneKeyFlag = false;
        IntroFinishFlg = false;
        AudioManager.GetInstance.ChangeVolume(0.2f, 1.0f);
        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_MOVIE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
    }
	
	// Update is called once per frame
	void Update () {
        if ((!IntroFinishFlg) && (!OneKeyFlag)) {
            //EndFlgオブジェクトがTimeline上でactiveにされた時、シーン遷移する
            ENDFLG = GameObject.Find("EndFlg");
            if (ENDFLG != null){
                IntroFinishFlg = true;
            }
        }
        if (((Input.anyKeyDown == true) && (!OneKeyFlag)) || (IntroFinishFlg == true)){
            SceneChangeFlg = true;
            OneKeyFlag = true;
            AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
            IntroFinishFlg = false;
        }
        if (SceneChangeFlg == true){
            flame++;
            if (flame == SceneChangeTime){
                SceneController.GetInstance.ChangeScene("WarmingUp", 2);
            }
        }
    }
}
