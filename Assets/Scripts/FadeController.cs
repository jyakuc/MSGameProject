using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// CanvasGroupがアタッチされる
[RequireComponent(typeof(CanvasGroup))]

public class FadeController : MonoBehaviour {

    // フェード状態
    private enum FadeState
    {
        FadeIn,
        FadeOut,
        None
    }
    private FadeState m_fadeState = FadeState.None;

    // フェード中かどうか
    public bool IsFade
    {
        get {

            return m_fadeState != FadeState.None;
        }
    }

    // フェード用キャンバス
    private CanvasGroup m_canvasGroup;
    private CanvasGroup CanvasGroup
    {
        get {
            if(m_canvasGroup == null)
            {
                if((m_canvasGroup = GetComponent<CanvasGroup>()) == null)
                { 
                    m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return m_canvasGroup;
        }
    }

    // フェードのアルファ値
    public float Alpha
    {
        get { return CanvasGroup.alpha; }
        set { CanvasGroup.alpha = value; }
    }

    // フェード時間
    [SerializeField]
    private float m_duration = 1.0f;
    public float Duration
    {
        get { return m_duration; }
    }

    // タイムスケールを無視するかどうか
    [SerializeField]
    private bool m_ignoreTimeScale = true;

    // フェード終了後のコールバック
    public event Action m_onFinished = null;

    
	// Update is called once per frame
	void Update () {
        if (!IsFade) return;
        float fadeSpeed = 1.0f / m_duration;
        if (m_ignoreTimeScale)
        {
            fadeSpeed *= Time.unscaledDeltaTime;
        }
        else
        {
            fadeSpeed *= Time.deltaTime;
        }

        if(m_fadeState == FadeState.FadeIn)
        {
            Alpha += fadeSpeed * 1.0f;
        }
        else
        {
            Alpha += fadeSpeed * -1.0f;
        }
        
        // Fade中
        if (Alpha > 0 && Alpha < 1) return;

        m_fadeState = FadeState.None;
        //this.enabled = false;

        if (m_onFinished != null)
        {
 //           Debug.Log("FadeFade");
            m_onFinished();
        }
	}

    /// <summary>
    /// Objectのフェード開始
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_isFadeOut"></param>
    /// <param name="_duration"></param>
    public static void Begin(GameObject _target,bool _isFadeOut,float _duration)
    {
        FadeController fadeController = _target.GetComponent<FadeController>();
        if(fadeController == null)
        {
            fadeController = _target.AddComponent<FadeController>();
        }
        fadeController.enabled = true;

        fadeController.Play(_isFadeOut,_duration);

    }

    /// <summary>
    /// 実際にフェードを開始
    /// </summary>
    /// <param name="_isFadeOut"></param>
    /// <param name="_duration"></param>
    /// <param name="_ignoreTimeScale"></param>
    /// <param name="_onFinished"></param>
    public void Play(bool _isFadeOut,float _duration,bool _ignoreTimeScale = true, Action _onFinished = null)
    {
        this.enabled = true;

        m_ignoreTimeScale = _ignoreTimeScale;
        m_onFinished = _onFinished;

        if (_isFadeOut)
        {
            Alpha = 1;
            m_fadeState = FadeState.FadeOut;
            m_duration = _duration;
        }
        else
        {
            Alpha = 0;
            m_fadeState = FadeState.FadeIn;

            m_duration = _duration;
        }

    }

    /// <summary>
    /// フェード停止
    /// </summary>
    public void Stop()
    {
        m_fadeState = FadeState.None;
        this.enabled = false;
    }

    public void FadeIn()
    {

    }

    public void FadeOut()
    {

    }
}
