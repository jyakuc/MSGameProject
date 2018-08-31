using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneAutoLoader {

    // シーン読み込み前に実行される
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadManagerScene()
    {
        string name = "ManagerScene";

        // ManagerSceneが読み込まれていない時だけLoadする
        if (!SceneManager.GetSceneByName(name).IsValid())
        {
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

       
    }
}
