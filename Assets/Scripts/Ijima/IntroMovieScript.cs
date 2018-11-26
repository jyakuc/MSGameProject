using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroMovieScript : MonoBehaviour {

    public int MovieTime;
    private int SceneChangeTime;     //遅延フレーム数
    private bool SceneChangeFlg;
    private int flame;
    private bool OneKeyFlag = false;
    private bool IntroFinishFlg;

    // Use this for initialization
    void Start () {
        SceneChangeTime = 60;
        flame = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if ((!IntroFinishFlg) && (!OneKeyFlag)) {
            flame++;
            if (flame >= MovieTime)
            {
                IntroFinishFlg = true;
            }
        }
        if (((Input.anyKeyDown == true) && (!OneKeyFlag)) || (IntroFinishFlg == true))
        {
            SceneChangeFlg = true;
            OneKeyFlag = true;
            AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
            IntroFinishFlg = false;
            flame = 0;
        }
        if (SceneChangeFlg == true)
        {
            flame++;
            if (flame == SceneChangeTime)
            {
                SceneController.GetInstance.ChangeScene("TitleScene", 2);
            }
        }
    }
}
