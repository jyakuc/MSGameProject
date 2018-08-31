using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTitle : MonoBehaviour {

	public void OnButton1_Click()
    {
        SceneController.GetInstance.ChangeScene(_sceneName: "StageSelectScene");
    }

    public void OnButton2_Click()
    {
        SceneController.GetInstance.ChangeScene(_sceneName: "GameScene");
    }

    public void OnButton3_Click()
    {
        SceneController.GetInstance.ChangeScene(_sceneName: "TitleScene");
    }
}
