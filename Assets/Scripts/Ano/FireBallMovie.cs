using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovie : MonoBehaviour {
    //エフェクト
    private ParticleSystem MyParticle;
    [Range(0, 60)]
    public float EndTime = 10;
	// Use this for initialization
    void Awake()
    {
        MyParticle = GetComponent<ParticleSystem>();

    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
        EndTime-=Time.deltaTime;
		if(EndTime<=0)
        {
            Destroy(this.gameObject);
        }
	}
}
