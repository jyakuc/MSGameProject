using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour {

    // プレハブを取得
    public GameObject NormalStage;       //生成するステージ
    public GameObject Colosseum;         //コロシアム生成
    public GameObject Collision;         //生成するコリジョン
    public GameObject ColosseumCamera;   //生成するカメラ(コロシアム)
    public GameObject ColloseumCannons;  //生成するキャノン(コロシアム)

    public GameObject HoruHoruMountain;  //ホルホル山
    public GameObject HoruhoruCamera;    //生成するカメラ(ホルホルマウンテン)
    public GameObject HoruHoruCannons;   //生成するキャノン(ホルホルマウンテン)

    public GameObject ColdSleepCamera;   //雪山カメラ
    public GameObject ColdSleepMountain; //雪山
    public GameObject ColdSleepCannons;  //生成するキャノン(雪山)

    public GameObject Cursol;            //生成するカーソル

    private int min = -2;
    private int max = 2;

    [SerializeField]
    private float x, y, z;

    public enum SelectingStage
    {
        Colloseum,
        HoruhoruMountain,
        ColdSleepMountain
    }

    public SelectingStage Stages;

	// Use this for initialization
	void Start () {


        switch (Stages)
        {
            case SelectingStage.Colloseum:
                // プレハブからインスタンスを生成
                Instantiate(ColosseumCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                    //カメラ生成
                Instantiate(NormalStage, new Vector3(x, y, z), Quaternion.identity);                                 //ステージ生成
                Instantiate(Colosseum, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                          //外枠生成
                // プレハブからコリジョンを生成
                Instantiate(Collision, new Vector3(0.0f, 0.0f, 0.0f), Collision.GetComponent<Transform>().rotation); //迫ってくるコリジョン生成
                Instantiate(ColloseumCannons, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                   //キャノン生成
                Instantiate(Cursol, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                             //カーソル生成
                break;
            case SelectingStage.HoruhoruMountain:
                //プレハブからインスタンスを生成
                Instantiate(HoruhoruCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                     //カメラ生成
                Instantiate(HoruHoruMountain, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                   //ステージ生成
                Instantiate(HoruHoruCannons, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                    //キャノン生成
                Instantiate(Cursol, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                             //カーソル生成
                break;
            case SelectingStage.ColdSleepMountain:
                //プレハブからインスタンスを生成
                Instantiate(ColdSleepCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                    //カメラ生成
                Instantiate(ColdSleepMountain, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                  //ステージ生成
                Instantiate(ColdSleepCannons, new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);                      //キャノン生成
                Instantiate(Cursol, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                             //カーソル生成
                break;
        }
        

        

        
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
