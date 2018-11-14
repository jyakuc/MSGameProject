using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour {
    private Dictionary<int, CostParts> m_playerArtCostData = new Dictionary<int, CostParts>();  // 1ステージで獲得した芸術得点
    private Dictionary<int, float> m_playerBattleCostData = new Dictionary<int, float>();       // 1ステージで獲得したバトル得点
    private const int MaxPlayer = 6;
    private int[] m_saveCostData = new int[MaxPlayer];                                          // プレイヤーごとの得点保存
    [SerializeField]
    private float m_ArtMagnification;      // 芸術ポイント倍率
    [SerializeField]
    private float m_battleMagnification;   // バトルポイント倍率
    [SerializeField]
    private float m_criticalPoint;         // クリティカルポイント
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {

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
        m_playerBattleCostData.Add(playerID, battleCost * m_criticalPoint);
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
        // 芸術ポイントがコンテナにある場合
        if (m_playerArtCostData.ContainsKey(playerID))
        {
            cost += ConversionCost(m_playerArtCostData[playerID].allCost,true);
        }

        // バトルポイントがコンテナにある場合
        if (m_playerBattleCostData.ContainsKey(playerID))
        {
            cost += ConversionCost(m_playerBattleCostData[playerID], false);
        }

        return cost;
    }

    // 次のステージに移るためにコストを保存する
    public void SaveCost()
    {
        for (int i = 0; i < m_saveCostData.Length; ++i)
        {
            if (m_playerArtCostData.ContainsKey(i))
            {
                m_saveCostData[i] += ConversionCost(m_playerArtCostData[i].allCost, true);
            }
            if (m_playerBattleCostData.ContainsKey(i))
            {
                m_saveCostData[i] += ConversionCost(m_playerBattleCostData[i], false);
            }
        }
    }
}
