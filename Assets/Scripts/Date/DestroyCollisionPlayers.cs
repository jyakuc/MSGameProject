using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollisionPlayers : MonoBehaviour {
    private List<GameObject> m_fallPlayerList = new List<GameObject>();
    private RankingInGame m_rankingInGame;
    private CrushPointManager m_crushPointManager;
    private int m_restrictionNum;
    private void Start()
    {
        if (m_rankingInGame == null) m_rankingInGame = FindObjectOfType<RankingInGame>();
        if (m_crushPointManager == null) m_crushPointManager = FindObjectOfType<CrushPointManager>();
        m_restrictionNum = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum-1 : 6-1;

    }

    // FallCollision時プレイヤー格納
    public void AddFallPlayer(PlayerController fallplayer)
    {
        
        if (m_fallPlayerList.Count >= m_restrictionNum) return;
        // UIに順位更新
        // m_rankingInGame.SetRank(fallplayer.PlayerID);
        // CrushPointManagerを更新
        m_crushPointManager.DamageDead(fallplayer.PlayerID, fallplayer.HitReceivePlayerID);
        // Active = false
        fallplayer.Dead();
        m_fallPlayerList.Add(fallplayer.gameObject);
    }

    // リスト内のプレイヤーを破棄
    public void DestroyFallPlayer()
    {
        foreach(var player in m_fallPlayerList)
        {
            Destroy(player);
        }
        m_fallPlayerList.Clear();
    }
    
}
