using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour {

    Slider _slider;
    [SerializeField]
    private float time;
    private float SetTime;

    private bool TimerStartflg;
    public bool TimerFinishflg;

    private GameController Gb;

	// Use this for initialization
	void Start () {
        // スライダーを取得する
        _slider = gameObject.GetComponent<Slider>();

        SetTime = time;
        _slider.maxValue = SetTime;
        _slider.value = SetTime;

        TimerStartflg = false;
        TimerFinishflg = false;
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (TimerStartflg)
        {
            time -= Time.deltaTime;
            _slider.value = time;
        }
        if (Gb.GameState == GameController.EState.Main)
        {
            Off_TimeStartFlg();
            TimerFinishflg = false;
        }
        if (time < 0 && TimerFinishflg == false)
        {
            TimerFinishflg = true;
        }
	}
    public void TimerInit()
    {
        _slider.maxValue = SetTime;
        _slider.value = SetTime;
    }

    public void On_TimeStartFlg()
    {
        time = SetTime;
        TimerStartflg = true;
    }
    public void Off_TimeStartFlg()
    {
        TimerStartflg = false;
        time = SetTime;
    }
}
