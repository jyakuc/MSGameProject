﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour {
    //サイズ変更適用Transform
    Transform Trans;
    //サイズ減少用変数
    [Range(1, 10)]
    public float Speed = 0;
    //サイズ限界値変数
    [Range(0, 1)]
    public float LimitSize = 0;
    [Range(0,120)]
    public float StartTime = 0;
    [Range(-150,170)]
    public float RandmPosX = 0;
    [Range(-170, 150)]
    public float RandmPosY = 0;
    //debug用の秒数確認用
    public float NowTime = 0;
    [SerializeField]
    private GameController Gb;
    private void Awake()
    {
    }
    // Use this for initialization
    void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Gb.IsGameStart())
        {
            NowTime += Time.deltaTime;

            if (NowTime > StartTime)
            {
                if (Trans.localScale.x <= LimitSize)
                {
                    Trans.localScale = new Vector3(LimitSize, LimitSize, Trans.localScale.z);
                }
                else
                {
                    Trans.localScale = new Vector3(Trans.localScale.x - (Speed / 10000), Trans.localScale.y - (Speed / 10000), Trans.localScale.z);

                }
            }

        }

    }
}
