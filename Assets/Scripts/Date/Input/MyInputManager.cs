using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager:MonoBehaviour {

    public string[] name;
    public PlayerController[] input = new PlayerController[6];
    public int[] joysticks = new int[6];
    private int[] oldJoysticks = new int[6];
    [SerializeField]
    private string controllerName;
    [SerializeField]
    private ControllerDisconnect canvasInterruption;
    private bool m_isAllConnectFlag;
    public bool IsAllConnectedFlag
    {
        get { return m_isAllConnectFlag; }
    }
	// Use this for initialization
	void Start () {
        name = new string[10];
        // ダミー
        for(int i = 0; i < 6; ++i)
        {
            oldJoysticks[i] = i + 1;
            joysticks[i] = i+1;
        }
        if (canvasInterruption == null) Debug.LogError("コントローラー抜け表示のCanvasをセットしてください。");

	}

    // Update is called once per frame
    void Update() {
        if (DebugModeGame.GetProperty().m_debugMode)
            if (DebugModeGame.GetProperty().m_controllerDisable) return;

        var stick = Input.GetJoystickNames();
        //Debug.Log("コントローラー接続台数:" + stick.Length);

        int num = 0;
        int directNum = 0;

        // 前フレームでのコントローラー接続退避
        for(int i = 0; i < oldJoysticks.Length; i++)
        {
            oldJoysticks[i] = joysticks[i];
        }

        for (int i = 0; i < stick.Length; ++i)
        {
            name[i] = stick[i];
            if (!string.IsNullOrEmpty(stick[i]))
            {
                num++;
                if (stick[i] == controllerName)
                {
                    directNum++;
                    //input[directNum - 1].JoystickNum = i+1;
                    joysticks[directNum - 1] = i + 1;
                }

            }
        }
        // どこか抜けてないか確認
        List<int> disconnectedNum = new List<int>();
        for(int i = 0;i< oldJoysticks.Length; i++)
        {
            if (oldJoysticks[i] != joysticks[i])
                disconnectedNum.Add(i + 1);
        }
        Debug.Log("動的接続:" + directNum);
        if (directNum == 6)
        {
            m_isAllConnectFlag = true;
            canvasInterruption.OnConnectedReady();
        }
        else
        {
            m_isAllConnectFlag = false;
            canvasInterruption.OnDisconnected(disconnectedNum);
        }
    }
    
}
