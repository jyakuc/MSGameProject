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

    private float Rate;
    FadeController FadeObj;
    SceneController SceneObj;
    private bool FadeFlg;

    private StartTimer S_timer;
    private GameObject g_UI;
    private GameObject _slider_Background;
    private GameObject _slider_Fillarea;
    // Use this for initialization
    void Start()
    {

        Rate = 1;
        Count = 0;
        SceneObj = GameObject.FindObjectOfType<SceneController>();
        FadeObj = SceneObj.transform.Find("FadeCanvas").GetComponent<FadeController>();
        Cannons = GameObject.Find("ColdSleepCannons(Clone)");
        FadeFlg = false;
        g_UI = GameObject.Find("GameUI");
        _slider_Background = g_UI.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject;
        _slider_Fillarea = g_UI.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject;
        S_timer = GameObject.FindObjectOfType<StartTimer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Count < 270)
        {
            this.transform.Rotate(0, Speed * Time.deltaTime, 0);
            Count += Speed * Time.deltaTime;
        }
        else if (!FadeObj.IsFade && FadeFlg == false) //フェードイン
        {
            FadeObj.gameObject.SetActive(true);
            FadeController.Begin(FadeObj.gameObject, false, Rate);
            FadeFlg = true;
            FadeObj.m_onFinished += ChangeCamera;
        }
    }

    public void ChangeCamera()
    {
        MainCamera.SetActive(!MainCamera.activeSelf);
        SubCamera.SetActive(!SubCamera.activeSelf);
        Cannons.transform.Find("Cannon6Arc").gameObject.SetActive(true);
        Cannons.transform.Find("CannonMng").gameObject.SetActive(true);
        _slider_Background.gameObject.SetActive(true);
        _slider_Fillarea.gameObject.SetActive(true);
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

}
