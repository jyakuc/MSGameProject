using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour {

    public int SceneChangeTime;
    private bool SceneChangeFlg;
    private int flame;

    private StageCreate.SelectingStage selectingStage;
    private FadeController fadeController;
    public GameController gameController;

    private void Start()
    {
        SceneChangeFlg = false;
        flame = 0;
        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
        fadeController = FindObjectOfType<FadeController>();
    }

    void Update()
    {
        if (SceneChangeFlg == true)
        {
            flame++;
        }
        if (flame == SceneChangeTime)
        {
            switch (selectingStage)
            {
                case StageCreate.SelectingStage.Colloseum:
                    fadeController.m_onFinished += LoadStageColloseum_UnloadHoruhoruMountain;
                    fadeController.Play(false,1);

                    break;
                case StageCreate.SelectingStage.HoruhoruMountain:
                    fadeController.m_onFinished += LoadStageHoruhoruMountain_UnloadColdSleepMountain;
                    fadeController.Play( false, 1);

                    break;
                case StageCreate.SelectingStage.ColdSleepMountain:
                    SceneController.GetInstance.ChangeScene("ResultScene");
                    break;
            }

            //SceneController.GetInstance.ChangeScene("TitleScene");
        }
    }
    
    public void ChangeScene(StageCreate.SelectingStage _selectingStage)
    {
        if (SceneChangeFlg) return;
        selectingStage = _selectingStage;
        SceneChangeFlg = true;
        AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
    }

    // コロシアムステージの破棄＆ホルホルマウンテンの読み込み
    public void LoadStageColloseum_UnloadHoruhoruMountain()
    {
        gameController.Reset(StageCreate.SelectingStage.Colloseum,StageCreate.SelectingStage.HoruhoruMountain);
    }
    // ホルホルマウンテンの破棄＆雪山ステージの読み込み
    public void LoadStageHoruhoruMountain_UnloadColdSleepMountain()
    {
        gameController.Reset(StageCreate.SelectingStage.HoruhoruMountain,StageCreate.SelectingStage.ColdSleepMountain);
    }
}
