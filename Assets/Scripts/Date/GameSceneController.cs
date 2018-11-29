using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour {


    private int MaxPlayer = 6;

    enum EState
    {
        Wait,
        FrameCount,
        Transition,
        NotUpdate,
        Unstage,
        UnLoad,
        DisplayLoad,
        WaitInput,
        Load,
        Restart,
    }

    public int SceneChangeTime;
    [SerializeField, Range(0.5f, 5.0f)]
    public float FadeTimeSecond;
    private int frame;

    private StageCreate m_stageCreater;
    private LoadCreate m_LoadCreater;
    private FadeController fadeController;
    private ArtArmatureSave armatureSave;

    private bool[] StDownflg;

    private EState m_state;

    private void Start()
    {
        frame = 0;
        m_state = EState.Wait;

        BgmSet();
        fadeController = FindObjectOfType<FadeController>();
        m_stageCreater = FindObjectOfType<StageCreate>();
        m_LoadCreater = FindObjectOfType<LoadCreate>();
        armatureSave = FindObjectOfType<ArtArmatureSave>();
        MaxPlayer = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum : 6;
        StDownflg = new bool[MaxPlayer];
        for (int i = 0; i < MaxPlayer; i++)
        {
            StDownflg[i] = false;
        }
            
    }

    void Update()
    {
        switch (m_state)
        {
            case EState.FrameCount:
                Debug.Log("FrameCount");
                frame++;
                if (frame == SceneChangeTime)
                    m_state = EState.Transition;
                break;
            case EState.Transition:
                Debug.Log("Trasition");
                frame = 0;
                fadeController.Play(false, 1);
                fadeController.m_onFinished += FadeInFinish;
                m_state = EState.NotUpdate;
                break;
            case EState.Unstage:
                Debug.Log("Unstage");
                m_stageCreater.Unstage();
                armatureSave.SetAllActives(false);
                StartCoroutine(UnLoadEnumrator());
                m_state = EState.NotUpdate;
                break;
            case EState.UnLoad:
                Debug.Log("UnLoad");
                m_LoadCreater.Unload();
                m_state = EState.NotUpdate;
                break;
            case EState.DisplayLoad:
                Debug.Log("ロード画面");
                // ロード画面生成
                m_LoadCreater.Loadinfo();
                m_state = EState.WaitInput;
                break;
            case EState.WaitInput:
                Debug.Log("入力待ち");
                for (int i = 0; i < MaxPlayer; i++)
                {
                    Debug.Log(StDownflg[0]);
                    if (StDownflg[i]) continue;
                    StDownflg[i] = Input.GetButtonDown("ST_Player" + (i + 1));
                    if (!StDownflg[i])
                        return;
                }
                m_state = EState.Load;
                break;
            case EState.Load:
                Debug.Log("Load");
                m_stageCreater.Load();
                m_state = EState.Restart;
                break;
        }
        
    }
    
    public void ChangeScene()
    {
        if (m_state != EState.Wait) return;

        AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);

        // 最後のステージならリザルトシーンに移行
        if (m_stageCreater.Stages == StageCreate.SelectingStage.HoruhoruMountain)
            SceneController.GetInstance.ChangeScene("ResultScene");
        else
            m_state = EState.FrameCount;
    }

    // フェードインが終了したコールバック関数
    public void FadeInFinish()
    {
        fadeController.m_onFinished -= FadeInFinish;
        m_state = EState.Unstage;
    }

    // フェードアウトが終了したコールバック関数
    public void FadeOutFinish()
    {
        fadeController.m_onFinished -= FadeOutFinish;
        m_state = EState.Wait;
    }

    // 全て破棄されるまで処理を進めない
    IEnumerator UnLoadEnumrator()
    {
        //yield return new WaitUntil(()=> m_stageCreater.IsDestroy()==true);
        yield return new WaitForSeconds(FadeTimeSecond);
        m_state = EState.DisplayLoad;
    }

    public bool IsRestart()
    {
        if (m_state != EState.Wait) return true;
        return false;
    }

    public bool IsRestartReady()
    {
        if (m_state == EState.Restart) return true;
        return false;
    }

    public void ReStart()
    {
        fadeController.Play(true, 1);
        fadeController.m_onFinished += FadeOutFinish;
    }

    public void BgmSet()
    {
        if (SceneManager.GetActiveScene().name == "WarmingUp")
        {
            AudioManager.GetInstance.PlayBGM(AUDIO.BGM_RESULT, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
            AudioManager.GetInstance.ChangeVolume(1.0f, 1.0f);
        }
        else
        {
            AudioManager.GetInstance.PlayBGM(AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
            AudioManager.GetInstance.ChangeVolume(1.0f, 1.0f);
        }
    }
}
