using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractionCollision : MonoBehaviour {

    private float y;        //収縮オブジェクトのY取得
    public float x, z;      //収縮オブジェクトのXZ取得
    private int MinObj;     //小さくなるときの最小値
    public float DecreaseX; //収縮の速さＸ
    public float DecreaseZ; //収縮の速さＺ
    [SerializeField]
    private GameController Gb;

	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        y = 4.0f;
        MinObj = 4;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Gb.StartFlg)
        {
            x += DecreaseX;
            //y += -0.01f;
            z += DecreaseZ;

            if (x >= MinObj && y >= MinObj && z >= MinObj)
            {
                this.transform.localScale = new Vector3(x, 4, z);
            }
        }
	}
}
