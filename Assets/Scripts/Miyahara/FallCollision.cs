using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour {

    public List <int> Rank = new List<int>();
    public RankingInGame rankingInGame;
    public GameObject FallEffect;
	// Use this for initialization
	void Start () {
		if(rankingInGame == null)
        {
            rankingInGame = FindObjectOfType<RankingInGame>();
            if (rankingInGame == null)
                Debug.LogError("RankingInGameがシーン存在しません");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;
        Debug.Log(other.gameObject.name);
        CreateEffect(other.gameObject.transform);
        Rank.Add(other.gameObject.transform.root.GetComponent<PlayerController>().PlayerID);
        other.gameObject.transform.root.GetComponent<PlayerController>().Dead();
        Destroy(other.gameObject.transform.root.gameObject);
        rankingInGame.SetRank(other.gameObject.transform.root.GetComponent<PlayerController>().PlayerID);
    }
    //Effect生成
    private void CreateEffect(Transform Trans)
    {
        GameObject Effect = (GameObject)Instantiate(FallEffect, Trans.position, Quaternion.identity);
        ParticleSystem Particle = Effect.GetComponent<ParticleSystem>();
        Particle.Play();

    }
}
