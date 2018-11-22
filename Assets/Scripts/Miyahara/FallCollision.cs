using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour {

    //public List <int> Rank = new List<int>();
    public RankingInGame rankingInGame;
    private CrushPointManager CrushPointManager;
    public GameObject FallEffect;

	// Use this for initialization
	void Start () {
		if(rankingInGame == null)
        {
            rankingInGame = FindObjectOfType<RankingInGame>();
            if (rankingInGame == null)
                Debug.LogError("RankingInGameがシーンに存在しません");
        }
        
        if(CrushPointManager == null)
        {
            CrushPointManager = FindObjectOfType<CrushPointManager>();
            if (CrushPointManager == null)
                Debug.LogError("CrushPointManagerがジーンに存在しません");
        }

       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

        CreateEffect(other.gameObject.transform);

        // 落ちたプレイヤーのIDを取得
        PlayerController fallPlayer = other.gameObject.transform.root.GetComponent<PlayerController>();
        int playerID = fallPlayer.PlayerID;
        // UIに順位更新
        rankingInGame.SetRank(playerID);
        // CrushPointManagerを更新
        CrushPointManager.DamageDead(playerID, fallPlayer.HitReceivePlayerID);
        fallPlayer.Dead();
    }

    //Effect生成
    private void CreateEffect(Transform Trans)
    {
        GameObject Effect = (GameObject)Instantiate(FallEffect, Trans.position, Quaternion.identity);
        ParticleSystem Particle = Effect.GetComponent<ParticleSystem>();
        Particle.Play();

    }
}
