using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCreate : MonoBehaviour {

    public GameObject LoadColloseumCamera;      //ロードシーンのコロシアムカメラ
    public GameObject LoadColloseum;            //コロシアムのロード

    public GameObject LoadHoruHoruCamera;       //ロードシーンのホルホルマウンテンカメラ
    public GameObject LoadHoruHoru;             //ホルホルマウンテンのロード

    public GameObject LoadColdSleepCamera;      //ロードシーンのコールドスリープアイスフィールドのカメラ
    public GameObject LoadColdSleep;            //コールドスリープアイスフィールドのロード

    public enum SelectingLoad
    {
        Colloseum,
        ColdSleepMountain,
        HoruhoruMountain,
    }

    public SelectingLoad Stages;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load()
    {
        switch (Stages)
        {
            case SelectingLoad.Colloseum:
                Instantiate(LoadColloseumCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);           //カメラ生成
                Instantiate(LoadColloseum, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                 //ステージ生成
                Stages = SelectingLoad.Colloseum;
                break;
            case SelectingLoad.HoruhoruMountain:
                Instantiate(LoadHoruHoruCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);           //カメラ生成
                Instantiate(LoadHoruHoru, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                 //ステージ生成
                Stages = SelectingLoad.HoruhoruMountain;
                break;
            case SelectingLoad.ColdSleepMountain:
                Instantiate(LoadColdSleepCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);           //カメラ生成
                Instantiate(LoadColdSleep, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);                 //ステージ生成
                Stages = SelectingLoad.ColdSleepMountain;
                break;
        }
    }

}
