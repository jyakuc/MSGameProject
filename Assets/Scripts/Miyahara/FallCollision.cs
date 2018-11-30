using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour {

    //public List <int> Rank = new List<int>()
    public GameObject FallEffect;
    private DestroyCollisionPlayers destroyer;
	// Use this for initialization
	void Start () {
		
        if(destroyer == null)
        {
            destroyer = FindObjectOfType<DestroyCollisionPlayers>();
            if (destroyer == null)
                Debug.LogError("DestroyCollisionPlayersがシーンに存在しません");
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if (gameController.GetLivePlayerNum() == 1) return;
        if (other.gameObject.tag != "Player")
            return;

        CreateEffect(other.gameObject.transform);
        destroyer.AddFallPlayer(other.GetComponent<PlayerController>());
    }

    //Effect生成
    private void CreateEffect(Transform Trans)
    {
        GameObject Effect = (GameObject)Instantiate(FallEffect, Trans.position, Quaternion.identity);
        ParticleSystem Particle = Effect.GetComponent<ParticleSystem>();
        Particle.Play();

    }
}
