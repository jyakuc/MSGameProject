using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour {

    public int SceneChangeTime;
    private bool SceneChangeFlg;
    private int flame;

    private void Start()
    {
        SceneChangeFlg = false;
        flame = 0;
        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
    }

    void Update()
    {
        if (SceneChangeFlg == true)
        {
            flame++;
        }
        if (flame == SceneChangeTime)
        {
            SceneController.GetInstance.ChangeScene("TitleScene");
        }
    }
    
    public void ChangeScene()
    {
        if (SceneChangeFlg) return;
        SceneChangeFlg = true;
        AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
    }
}
