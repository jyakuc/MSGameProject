﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour {
    private Dictionary<int, CostParts> m_playerCostData = new Dictionary<int, CostParts>();
    [SerializeField]
    private float m_magnification;      // 倍率
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCostData(int playerID,CostParts cost)
    {
        m_playerCostData.Add(playerID, cost);
    }

    // コストから得点に変換し取得
    int ConversionCost(float cost)
    {
        return Mathf.RoundToInt(cost * m_magnification);
    }
}
