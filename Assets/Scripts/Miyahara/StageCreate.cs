using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour {

    // プレハブを取得
    public GameObject NormalStage;       //生成するステージ
    public GameObject HoruHoruMountain;  //ホルホル山
    public GameObject Colosseum;         //コロシアム生成
    public GameObject Collision;     //生成するコリジョン
    private int min = -2;
    private int max = 2;

    [SerializeField]
    private float x, y, z;

    public enum SelectingStage
    {
        NormalStage,
        HoruhoruMountain
    }

    public SelectingStage Stages;

	// Use this for initialization
	void Start () {


        switch (Stages)
        {
            case SelectingStage.NormalStage:
                // プレハブからインスタンスを生成
                Instantiate(NormalStage, new Vector3(x, y, z), Quaternion.identity);
                Instantiate(Colosseum, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                // プレハブからコリジョンを生成
                Instantiate(Collision, new Vector3(0.0f, 0.0f, 0.0f), Collision.GetComponent<Transform>().rotation);
                break;
            case SelectingStage.HoruhoruMountain:
                //プレハブからインスタンスを生成
                Instantiate(HoruHoruMountain, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                break;
        }
        

        

        
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
