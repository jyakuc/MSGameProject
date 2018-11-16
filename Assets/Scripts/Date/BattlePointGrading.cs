using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePointGrading : MonoBehaviour {

    private List<int> m_attackPointData = new List<int>();
    private const int MaxPlayer = 6;

    private CostManager m_costManager;
    private PlayerController m_playerController;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < MaxPlayer; ++i)
        {
            m_attackPointData.Add(0);
        }
        m_costManager = FindObjectOfType<CostManager>();

        m_playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // クリティカルヒットの攻撃相手保存(ID)
    public void AddCriticalPoint(int hitplayerID,int myPlayerID)
    {
        Debug.Log(myPlayerID + ":攻撃者 " + hitplayerID + ":被害者");
        m_attackPointData[hitplayerID-1] +=m_costManager.CriticalPoint;
        m_costManager.SaveBattleCostData(myPlayerID, GetAllPoint());
    }

    // バトル得点の合計を取得
    public int GetAllPoint()
    {
        int cost = 0;

        for (int i = 0; i < m_attackPointData.Count; ++i)
        {
             cost += m_attackPointData[i];
        }
        Debug.Log("ばとP"+cost);
        return cost;
    }
}
