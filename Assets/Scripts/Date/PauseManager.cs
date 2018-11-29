using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    private bool m_pauseFlg;
    public bool IsPause
    {
        get { return m_pauseFlg; }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPause(bool flg)
    {
        if (m_pauseFlg == flg) return;
        if (flg)
        {
            // ポーズ出す
            Debug.Log(Time.timeScale);
            Time.timeScale = 0;
        }
        else
        {
            // ポーズ外す
        }
    }
    
}
