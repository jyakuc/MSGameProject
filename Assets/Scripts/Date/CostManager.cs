using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour {
    public struct PointType
    {
        public int art;
        public int critical;
        public int crush;
    }

    private Dictionary<int, CostParts> m_playerArtCostData = new Dictionary<int, CostParts>();  // 1ステージで獲得した芸術得点
    private Dictionary<int, float> m_playerBattleCostData = new Dictionary<int, float>();       // 1ステージで獲得したバトル得点
    private const int MaxPlayer = 6;

    // 各プレイヤーの総合得点リスト
    private PointType[] m_saveCostData = new PointType[MaxPlayer];                                          // プレイヤーごとの得点保存

    [SerializeField]
    private float m_ArtMagnification;      // 芸術ポイント倍率
    [SerializeField]
    private float m_battleMagnification;   // バトルポイント倍率
    [SerializeField]
    private int m_criticalPoint;         // クリティカルポイント
    public int CriticalPoint
    {
        get { return m_criticalPoint; }
    }

    private CrushPointManager m_crushPointManager;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        m_crushPointManager = gameObject.GetComponent<CrushPointManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveArtCostData(int playerID,CostParts cost)
    {
        m_playerArtCostData.Add(playerID, cost);
    }
    
    public void SaveBattleCostData(int playerID,int battleCost)
    {
        Debug.Log("バトルポイント保存" + playerID);
        if (m_playerBattleCostData.ContainsKey(playerID))
        {
            m_playerBattleCostData[playerID] = battleCost  * m_battleMagnification;
        }
        else
        {
            m_playerBattleCostData.Add(playerID, battleCost  * m_battleMagnification);
        }
    }

    // コストから得点に変換し取得
    int ConversionCost(float cost,bool artFlg)
    {
        if(artFlg)
            return Mathf.RoundToInt(cost * m_ArtMagnification);
        return Mathf.RoundToInt(cost * m_battleMagnification);
    }

    // プレイヤーの1ステージに獲得したポイントを取得
    public int GetPlayerAllCost(int playerID)
    {
        int cost = 0;

        cost += GetPlayerArtPoint(playerID);
        cost += GetPlayerBattlePoint(playerID);

        return cost;
    }

    // プレイヤーの1ステージに獲得した芸術ポイントを取得
    public int GetPlayerArtPoint(int playerID)
    {
        int cost = 0;
        // 芸術ポイントがコンテナにある場合
        if (m_playerArtCostData.ContainsKey(playerID))
        {
            cost = ConversionCost(m_playerArtCostData[playerID].allCost, true);
        }
        return cost;
    }
    // プレイヤーの1ステージに獲得したバトルポイントを取得
    public int GetPlayerBattlePoint(int playerID)
    {
        int cost = 0;
        // バトルポイントがコンテナにある場合
        if (m_playerBattleCostData.ContainsKey(playerID))
        {
            cost = ConversionCost(m_playerBattleCostData[playerID], false);
        }
        return cost;
    }
    // プレイヤーの1ステージに獲得したクラッシュポイントを獲得
    public int GetPlayerCrushPoint(int playerID)
    {
        return m_crushPointManager.GetCrushPoint(playerID);
    }

    // 次のステージに移るためにコストを保存する
    public void Init()
    {
        for (int id = 1; id <= m_saveCostData.Length; ++id)
        {
            if (m_playerArtCostData.ContainsKey(id))
            {
                m_saveCostData[id-1].art += ConversionCost(m_playerArtCostData[id].allCost, true);
                m_playerArtCostData.Remove(id);
            }
            if (m_playerBattleCostData.ContainsKey(id))
            {
                m_saveCostData[id-1].critical += ConversionCost(m_playerBattleCostData[id], false);
                m_playerBattleCostData.Remove(id);
            }

            m_saveCostData[id-1].crush += m_crushPointManager.GetCrushPoint(id);
        }
        
        m_playerArtCostData.Clear();
        m_playerBattleCostData.Clear();
        m_crushPointManager.Init();
    }
}
