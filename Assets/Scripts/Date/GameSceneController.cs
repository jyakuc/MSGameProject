using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour {

    enum EState
    {
        Wait,
        FrameCount,
        Transition,
        NotUpdate,
        Unload,
        Load,
        Restart,
    }

    public int SceneChangeTime;
    [SerializeField, Range(0.5f, 5.0f)]
    public float FadeTimeSecond;
    private int frame;

    private StageCreate m_stageCreater;
    private FadeController fadeController;

    //public GameController gameController;
    private EState m_state;

    private void Start()
    {
        frame = 0;
        m_state = EState.Wait;

        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
        fadeController = FindObjectOfType<FadeController>();
        m_stageCreater = FindObjectOfType<StageCreate>();
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
            case EState.Unload:
                Debug.Log("Unload");
                m_stageCreater.Unload();
                StartCoroutine(UnLoadEnumrator());
                m_state = EState.NotUpdate;
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
        m_state = EState.Unload;
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
        m_state = EState.Load;
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
}
