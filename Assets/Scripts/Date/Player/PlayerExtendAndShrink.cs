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
        NowExtend,
        Max
    }

    // キャスト書くのめんどくさい用
    const int ShrinkPointMax = (int)EShrinkPoint.Max;
    // 手足の状態
    private EState[] m_state = new EState[ShrinkPointMax];

    // 共通の値
    [SerializeField]
    private PlayerShrinkAndExtend_ParamTable m_paramTable;

    [SerializeField]
    [Header("JointExtendスクリプト")]
    private JointExtend[] m_jointExtends = new JointExtend[ShrinkPointMax];
    
	// Use this for initialization
	void Start () {
       
        // null チェック
        for(int i=0;i<ShrinkPointMax;++i)
        {
            if (m_jointExtends[i] == null) Debug.LogError("JointExtendスクリプトをセットしてください。");
        }

        // 状態 , パラメータ初期化
        for(int i = 0; i < ShrinkPointMax; ++i)
        {
            m_state[i] = EState.Idle;
            
            if (m_jointExtends[i].IsHand)
            {
                // 手のパラメータを設定
                m_jointExtends[i].SetShrinkParameters(
                    m_paramTable.shrinkInitPower_Hand,
                    m_paramTable.shrinkMaxPower_Hand,
                    m_paramTable.shrinkAddPower_Hand);
                m_jointExtends[i].SetExtendParameters(
                    m_paramTable.extendInitPower_Hand,
                    m_paramTable.extendMaxPower_Hand,
                    m_paramTable.extendAddPower_Hand);
            }
            else
            {
                // 足のパラメータを設定
                m_jointExtends[i].SetShrinkParameters(
                    m_paramTable.shrinkInitPower_Foot,
                    m_paramTable.shrinkMaxPower_Foot,
                    m_paramTable.shrinkAddPower_Foot);
                m_jointExtends[i].SetExtendParameters(
                    m_paramTable.extendInitPower_Foot,
                    m_paramTable.extendMaxPower_Foot,
                    m_paramTable.extendAddPower_Foot);
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
        int idx = (int)eShrinkPoint;
        m_jointExtends[idx].StartShrink();
        m_state[idx] = EState.NowShrink;

        /*
        if (m_state[(int)eShrinkPoint] != EState.Idle) return;
        m_state[(int)eShrinkPoint] = EState.NowShrink;
        m_extendForce[(int)eShrinkPoint] = m_paramTable.extendPower[(int)eShrinkPoint];
        */
    }

    // 伸ばし開始
    public void StartExtend(EShrinkPoint eShrinkPoint)
    {
        int idx = (int)eShrinkPoint;
        if (m_state[idx] != EState.NowShrink) return;
        m_jointExtends[idx].StartExtend();
        m_state[idx] = EState.NowExtend;
        /*
        if (!(m_state[(int)eShrinkPoint] == EState.NowShrink || m_state[(int)eShrinkPoint] == EState.MaxShrink)) return;
        m_state[(int)eShrinkPoint] = EState.NowExtend;
        */
    }

    /// ===============================================================
    /// クラス内処理
    /// ===============================================================
    
    /*
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
    */

    /*
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
    */

    /*
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
    */
}
