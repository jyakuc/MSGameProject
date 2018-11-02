using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEffectSystem : MonoBehaviour {
    //色変更用
    public Color ColorData;
    // Use this for initialization
    void Start () {
        ParticleSystem.MainModule CircleParticle = GetComponent<ParticleSystem>().main;
        CircleParticle.startColor = ColorData;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
