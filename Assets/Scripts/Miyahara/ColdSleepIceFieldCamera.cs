using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdSleepIceFieldCamera : MonoBehaviour {

    [SerializeField]
    private float Speed;
    private float Count;
    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject SubCamera;
    private GameObject Cannons;

    private GameObject Cursors;

    private float Rate;
    FadeController FadeObj;
    SceneController SceneObj;
    private bool FadeFlg;

    private StartTimer S_timer;
    private GameObject g_UI;
    private GameObject _slider_Background;
    private GameObject _slider_Fillarea;
    private GameObject _sliderText1;
    private GameObject _sliderText2;


    //カメラ演出用
    private GameObject PerformanceGround;
    private VanishWall VanishGround;
    private GameObject ParentGround1;
    private GameObject ParentGround2;
    private GameObject ParentGround3;
    private VanishWall VanishParent1;
    private VanishWall VanishParent2;
    private VanishWall VanishParent3;
    private bool VanishGroundFlg;
    private GameObject LookTarget;


    // Use this for initialization
    void Start()
    {

        Rate = 1;
        Count = 0;
        SceneObj = GameObject.FindObjectOfType<SceneController>();
        FadeObj = SceneObj.transform.Find("FadeCanvas").GetComponent<FadeController>();
        Cannons = GameObject.Find("ColdSleepCannons(Clone)");
        Cursors = GameObject.Find("Cursors(Clone)");
        FadeFlg = false;
        g_UI = GameObject.Find("GameUI");
        _slider_Background = g_UI.transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).gameObject;
        _slider_Fillarea = g_UI.transform.GetChild(0).transform.GetChild(4).transform.GetChild(1).gameObject;
        _sliderText1 = g_UI.transform.GetChild(0).transform.GetChild(4).transform.GetChild(2).gameObject;
        _sliderText2 = g_UI.transform.GetChild(0).transform.GetChild(4).transform.GetChild(3).gameObject;
        S_timer = GameObject.FindObjectOfType<StartTimer>();
        VanishGroundFlg = false;
        PerformanceGround = GameObject.Find("PerformanceGround");
        ParentGround1 = GameObject.Find("PerfomanceObj01");
        ParentGround2 = GameObject.Find("PerfomanceObj02");
        ParentGround3 = GameObject.Find("PerfomanceObj03");
        VanishGround = PerformanceGround.GetComponent<VanishWall>();
        VanishParent1 = ParentGround1.GetComponent<VanishWall>();
        VanishParent2 = ParentGround2.GetComponent<VanishWall>();
        VanishParent3 = ParentGround3.GetComponent<VanishWall>();
        LookTarget = GameObject.Find("LookTarget");
    }

    // Update is called once per frame
    void Update()
    {
        if (!VanishGroundFlg)
        {
            VanishGround.OnVanish();
            VanishParent1.OnVanish();
            VanishParent2.OnVanish();
            VanishParent3.OnVanish();
            VanishGroundFlg = true;
        }
        if (Count < 110)
        {
            this.transform.position -= new Vector3(Speed* Time.deltaTime, 0.0f, 0.0f);
            //this.transform.Rotate(0, Speed * Time.deltaTime, 0);
            Count += Speed * Time.deltaTime;
        }
        else if (!FadeObj.IsFade && FadeFlg == false) //フェードイン
        {
            FadeObj.gameObject.SetActive(true);
            FadeController.Begin(FadeObj.gameObject, false, Rate);
            FadeFlg = true;
            FadeObj.m_onFinished += ChangeCamera;
        }

        this.transform.LookAt(LookTarget.transform);

    }

    public void ChangeCamera()
    {
        MainCamera.SetActive(!MainCamera.activeSelf);
        SubCamera.SetActive(!SubCamera.activeSelf);
        this.gameObject.SetActive(false);
        Cannons.transform.Find("Cannon6Arc").gameObject.SetActive(true);
        Cannons.transform.Find("CannonMng").gameObject.SetActive(true);
        OnCursors();
        _slider_Background.gameObject.SetActive(true);
        _slider_Fillarea.gameObject.SetActive(true);
        _sliderText1.gameObject.SetActive(true);
        _sliderText2.gameObject.SetActive(true);
        FadeController.Begin(FadeObj.gameObject, true, Rate);
        if (S_timer == null)
        {
            S_timer = GameObject.FindObjectOfType<StartTimer>();
            S_timer.On_TimeStartFlg();
        }
        else
        {
            S_timer.On_TimeStartFlg();
        }
        FadeObj.m_onFinished -= ChangeCamera;
    }

    void OnCursors()
    {
        Cursors.transform.Find("Cursor").gameObject.SetActive(true);
        Cursors.transform.Find("Cursor (1)").gameObject.SetActive(true);
        Cursors.transform.Find("Cursor (2)").gameObject.SetActive(true);
        Cursors.transform.Find("Cursor (3)").gameObject.SetActive(true);
        Cursors.transform.Find("Cursor (4)").gameObject.SetActive(true);
        Cursors.transform.Find("Cursor (5)").gameObject.SetActive(true);
    }

}
