using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour {

    public List <int> Rank = new List<int>();
    public RankingInGame rankingInGame;
    public GameObject FallEffect;
    public CostManager costManager;
	// Use this for initialization
	void Start () {
		if(rankingInGame == null)
        {
            rankingInGame = FindObjectOfType<RankingInGame>();
            if (rankingInGame == null)
                Debug.LogError("RankingInGameがシーン存在しません");
        }

        /*   costManager = GameObject.FindObjectOfType<CostManager>();
           if (costManager == null)
               Debug.LogError("CostManagerがシーンに存在しません。");
       */
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;

        CreateEffect(other.gameObject.transform);

        Transform parent = other.gameObject.transform.root;
        int playerID = parent.GetComponent<PlayerController>().PlayerID;
        
        Rank.Add(playerID);
        parent.GetComponent<PlayerController>().Dead();
      /*  if (parent.GetComponent<ArtGrading>() != null)
        {
            parent.GetComponent<ArtGrading>().Save();
            parent.GetComponent<ArtGrading>().ArtistGrading();
            costManager.AddCostData(playerID, parent.GetComponent<ArtGrading>().Cost);
            Debug.Log(parent.GetComponent<ArtGrading>().Cost);
        }
        */
        rankingInGame.SetRank(playerID);
    }
    //Effect生成
    private void CreateEffect(Transform Trans)
    {
        GameObject Effect = (GameObject)Instantiate(FallEffect, Trans.position, Quaternion.identity);
        ParticleSystem Particle = Effect.GetComponent<ParticleSystem>();
        Particle.Play();

    }
}
