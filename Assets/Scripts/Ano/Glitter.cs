using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitter : MonoBehaviour {
    private ParticleSystem ParticleData;
    // Use this for initialization
    void Start () {
        ParticleData = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!ParticleData.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
