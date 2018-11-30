using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCreate : MonoBehaviour {

    public GameObject LoadColloseumCamera;      //ロードシーンのコロシアムカメラ
    public GameObject LoadColloseum;            //コロシアムのロード

    public GameObject LoadHoruHoruCamera;       //ロードシーンのホルホルマウンテンカメラ
    public GameObject LoadHoruHoru;             //ホルホルマウンテンのロード

    public GameObject LoadColdSleepCamera;      //ロードシーンのコールドスリープアイスフィールドのカメラ
    public GameObject LoadColdSleep;            //コールドスリープアイスフィールドのロード

    // 生成したステージを格納　破棄に使う
    public List<GameObject> createLoadObjects = new List<GameObject>();
    [SerializeField]
    private GameObject LoadUI;
    [SerializeField]
    private GameObject Game_UI;
    private GameObject CollseumText;
    private GameObject ColdSleepText;
    private GameObject HoruHoruText;

    private GameObject ReadyImage;
    private GameObject StartImage;
    private bool animOnce = false;

    public enum SelectingLoad
    {
        Colloseum,
        ColdSleepMountain,
        HoruhoruMountain,
    }

    public SelectingLoad Stages;

	// Use this for initialization
	void Start () {
        CollseumText = LoadUI.transform.GetChild(0).transform.GetChild(1).gameObject;
        ColdSleepText = LoadUI.transform.GetChild(0).transform.GetChild(2).gameObject;
        HoruHoruText = LoadUI.transform.GetChild(0).transform.GetChild(3).gameObject;
        ReadyImage = LoadUI.transform.GetChild(0).transform.GetChild(5).gameObject;
        StartImage = LoadUI.transform.GetChild(0).transform.GetChild(6).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Loadinfo()
    {
        animOnce = false;
        ReadyImage.SetActive(false);

        createLoadObjects.Clear();
        SelectText(Stages);         //テキストを選択
        switch (Stages)
        {
            case SelectingLoad.Colloseum:
                LoadUI.SetActive(true);
                Game_UI.SetActive(false);
                createLoadObjects.Add(Instantiate(LoadColloseumCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));           //カメラ生成
                createLoadObjects.Add(Instantiate(LoadColloseum, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                 //ステージ生成
                Stages = SelectingLoad.Colloseum;
                break;
            case SelectingLoad.HoruhoruMountain:
                LoadUI.SetActive(true);
                Game_UI.SetActive(false);
                createLoadObjects.Add(Instantiate(LoadHoruHoruCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));           //カメラ生成
                createLoadObjects.Add(Instantiate(LoadHoruHoru, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                 //ステージ生成
                Stages = SelectingLoad.HoruhoruMountain;
                break;
            case SelectingLoad.ColdSleepMountain:
                LoadUI.SetActive(true);
                Game_UI.SetActive(false);
                createLoadObjects.Add(Instantiate(LoadColdSleepCamera, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));           //カメラ生成
                createLoadObjects.Add(Instantiate(LoadColdSleep, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));                 //ステージ生成
                Stages = SelectingLoad.ColdSleepMountain;
                break;
        }
        Canvas canvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        canvas.worldCamera = createLoadObjects[0].GetComponentInChildren<Camera>();
        
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

    void SelectText(SelectingLoad nowLoad)
    {
        switch (nowLoad)
        {
            case SelectingLoad.Colloseum:
                CollseumText.SetActive(true);
                ColdSleepText.SetActive(false);
                HoruHoruText.SetActive(false);
                break;
            case SelectingLoad.HoruhoruMountain:
                HoruHoruText.SetActive(true);
                CollseumText.SetActive(false);
                ColdSleepText.SetActive(false);
                break;
            case SelectingLoad.ColdSleepMountain:
                ColdSleepText.SetActive(true);
                HoruHoruText.SetActive(false);
                CollseumText.SetActive(false);
                break;
        }
    }


    public bool Wait(ref bool DownFlg)
    {
        bool animEnd = false;
        bool nextStage = false;

        if (!animOnce && DownFlg)
        {
            ReadyImage.SetActive(true);
            ReadyImage.GetComponent<Animation>().Play();
            animOnce = true;
            DownFlg = false;
            StartImage.GetComponent<Image>().color = new Color(255.0f, 255.0F, 255.0f, 0);
            StartImage.GetComponent<RectTransform>().localPosition = new Vector3(350, -70, 0);
            StartImage.GetComponent<Image>().color = new Color(255.0f, 255.0F, 255.0f, 255/0f);
        }

        if (!ReadyImage.GetComponent<Animation>().IsPlaying("ReadyAnim") && animOnce)
            animEnd = true;

        if (animEnd && DownFlg)
            nextStage = true;

        return nextStage;
    } 
}
