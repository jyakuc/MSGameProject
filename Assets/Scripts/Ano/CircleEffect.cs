using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour {
    //サイズ変更適用Transform
    private Transform CircleTrans;
    public Transform FilterTrans;
    //色変更用
    public Color ColorData;

    private void Awake()
    {
        ParticleSystem.MainModule CircleParticle = GetComponent<ParticleSystem>().main;
        CircleTrans = GetComponent<Transform>();
        CircleParticle.startColor = ColorData;        
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        CircleTrans.localScale = FilterTrans.localScale;
    }

    

}
