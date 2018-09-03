using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour {

    // プレハブを取得
    public GameObject prefab;

	// Use this for initialization
	void Start () {
        
        // プレハブからインスタンスを生成
        Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
