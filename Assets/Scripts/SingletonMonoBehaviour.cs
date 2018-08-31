using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviorを継承したシングルトン基底クラス
/// アクセス時又はAwake時に初期化が完了している
/// （Awake前にアクセスしても使えるように）
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviourInitter where T : MonoBehaviourInitter{

    private static T m_instance;
    public static T GetInstance
    {
        get {

            if(m_instance == null)
            {
                // シーン内から取得
                // （※一つである前提）
                if((m_instance = FindObjectOfType<T>()) == null)
                {
                    Debug.LogError(typeof(T) + "がシーン内に存在しません。");
                }
                else
                {
                    // アクセス時の初期化
                    m_instance.Initter();   
                }
            }

            return m_instance;
        }
    }

    // Awake時初期化（このメソッドは継承させない : sealed）
    protected sealed override void Awake()
    {
        // ここでアクセス
        if(this != GetInstance)
        {
            Debug.LogError(typeof(T) + "が重複しています。");
        }
    }
}

/// <summary>
/// 初期化メソッド付きMonoBehaviour
/// </summary>
public class MonoBehaviourInitter : MonoBehaviour
{
    private bool m_isInited = false;

    public void Initter()
    {
        if (!m_isInited)
        {
            m_isInited = true;
            Init();
        }
    }

    /// <summary>
    /// 初アクセス又はAwake時のどちらか
    /// </summary>
    protected virtual void Init() { }

    protected virtual void Awake() { }
}
