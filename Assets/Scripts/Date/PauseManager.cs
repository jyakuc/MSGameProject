using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    private static bool gPauseFlg = false;
    public static bool IsPause
    {
        get { return gPauseFlg; }
    }
    private static int saveID = 0;

    public static void OnPause(int playerID)
    {
        if (gPauseFlg && saveID != playerID) return;
        gPauseFlg = !gPauseFlg;
        saveID = playerID;
        if (gPauseFlg)
        {
            // ポーズ出す
            Time.timeScale = 0;
        }
        else
        {
            // ポーズ外す
            Time.timeScale = 1;
        }
    }

    // ポーズからタイトル
    public static void PauseToTitle(int playerID)
    {
        if (playerID != saveID) return;
        saveID = -1;
        GameObject.Find("SceneController/FadeCanvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        SceneController.GetInstance.ChangeScene("TitleScene");
    }
}
