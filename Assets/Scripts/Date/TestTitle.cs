using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTitle : MonoBehaviour {

    public int SceneChangeTime;     //遅延フレーム数
    private bool SceneChangeFlg;
    private int flame;


    private void Start(){
        SceneChangeFlg = false;
        flame = 0;
        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_TITLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
    }

    void Update(){
        if (Input.anyKeyDown == true){
            SceneChangeFlg = true;
            AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
        }
        if (SceneChangeFlg == true){
            flame++;
        }
        if (flame == SceneChangeTime){
            SceneController.GetInstance.ChangeScene("GameScene");
        }
    }
}
