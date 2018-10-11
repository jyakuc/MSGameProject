using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour {
    //サイズ変更適用Transform
    private Transform CircleTrans;
    //色変更用
    public Color ColorData;
    //サイズ用変数
    [Range(0, 10)]
    public float CircleSize = 0;
    //サイズ減少用変数
    [Range(0, 10)]
    public float DecreaseSpeed = 0;
    //サイズ限界値変数
    [Range(0, 10)]
    public float LimitSize = 0;
    private void Awake()
    {
        ParticleSystem.MainModule CircleParticle = GetComponent<ParticleSystem>().main;
        CircleTrans = GetComponent<Transform>();
        CircleParticle.startColor = ColorData;
        CircleTrans.localScale = new Vector3(CircleSize, CircleSize, CircleSize);
        
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
        CircleSize -= DecreaseSpeed;
        if(CircleSize<= LimitSize)
        {
            CircleSize = LimitSize;
        }
        CircleTrans.localScale = new Vector3(CircleSize, CircleSize, 1);

    }

    

}
