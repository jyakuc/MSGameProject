using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour {

    public enum RayDirection
    {
        NoHit,
        Forward,
        Back
    }

    // Rayの判定を行うレイヤー
    private LayerMask m_mask;

    // 判定の有無
    private bool m_hitFlg;
    public bool Hit
    {
        get { return m_hitFlg; }
    }

    // うつ伏せか仰向けを取得
    private RayDirection m_rayDirection;
    public RayDirection Dir
    {
        get { return m_rayDirection; }
    }

    // 向きが変わったかどうか
    private bool m_isDiffDir;

    // 共通の値
    [SerializeField]
    private PlayerRay_ParamTable m_paramTable;

    // Use this for initialization
    void Start () {
        // Nullチェック
        if (m_paramTable == null) Debug.LogError("PlayerRay_ParamTableがアタッチされてません。");

        // レイヤー名からレイヤー番号取得
        int no = LayerMask.NameToLayer(m_paramTable.layerMaskName);
        m_mask = 1 << no;
        m_hitFlg = false;
        m_isDiffDir = false;
    }
	
	// Update is called once per frame
	void Update () {

        // 仰向け
        if (Physics.Raycast(transform.position, transform.forward, m_paramTable.rayDistance, m_mask))
        {
            if (m_rayDirection != RayDirection.Forward) m_isDiffDir = true;
            m_rayDirection = RayDirection.Forward;
            m_hitFlg = true;
        }
        // うつ伏せ
        else if (Physics.Raycast(transform.position, -transform.forward, m_paramTable.rayDistance, m_mask))
        {
            if (m_rayDirection != RayDirection.Back) m_isDiffDir = true;
            m_rayDirection = RayDirection.Back;
            m_hitFlg = true;
        }
        
        else
        {
            m_hitFlg = false;
        }

    }

    public bool IsDIffDirection()
    {
        return m_isDiffDir;
    }

    public void InitDiffDirection()
    {
        m_isDiffDir = false;
    }
}
