using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : SingletonMonoBehaviour<SceneController> {

    [SerializeField]
    private FadeController m_fader = null;
    public bool IsFadeing
    {
        get{ return m_fader.IsFade;  }
    }

    public const float FADE_TIME = 0.5f;
    private float m_fadeTime = FADE_TIME;


    private string m_oldSceneName = "";
    public string OldSceneName
    {
        get { return m_oldSceneName; }
    }

    private string m_currentSceneName = "";
    public string CurrentSceneName
    {
        get { return m_currentSceneName; }
    }

    private string m_nextSceneName = "";
    public string NextSceneName
    {
        get { return m_nextSceneName; }
    }

    // フェード後のイベント
    public event Action onFadeOutFinished = null;
    public event Action onFadeInFinished = null;


    /// <summary>
    /// 初期化メソッド(Awake時か　それ以前のアクセス　の　1回でしか呼ばれない)
    /// </summary>
    protected override void Init()
    {
        base.Init();

        if(m_fader == null)
        {
            Reset();
        }

        // 最初のシーン名を設定
        m_currentSceneName = SceneManager.GetSceneAt(0).name;

        // 破棄されないようにする
        DontDestroyOnLoad(gameObject);

       // m_fader.gameObject.SetActive(false);
    }

    // コンポーネント追加時に実行
    private void Reset()
    {
        gameObject.name = "SceneController";

        // フェード用のキャンバス作成
        GameObject fadeCavas = new GameObject("FadeCanvas");
        fadeCavas.transform.SetParent(transform);
       // fadeCavas.SetActive(false);

        Canvas canvas = fadeCavas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        fadeCavas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        fadeCavas.AddComponent<GraphicRaycaster>();
        m_fader = fadeCavas.AddComponent<FadeController>();
        m_fader.Alpha = 0;

        //CreateFadeImage(fadeCavas , new Vector2(2000,2000));
    }

    /// <summary>
    /// フェード用イメージ作成
    /// </summary>
    /// <param name="_obj">親にするCanvas</param>
    /// <param name="_size">イメージサイズ</param>
    protected void CreateFadeImage(GameObject _obj,Vector2 _size)
    {
        GameObject imageObj = new GameObject("Image");
        imageObj.transform.SetParent(_obj.transform, false);
        imageObj.AddComponent<Image>().color = Color.black;
        imageObj.GetComponent<RectTransform>().sizeDelta = _size;
    }

    /// <summary>
    /// シーン変更
    /// </summary>
    /// <param name="_sceneName">遷移するシーンの名前</param>
    /// <param name="_fadaTime">フェードする時間</param>
    public void ChangeScene(string _sceneName , float _fadaTime = FADE_TIME)
    {
        if (IsFadeing)
        {
            Debug.LogError("フェード中にシーンを変更しています");
            return;
        }

        m_nextSceneName = _sceneName;
        m_fadeTime = _fadaTime;

        // フェードイン
        m_fader.gameObject.SetActive(true);
        m_fader.Play(_isFadeOut: false, _duration: m_fadeTime , _onFinished:OnFadeInFinish);

    }

    private void OnFadeInFinish()
    {
        if (onFadeInFinished != null)
        {
            onFadeInFinished();
        }
        Debug.Log("FadeIn:終わり");
        SceneManager.LoadScene(m_nextSceneName);

        m_oldSceneName = m_currentSceneName;
        m_currentSceneName = m_nextSceneName;

        StartCoroutine(LoadSceneWait(SceneManager.GetSceneByName(m_currentSceneName)));
        //フェードアウト
        //m_fader.gameObject.SetActive(true);
       
    }

    private void OnFadeOutFinish()
    {
        //m_fader.gameObject.SetActive(false);
        if(onFadeOutFinished != null)
        {
            onFadeOutFinished();
        }
        Debug.Log("FadeOut:終わり");
    }
    
    IEnumerator LoadSceneWait(Scene scene)
    {
        while(!scene.isLoaded)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(1);   
        m_fader.Play(_isFadeOut: true, _duration: m_fadeTime, _onFinished: OnFadeOutFinish);
        yield break;
    }
}
