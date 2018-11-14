using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePointGrading : MonoBehaviour {

    private List<int> m_attackPointData = new List<int>();
    private const int MaxPlayer = 6; 
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < MaxPlayer; ++i)
        {
            m_attackPointData.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // クリティカルヒットの攻撃相手保存(ID)
    public void AddCriticalPoint(int playerID)
    {
        m_attackPointData[playerID-1] ++;
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
