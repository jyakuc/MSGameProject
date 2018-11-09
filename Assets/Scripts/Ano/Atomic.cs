using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atomic : MonoBehaviour {
    ParticleSystem MyParticle;
    // Use this for initialization
    void Awake () {
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
