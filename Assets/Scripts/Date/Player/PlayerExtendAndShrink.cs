using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtendAndShrink : MonoBehaviour {
    public enum EShrinkPoint
    {
        RightHand,
        LeftHand,
        RightFoot,
        LeftFoot,
        Max
    }

    // 手足の状態
    public enum EState
    {
        Idle,
        NowShrink,
        MaxShrink,
        NowExtend,
        MaxExtend,
        Max
    }

    // キャスト書くのめんどくさい用
    const int ShrinkPointMax = (int)EShrinkPoint.Max;

    // 縮こまる位置（GameObject）
    [SerializeField]
    [Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    private GameObject[] m_shrinkPointObjs = new GameObject[ShrinkPointMax];
    // 制御するIKオブジェクト
    [SerializeField]
    [Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    private GameObject[] m_IKcontrolObjs = new GameObject[ShrinkPointMax];

    // 手足の状態
    private EState[]    m_state = new EState[ShrinkPointMax];
    // 手足の縮こまり具合
    private float[]     m_shrinkTime = new float[ShrinkPointMax];
    // 手足の伸ばす力
    private Vector3[]   m_extendForce = new Vector3[ShrinkPointMax];


    // 共通の値
    [SerializeField]
    private PlayerShrinkAndExtend_ParamTable m_paramTable;
    
	// Use this for initialization
	void Start () {
        // Nullチェック
        for (int i = 0; i < ShrinkPointMax; ++i)
        {
            if (m_shrinkPointObjs[i] == null)   Debug.LogError("縮こまりポイントのオブジェクトがありません。");
            if (m_IKcontrolObjs[i] == null)     Debug.LogError("IKコントロールオブジェクトがありません。");
        }

        // 状態初期化
        for(int i = 0; i < ShrinkPointMax; ++i)
        {
            m_state[i] = EState.Idle;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for(int i = 0; i< ShrinkPointMax; ++i)
        {
            switch (m_state[i])
            {
                // 縮こまり中
                case EState.NowShrink:
                    DoShrink(i);
                    break;
                // 最大縮こまり中
                case EState.MaxShrink:
                    StoringTheForce(i);
                    break;
                case EState.NowExtend:
                    ExtendForceRelease(i);
                    break;
            }

        }
	}

    // 手足の状態取得
    public EState GetState(EShrinkPoint eShrinkPoint)
    {
        return m_state[(int)eShrinkPoint];
    }

    // 縮こまり開始
    public void StartShrink(EShrinkPoint eShrinkPoint)
    {
        if (m_state[(int)eShrinkPoint] != EState.Idle) return;
        m_state[(int)eShrinkPoint] = EState.NowShrink;
        m_extendForce[(int)eShrinkPoint] = m_paramTable.extendPower[(int)eShrinkPoint];
    }

    // 伸ばし開始
    public void StartExtend(EShrinkPoint eShrinkPoint)
    {
        if (!(m_state[(int)eShrinkPoint] == EState.NowShrink || m_state[(int)eShrinkPoint] == EState.MaxShrink)) return;
        m_state[(int)eShrinkPoint] = EState.NowExtend;
    }

    /// ===============================================================
    /// クラス内処理
    /// ===============================================================
    
    // 縮こまる
    private void DoShrink(int idx)
    {
        Vector3 ikPos = m_IKcontrolObjs[idx].transform.position;
        Vector3 shrinkPos = m_shrinkPointObjs[idx].transform.position;

        // 縮こまり位置まで線形補間
        m_IKcontrolObjs[idx].transform.position = 
            Vector3.Lerp(ikPos, shrinkPos, m_shrinkTime[idx]);

        m_shrinkTime[idx] += m_paramTable.AddLerpTime * Time.fixedDeltaTime;
        Debug.Log(m_shrinkTime[idx]);
        // 最大まで縮こまったら遷移
        if (m_shrinkTime[idx] >= 1.0f)
        {
            m_state[idx] = EState.MaxShrink;
            m_shrinkTime[idx] = 0.0f;
        }
    }

    // 最大に縮こまって力を溜めてる
    private void StoringTheForce(int idx)
    {
        Vector3 ikPos = m_IKcontrolObjs[idx].transform.position;
        Vector3 shrinkPos = m_shrinkPointObjs[idx].transform.position;
        // 縮こまり位置まで線形補間
        m_IKcontrolObjs[idx].transform.position =
            Vector3.Lerp(ikPos, shrinkPos, 1);
        Vector3 add = m_paramTable.extendAddPower[idx];

        if (Mathf.Abs(m_extendForce[idx].x) >= Mathf.Abs(m_paramTable.extendMaxPower[idx].x))
            add.x = 0;
        if (Mathf.Abs(m_extendForce[idx].y) >= Mathf.Abs(m_paramTable.extendMaxPower[idx].y))
            add.y = 0;
        if (Mathf.Abs(m_extendForce[idx].z) >= Mathf.Abs(m_paramTable.extendMaxPower[idx].z))
            add.z = 0;

        m_extendForce[idx] += add;
    }

    // 溜めた力を解放
    private void ExtendForceRelease(int idx)
    {
        Rigidbody rigidbody = m_IKcontrolObjs[idx].GetComponent<Rigidbody>();
        //Vector3 worldVelocity = m_IKcontrolObjs[idx].transform.TransformDirection(transform.right) * m_extendForce[idx].x;
        //Debug.Log(worldVelocity);
        //Debug.Log(transform.right);
        rigidbody.AddForce(m_extendForce[idx], ForceMode.Impulse);

        m_state[idx] = EState.Idle;
    }
}
