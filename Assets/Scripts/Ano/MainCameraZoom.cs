using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainCameraZoom : MonoBehaviour {
    private Camera MyCamera;
    private StartTimer TimerFlag;
    private GameController Gamecontroller;
    private int DebugPlayerNum;
    public int SurvivalPlayerNum;
    public GameObject[] Players;
    public Vector3[] SavePos = new Vector3[6];
    bool PlayersSetFlag = false;
    private Vector3 offset=new Vector3(0,90,-60);

    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 50;
    public float maxZoom = 10;
    public float zoomLimiter = 50;
    public bool ZoomMode = false;
    // Use this for initialization
    void Start () {
        MyCamera = GetComponent<Camera>();
        DebugPlayerNum = GameObject.Find("GameController").GetComponent<DebugModeGame>().m_debugMode.m_debugPlayerNum;
        Gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        Players = new GameObject[DebugPlayerNum];
        SurvivalPlayerNum = DebugPlayerNum;
    }
	
	// Update is called once per frame
	void LateUpdate() {
        if(ZoomMode)
        {
            PlayerSetChecker();
            if ((PlayersSetFlag) && (Gamecontroller.GameState == GameController.EState.Main))
            {
                Move();
                Zoom();
                DeletePlayerData();
            }
        }

    //ZoomingSystem();
    }
    private void PlayerSetChecker()
    {
        for (int i = 0; i < DebugPlayerNum - 1; i++)
        {
            if(Players[i]==null)
            {
                return;
            }
        }
        PlayersSetFlag = true;
    }

    private void Zoom()
    {
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        MyCamera.fieldOfView = Mathf.Lerp(MyCamera.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        transform.LookAt(centerPoint);
    }

    private float GetGreatestDistance()
    {
        //int PlayerLife = 0;
        //for(int i=0;i<DebugPlayerNum;i++)
        //{
        //    if (Players[i] != null)
        //    {
        //        PlayerLife = i;
        //        break;
        //    }
        //}
        var bounds = new Bounds(SavePos[0], Vector3.zero);
        for (int i = 0; i < SurvivalPlayerNum; i++)
        {
            //if(Players[i]==null)
            //{
            //    continue;
            //}
            bounds.Encapsulate(SavePos[i]);
        }
        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        for (int i = 0; i < SurvivalPlayerNum; i++)
        {
            //if (Players[i] == null)
            //{
            //    continue;
            //}
            //オブジェクトのポジションの平均値を算出
            pos += SavePos[i];
        }
        Vector3 center = pos / SurvivalPlayerNum;
        //center = (center / Mathf.Sin(MyCamera.fieldOfView / 2));
        return center;
    }
    public void SetPlayerData(GameObject PlayerData,int Id)
    {
        if(Players[Id-1]==null)
        {
            Players[Id - 1] = PlayerData;
            SavePos[Id - 1] = Players[Id - 1].transform.root.GetChild(0).position;
        }
    }
    private void DeletePlayerData()
    {
        for(int i=0;i< SurvivalPlayerNum; i++)
        {
            try
            {
                if (!Players[i].activeSelf)
                {
                    Players[i] = null;
                    //SurvivalPlayerNum -= 1;
                }
                else
                {
                    SavePos[i] = Players[i].transform.root.GetChild(0).position;
                }
            }
            catch
            {
                for(int j=i;j<5;j++)
                {
                    SavePos[j] = SavePos[j+1];
                    Players[j] = Players[j + 1];
                }
                SurvivalPlayerNum -= 1;
            }
        }

    }
}
