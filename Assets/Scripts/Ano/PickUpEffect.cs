using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour {
    //エフェクト
    private ParticleSystem MyParticle;
    void Awake()
    {
        MyParticle = GetComponent<ParticleSystem>();
    }
        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!MyParticle.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
