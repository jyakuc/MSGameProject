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

    // 生成したステージを格納　破棄に使う
    public List<GameObject> createLoadObjects = new List<GameObject>();

    public enum SelectingLoad
    {
        Colloseum,
        ColdSleepMountain,
        HoruhoruMountain,
    }

    public SelectingLoad Stages;

	// Use this for initialization
	void Start () {
        Loadinfo();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void Loadinfo()
    {
        createLoadObjects.Clear();
        switch (Stages)
        {
            case SelectingLoad.Colloseum:
                createLoadObjects.Add(Instantiate(LoadColloseumCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));           //カメラ生成
                createLoadObjects.Add(Instantiate(LoadColloseum, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                 //ステージ生成
                Stages = SelectingLoad.Colloseum;
                break;
            case SelectingLoad.HoruhoruMountain:
                createLoadObjects.Add(Instantiate(LoadHoruHoruCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));           //カメラ生成
                createLoadObjects.Add(Instantiate(LoadHoruHoru, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                 //ステージ生成
                Stages = SelectingLoad.HoruhoruMountain;
                break;
            case SelectingLoad.ColdSleepMountain:
                createLoadObjects.Add(Instantiate(LoadColdSleepCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));           //カメラ生成
                createLoadObjects.Add(Instantiate(LoadColdSleep, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                 //ステージ生成
                Stages = SelectingLoad.ColdSleepMountain;
                break;
        }
    }

    // ステージ破棄
    public void Unload()
    {
        for (int i = 0; i < createLoadObjects.Count; ++i)
        {
            Debug.Log("消去：" + createLoadObjects[i]);
            Destroy(createLoadObjects[i].gameObject);
        }
        Stages++;
        //createStageObjects.Clear();
    }

    // ステージがすべて破棄されたか確認
    public bool IsLoadDestroy()
    {
        for (int i = 0; i < createLoadObjects.Count; ++i)
        {
            if (createLoadObjects[i] != null) return false;
        }
        return true;
    }

}
