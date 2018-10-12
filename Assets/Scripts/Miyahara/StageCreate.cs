using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour {

    // プレハブを取得
    public GameObject prefab;       //生成するステージ
    public GameObject Collision;     //生成するコリジョン
    private int RandomX;
    private int RandomZ;
    private int min = -2;
    private int max = 2;

    [SerializeField]
    private float x, y, z;
	// Use this for initialization
	void Start () {
        RandomX = UnityEngine.Random.Range(min,max);
        RandomZ = UnityEngine.Random.Range(min, max);
        // プレハブからコリジョンを生成
        Instantiate(Collision, new Vector3(x,y,z), Quaternion.identity);

        // プレハブからインスタンスを生成
        Instantiate(prefab, new Vector3(-10.0f, 0.0f, -10.0f), Quaternion.identity);
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
