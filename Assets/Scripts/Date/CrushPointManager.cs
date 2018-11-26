using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPointManager : MonoBehaviour {

    [SerializeField]
    private int m_crushPoint;
    // 誰に撃破されたかのコンテナ
    private Dictionary<int, int> m_playerIscrushedContainer = new Dictionary<int, int>();
    // 撃破ポイントのコンテナ
    private Dictionary<int, int> m_crushPointIDContainer = new Dictionary<int, int>(); 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 死亡時最後に攻撃されたプレイヤーのIDを格納
    public void DamageDead(int deadplayerID,int attackerID)
    {
        if(!m_playerIscrushedContainer.ContainsKey(deadplayerID))
            m_playerIscrushedContainer.Add(deadplayerID, attackerID);

        // ポイント加算
        if (m_crushPointIDContainer.ContainsKey(attackerID))
            m_crushPointIDContainer[attackerID] += m_crushPoint;
        else
            m_crushPointIDContainer.Add(attackerID,m_crushPoint);
    }

    public void Init()
    {
        m_playerIscrushedContainer.Clear();
        m_crushPointIDContainer.Clear();
    }

    public int GetCrushPoint(int id)
    {
        if (m_crushPointIDContainer.ContainsKey(id))
            return m_crushPointIDContainer[id];
        return 0;
    }
}
