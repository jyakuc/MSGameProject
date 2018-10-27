using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
/// 

//////SEの再生 PlaySE0~3の4ライン
////AudioManager.GetInstance.PlaySE0 (AUDIO.SE_BUTTON);

//////BGM再生。AUDIO.BGM_BATTLEがBGMのファイル名
////AudioManager.GetInstance.PlayBGM (AUDIO.BGM_BATTLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);

//////BGMフェードアウト
////AudioManager.GetInstance.FadeOutBGM ();


public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //ボリューム保存用のkeyとデフォルト値
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 1.0f;
    private const float SE_VOLUME_DEFULT = 1.0f;

    //BGMがフェードするのにかかる時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 1.0f;
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
    private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

    //次流すBGM名、SE名
    private string _nextBGMName;
    private string _nextSEName;

    //BGMをフェードアウト中か
    private bool _isFadeOut = false;

    //BGM用、SE用に分けてオーディオソースを持つ
    public AudioSource AttachBGMSource;
    public AudioSource AttachSESource0;
    public AudioSource AttachSESource1;
    public AudioSource AttachSESource2;
    public AudioSource AttachSESource3;

    //全Audioを保持
    private Dictionary<string, AudioClip> _bgmDic, _seDic;

    //=================================================================================
    //初期化
    //=================================================================================

    private void Awake()
    {
        if (this != GetInstance){
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        //リソースフォルダから全SE&BGMのファイルを読み込みセット
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }
        AttachBGMSource = gameObject.AddComponent<AudioSource>();
        AttachSESource0 = gameObject.AddComponent<AudioSource>();
        AttachSESource1 = gameObject.AddComponent<AudioSource>();
        AttachSESource2 = gameObject.AddComponent<AudioSource>();
        AttachSESource3 = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
        AttachSESource0.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
        AttachSESource1.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
        AttachSESource2.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
        AttachSESource3.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);

    }

    //=================================================================================
    //SE
    //=================================================================================

    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void PlaySE0(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE0", delay);
    }

    private void DelayPlaySE0()
    {
        AttachSESource0.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }

    public void PlaySE1(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE1", delay);
    }

    private void DelayPlaySE1()
    {
        AttachSESource1.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }
    public void PlaySE2(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE2", delay);
    }

    private void DelayPlaySE2()
    {
        AttachSESource2.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }
    public void PlaySE3(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE3", delay);
    }

    private void DelayPlaySE3()
    {
        AttachSESource3.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }
    //=================================================================================
    //BGM
    //=================================================================================

    /// <summary>
    /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
    /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "という名前のBGMがありません");
            return;
        }

        //現在BGMが流れていない時はそのまま流す
        if (!AttachBGMSource.isPlaying)
        {
            _nextBGMName = "";
            AttachBGMSource.clip = _bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
        else if (AttachBGMSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }

    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
    {
        _bgmFadeSpeedRate = fadeSpeedRate;
        _isFadeOut = true;
    }

    private void Update()
    {
        if (!_isFadeOut)
        {
            return;
        }

        //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
        AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
        if (AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            _isFadeOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }

    }

    //=================================================================================
    //音量変更
    //=================================================================================

    /// <summary>
    /// BGMとSEのボリュームを別々に変更&保存
    /// </summary>
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource0.volume = SEVolume;
        AttachSESource1.volume = SEVolume;
        AttachSESource2.volume = SEVolume;
        AttachSESource3.volume = SEVolume;

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }
}
