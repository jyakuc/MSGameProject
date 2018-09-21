using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour {

    // プレハブを取得
    public GameObject prefab;       //生成するステージ
    public GameObject Collision;     //生成するコリジョン
    private int Random;
    private int min = 0;
    private int max = 10;
	// Use this for initialization
	void Start () {
        Random = UnityEngine.Random.Range(min,max);

        // プレハブからコリジョンを生成
        Instantiate(Collision, new Vector3(Random, 0.0f, Random), Quaternion.identity);

        // プレハブからインスタンスを生成
        Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
