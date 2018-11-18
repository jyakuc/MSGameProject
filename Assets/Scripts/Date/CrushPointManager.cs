using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPointManager : MonoBehaviour {

    [SerializeField]
    private int m_crushPoint;
    private Dictionary<int, int> m_crushIDContainer = new Dictionary<int, int>();


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 死亡時最後に攻撃されたプレイヤーのIDを格納
    public void DamageDead(int myplayerID,int attackerID)
    {
        if(!m_crushIDContainer.ContainsKey(myplayerID))
            m_crushIDContainer.Add(myplayerID, attackerID);
    }

    public void Init()
    {
        foreach(KeyValuePair<int, int> pair in m_crushIDContainer)
        {
            m_crushIDContainer.Remove(pair.Key);
        }
    }

    public int GetCrushPoint(int id)
    {
        if (m_crushIDContainer.ContainsKey(id))
            return m_crushIDContainer[id];
        return 0;
    }
}
