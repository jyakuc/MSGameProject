using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour {
    public struct PointType
    {
        public int art;
        public int critical;
        public int crush;
        public int rank;
    }

    private Dictionary<int, CostParts> m_playerArtCostData = new Dictionary<int, CostParts>();  // 1ステージで獲得した芸術得点
    private Dictionary<int, float> m_playerBattleCostData = new Dictionary<int, float>();       // 1ステージで獲得したバトル得点
    private Dictionary<int, int> m_playerRankCostData = new Dictionary<int, int>();         // 1ステージで獲得した順位ポイント
    private const int MaxPlayer = 6;

    // 各プレイヤーの総合得点リスト
    private PointType[] m_saveCostData = new PointType[MaxPlayer];                              // 1ゲームのプレイヤーごとの得点保存

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

    public int[] m_rankPoint = new int[MaxPlayer];  // 順位ポイント
    
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
        if (m_playerBattleCostData.ContainsKey(playerID))
        {
            m_playerBattleCostData[playerID] = battleCost  * m_battleMagnification;
        }
        else
        {
            m_playerBattleCostData.Add(playerID, battleCost  * m_battleMagnification);
        }
    }

    public void SaveRankPointData(int playerID,int rank)
    {
        if (m_playerRankCostData.ContainsKey(playerID))
        {
            m_playerRankCostData[playerID] = m_rankPoint[rank];
        }
        else
        {
            m_playerRankCostData.Add(playerID, m_rankPoint[rank]);
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

        /*cost += GetPlayerArtPoint(playerID);
        cost += GetPlayerBattlePoint(playerID);

        */

        cost += m_saveCostData[playerID - 1].art;
        cost += m_saveCostData[playerID - 1].critical;
        cost += m_saveCostData[playerID - 1].crush;
        cost += m_saveCostData[playerID - 1].rank;

        return cost;
    }

    public PointType GetPlayerCost(int playerID)
    {
        return m_saveCostData[playerID - 1];
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
    // プレイヤーの1ステージに獲得したクラッシュポイントを取得
    public int GetPlayerCrushPoint(int playerID)
    {
        return m_crushPointManager.GetCrushPoint(playerID);
    }
    // プレイヤーの1ステージに獲得した順位ポイントを取得


    // 次のステージに移るためにコストを保存する
    public void Init()
    {
        for (int id = 1; id <= m_saveCostData.Length; ++id)
        {
            if (m_playerArtCostData.ContainsKey(id))
            {
                m_saveCostData[id-1].art += ConversionCost(m_playerArtCostData[id].allCost, true);
                Debug.Log(id.ToString() + "P:芸術ポイント" + m_saveCostData[id - 1].art);
            }
            if (m_playerBattleCostData.ContainsKey(id))
            {
                m_saveCostData[id-1].critical += ConversionCost(m_playerBattleCostData[id], false);
                Debug.Log(id.ToString() + "P:クリティカルポイント" + m_saveCostData[id - 1].critical);
            }
            if (m_playerRankCostData.ContainsKey(id))
            {
                m_saveCostData[id - 1].rank += m_playerRankCostData[id];
                Debug.Log(id.ToString() + "P:順位ポイント" + m_saveCostData[id - 1].rank);
            }
            Debug.Log(id.ToString()+"P:撃破ポイント " + m_crushPointManager.GetCrushPoint(id));
            m_saveCostData[id-1].crush += m_crushPointManager.GetCrushPoint(id);

            Debug.Log(id.ToString() + "P:総合ポイント" + GetPlayerAllCost(id));
        }
        
        m_playerArtCostData.Clear();
        m_playerBattleCostData.Clear();
        m_playerRankCostData.Clear();
        m_crushPointManager.Init();
    }

    public void Clear()
    {
        for(int i = 0; i < m_saveCostData.Length; ++i)
        {
            m_saveCostData[i].art = 0;
            m_saveCostData[i].critical = 0;
            m_saveCostData[i].crush = 0;
            m_saveCostData[i].rank = 0;
        }
    }

}
