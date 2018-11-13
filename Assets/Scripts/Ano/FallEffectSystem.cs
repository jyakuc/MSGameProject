using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEffectSystem : MonoBehaviour {
    //色変更用
    ParticleSystem MyParticle;
    public Color ColorData;
    // Use this for initialization
    void Awake()
    {
        MyParticle = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule CircleParticle = GetComponent<ParticleSystem>().main;
        CircleParticle.startColor = ColorData;
    }
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
