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
    private PartsScale m_partsScale;

    // Use this for initialization
    void Start() {

        // null チェック
        for (int i = 0; i < ShrinkPointMax; ++i)
        {
            if (m_jointExtends[i] == null) Debug.LogError("JointExtendスクリプトをセットしてください。");
        }

        // 状態 , パラメータ初期化
        for (int i = 0; i < ShrinkPointMax; ++i)
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

        m_partsScale = GetComponent<PartsScale>();
        if (m_partsScale == null) Debug.LogError("PartsScaleをアタッチしてください。");
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

        // 縮こまり一回だけ呼ぶ
        if (EState.NowShrink != m_state[idx])
        {
            m_partsScale.PartsVariationSetting(Conversion(eShrinkPoint), PartsScale.SubTraction.SubChange, m_paramTable.partsSpeed.x);
            m_partsScale.PartsVariation();
        }

        m_state[idx] = EState.NowShrink;
       
    }

    // 伸ばし開始
    public void StartExtend(EShrinkPoint eShrinkPoint)
    {
        int idx = (int)eShrinkPoint;
        if (m_state[idx] != EState.NowShrink) return;

        m_jointExtends[idx].StartExtend();
        m_partsScale.PartsVariationSetting(Conversion(eShrinkPoint), PartsScale.SubTraction.AddChange, m_paramTable.partsSpeed.y);
        m_partsScale.PartsVariation();

        m_state[idx] = EState.NowExtend;

    }

    // enum分けちゃったゆえのやつ
    private PartsScale.Parts Conversion(EShrinkPoint eShrinkPoint)
    {
        switch(eShrinkPoint)
        {
            case EShrinkPoint.RightHand:
                return PartsScale.Parts.RightHand;
            case EShrinkPoint.LeftHand:
                return PartsScale.Parts.LeftHand;
            case EShrinkPoint.RightFoot:
                return PartsScale.Parts.RightCalf;
            case EShrinkPoint.LeftFoot:
                return PartsScale.Parts.LeftCalf;
        }
        return PartsScale.Parts.AllParts;
    }
}
