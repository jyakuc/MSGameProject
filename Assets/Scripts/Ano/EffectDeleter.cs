using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeleter : MonoBehaviour {
    private ParticleSystem MyParticle;
	// Use this for initialization
	void Start () {
        MyParticle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!MyParticle.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
}
