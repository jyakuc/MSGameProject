using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CannonInterface : MonoBehaviour
{

    private const int MaxPlayers = 6;
    private int PracticalPlayers;   // 実際の人数
    
    Cursor[] targetCursor = new Cursor[MaxPlayers];

    [SerializeField]
    CannonController cannon;

    //[SerializeField]
    //Text timeOfFlightText;

    [SerializeField]
    float defaultFireSpeed = 5;

    [SerializeField]
    float defaultFireAngle = 25;

    private float initialFireAngle;
    private float initialFireSpeed;
    
    private bool useLowAngle;

    private bool useInitialAngle;

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private MyInputManager myInputManager;

    private GameObject g_UI;
    private GameObject _slider_Background;
    private GameObject _slider_Fillarea;
    private StartTimer timer;

    void Awake()
    {
        useLowAngle = true;

        initialFireAngle = defaultFireAngle;
        initialFireSpeed = defaultFireSpeed;

        useInitialAngle = true;


    }

    private void Start()
    {
        PracticalPlayers = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum : MaxPlayers;

        GameObject ParentCursor = GameObject.Find("Cursors(Clone)");
        for (int i = 0; i < MaxPlayers; i++)
        {
            if (i < PracticalPlayers)
                targetCursor[i] = ParentCursor.transform.GetChild(i).GetComponent<Cursor>();
            else
                ParentCursor.transform.GetChild(i).gameObject.SetActive(false);
        }
        gameController = GameObject.FindObjectOfType<GameController>();

        if (gameController == null)
            Debug.LogError("Gamecontrollerがシーンにありません");

        myInputManager = GameObject.FindObjectOfType<MyInputManager>();
        if (myInputManager == null)
            Debug.LogError("MyInputManagerがシーンにありません");

        g_UI = GameObject.Find("GameUI");
        _slider_Background = g_UI.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject;
        _slider_Fillarea = g_UI.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject;
        timer = GameObject.FindObjectOfType<StartTimer>();


    }

    void Update()
    {
        for (int i = 0; i < PracticalPlayers; i++)
        {
            
            if (targetCursor[i] == null) continue;
            if (useInitialAngle)
                cannon.SetTargetWithAngle(targetCursor[i].transform.position, initialFireAngle, i);
            else
                cannon.SetTargetWithSpeed(targetCursor[i].transform.position, initialFireSpeed, useLowAngle, i);
            
            if (targetCursor[i].FireFlg)
            {
                if (timer.TimerFinishflg)
                {
                    gameController.AddPlayer(cannon.FireHuman(i));
                    targetCursor[i].FireFlg = false;
                    _slider_Background.gameObject.SetActive(false);
                    _slider_Fillarea.gameObject.SetActive(false);
                    DeleteCursor(i);
                }
            }
            // デバッグ用射出
            if(DebugModeGame.GetProperty().m_debugMode && DebugModeGame.GetProperty().m_Injection > i)
            {
                if (Input.GetButtonDown("Injection_" + (i + 1).ToString()))
                {
                    gameController.AddPlayer(cannon.FireHuman(i));
                    targetCursor[i].FireFlg = false;
                    DeleteCursor(i);
                }
            }
        }
        //timeOfFlightText.text = Mathf.Clamp(cannon.lastShotTimeOfFlight - (Time.time - cannon.lastShotTime), 0, float.MaxValue).ToString("F3");
    }

    public void SetInitialFireAngle(string angle)
    {
        initialFireAngle = Convert.ToSingle(angle);
    }

    public void SetInitialFireSpeed(string speed)
    {
        initialFireSpeed = Convert.ToSingle(speed);
    }

    public void SetLowAngle(bool useLowAngle)
    {
        this.useLowAngle = useLowAngle;
    }

    public void UseInitialAngle(bool value)
    {
        useInitialAngle = value;
    }

    public void DeleteCursor(int playerID)
    {
        Destroy(targetCursor[playerID].gameObject);
        Destroy(cannon.projectileArc[playerID].gameObject);
    }
}
