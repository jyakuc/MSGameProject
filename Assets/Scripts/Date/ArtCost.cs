using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtCost : MonoBehaviour {
    [SerializeField]
    private float m_magnification;      // 倍率

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


    }

    // コストから得点に変換し取得
    public int ConversionCost(float cost)
    {
        return Mathf.RoundToInt(cost * m_magnification);
    }
}
