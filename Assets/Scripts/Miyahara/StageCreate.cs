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

    public GameObject gameControllerObj;

    // 生成したステージを格納　破棄に使う
    public List<GameObject> createStageObjects = new List<GameObject>();

    private int min = -2;
    private int max = 2;

    [SerializeField]
    private float x, y, z;

    public enum SelectingStage
    {
        Colloseum,
        ColdSleepMountain,
        HoruhoruMountain,
    }

    public SelectingStage Stages;

	// Use this for initialization
	void Start () {

        //gameController =

        DebugModeGame debugModeGame = gameControllerObj.GetComponent<DebugModeGame>();
        GameController gameController = gameControllerObj.GetComponent<GameController>();

        if (!(debugModeGame.m_debugMode.m_debugMode && debugModeGame.m_debugMode.m_debugstageCreate))
        {
            Stages = SelectingStage.Colloseum;
        }
        
        Load();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // ステージ読み込み
    public void Load()
    {
        createStageObjects.Clear();
        switch (Stages)
        {
            case SelectingStage.Colloseum:
                // プレハブからインスタンスを生成
                createStageObjects.Add(Instantiate(ColosseumCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                    //カメラ生成
                createStageObjects.Add(Instantiate(NormalStage, new Vector3(x, y, z), Quaternion.identity));                                 //ステージ生成
                createStageObjects.Add(Instantiate(Colosseum, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                          //外枠生成
                // プレハブからコリジョンを生成
                createStageObjects.Add(Instantiate(Collision, new Vector3(0.0f, 0.0f, 0.0f), Collision.GetComponent<Transform>().rotation)); //迫ってくるコリジョン生成
                createStageObjects.Add(Instantiate(ColloseumCannons, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                   //キャノン生成
                createStageObjects.Add(Instantiate(Cursol, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                             //カーソル生成
                Stages = SelectingStage.Colloseum;
                break;
            case SelectingStage.HoruhoruMountain:
                //プレハブからインスタンスを生成
                createStageObjects.Add(Instantiate(HoruhoruCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                     //カメラ生成
                createStageObjects.Add(Instantiate(HoruHoruMountain, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                   //ステージ生成
                createStageObjects.Add(Instantiate(HoruHoruCannons, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                    //キャノン生成
                createStageObjects.Add(Instantiate(Cursol, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                             //カーソル生成
                Stages = SelectingStage.HoruhoruMountain;
                break;
            case SelectingStage.ColdSleepMountain:
                //プレハブからインスタンスを生成
                createStageObjects.Add(Instantiate(ColdSleepCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                    //カメラ生成
                createStageObjects.Add(Instantiate(ColdSleepMountain, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                  //ステージ生成
                createStageObjects.Add(Instantiate(ColdSleepCannons, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                      //キャノン生成
                createStageObjects.Add(Instantiate(Cursol, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                             //カーソル生成
                Stages = SelectingStage.ColdSleepMountain;
                break;
        }
    }

    // ステージ破棄
    public void Unload()
    {
        for(int i = 0; i < createStageObjects.Count; ++i)
        {
            Debug.Log("消去：" + createStageObjects[i]);
            Destroy(createStageObjects[i].gameObject);
        }
        Stages++;
        //createStageObjects.Clear();
    }

    // ステージがすべて破棄されたか確認
    public bool IsDestroy()
    {
        for (int i = 0; i < createStageObjects.Count; ++i)
        {
            if (createStageObjects[i] != null) return false;
        }
        return true;
    }
    
}
