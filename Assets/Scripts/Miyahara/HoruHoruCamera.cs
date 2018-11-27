using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoruHoruCamera : MonoBehaviour {

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


    [SerializeField]
    private GameObject fireball;
    private GameObject clone;
    private bool Fireballflg;


    //振動
    public float setShakeTime; // 持続振動時間

    private float lifeTime;
    private Vector3 savePosition;
    private float lowRangeX;
    private float maxRangeX;
    private float lowRangeY;
    private float maxRangeY;

    // Use this for initialization
    void Start()
    {
        
        Rate = 1;
        Count = 0;
        SceneObj = GameObject.FindObjectOfType<SceneController>();
        FadeObj = SceneObj.transform.Find("FadeCanvas").GetComponent<FadeController>();
        Cannons = GameObject.Find("HoruHoruCannons(Clone)");
        FadeFlg = false;
        g_UI = GameObject.Find("GameUI");
        _slider_Background = g_UI.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject;
        _slider_Fillarea = g_UI.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject;
        S_timer = GameObject.FindObjectOfType<StartTimer>();
        
        Fireballflg = false;

        //振動

        if (setShakeTime <= 0.0f)
            setShakeTime = 0.7f;
        lifeTime = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        

        if(!Fireballflg){
            clone = Instantiate(fireball, new Vector3(0.0f, 100.0f, 0.0f), Quaternion.identity);
            Instantiate(fireball, new Vector3(15.0f, 120.0f, -15.0f), Quaternion.identity);
            Instantiate(fireball, new Vector3(-15.0f, 140.0f, -15.0f), Quaternion.identity);
            Fireballflg = true;
        }
        if (clone == null)
        {
            Destroy(clone);
        }
        if (lifeTime < 0.0f)
        {
            transform.position = savePosition;
            lifeTime = 0.0f;
        }
        if (lifeTime > 0.0f)
        {
            lifeTime -= Time.deltaTime;
            float x_val = Random.Range(lowRangeX, maxRangeX);
            float y_val = Random.Range(lowRangeY, maxRangeY);
            transform.position = new Vector3(x_val, y_val, transform.position.z);
        }
        if (Count < 90)
        {
            CatchShake();
            //this.transform.Rotate(-5 * Time.deltaTime, 0, 0);
            Count += Speed * Time.deltaTime;
        }
        else if (!FadeObj.IsFade && FadeFlg == false) //フェードイン
        {
            Destroy(clone);
            FadeObj.gameObject.SetActive(true);
            FadeController.Begin(FadeObj.gameObject, false, Rate);
            FadeFlg = true;
            FadeObj.m_onFinished += ChangeCamera;
        }

        if (clone != null)
        {
            this.transform.LookAt(clone.transform);
        }
        
    }

    public void ChangeCamera()
    {
        MainCamera.SetActive(!MainCamera.activeSelf);
        SubCamera.SetActive(!SubCamera.activeSelf);
        this.gameObject.SetActive(false);
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

    void CatchShake()
    {
        savePosition = transform.position;
        lowRangeY = savePosition.y - 0.5f;
        maxRangeY = savePosition.y + 0.5f;
        lowRangeX = savePosition.x - 0.5f;
        maxRangeX = savePosition.x + 0.5f;
        lifeTime = setShakeTime;
    }

}
