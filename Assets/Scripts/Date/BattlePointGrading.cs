using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePointGrading : MonoBehaviour {

    private Dictionary<int, int> m_attackPointData = new Dictionary<int, int>();
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // クリティカルヒットの攻撃相手保存(ID)
    public void AddCriticalPoint(int playerID)
    {
        if (m_attackPointData.ContainsKey(playerID))
        {
            m_attackPointData[playerID] ++;
        }
        m_attackPointData.Add(playerID, 1);
    }

    // バトル得点の合計を取得
    public int GetAllPoint()
    {
        int cost = 0;
        for (int i = 0; i < m_attackPointData.Count; ++i)
        {
            cost += m_attackPointData[i];
        }
        return cost;
    }
}
