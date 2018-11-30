using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager:MonoBehaviour {

    public string[] name;
    public PlayerController[] input = new PlayerController[6];
    public int[] joysticks = new int[6];
    private int[] oldJoysticks = new int[6];
    private bool[] IsConnectFlgs = new bool[6];
    [SerializeField]
    private string controllerName;
    [SerializeField]
    private ControllerDisconnect canvasInterruption;
    private bool m_isAllConnectFlag;
    public bool IsAllConnectedFlag
    {
        get { return m_isAllConnectFlag; }
    }
    private List<int> m_disconnectedNum = new List<int>();
    // Use this for initialization
    void Start () {
        name = new string[10];
        var stick = Input.GetJoystickNames();
        for(int i = 0; i < stick.Length; ++i)
        {
            name[i] = stick[i];
        }
        // ダミー
        for (int i = 0; i < 6; ++i)
        {
            oldJoysticks[i] = i + 1;
            joysticks[i] = -1;
            IsConnectFlgs[i] = true;
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

        for (int i = 0; i < joysticks.Length; ++i)
        {
            //if(name[i] != stick[i])
                //name[i] = stick[i];
            if (!string.IsNullOrEmpty(stick[i]))
            {
                // 文字列にコントローラ名がある
                if (stick[i] == controllerName)
                {
                    num++;
                    directNum++;
                    joysticks[directNum - 1] = i + 1;
                    IsConnectFlgs[num - 1] = true;
                }
            }
            else
            {
                // 前フレームのコントローラ情報と比較
                if (name[i] == stick[i])
                {
                    num++;
                    IsConnectFlgs[num - 1] = false;
                }
            }
            name[i] = stick[i];
        }
        // どこか抜けてないか確認
        /*
        int diffNum = 0;
        int maxNum = 0;
        int lastcount = 0;
        for (int i = 0; i < oldJoysticks.Length; i++)
        {
            maxNum = i + 1 + diffNum;
            if (maxNum > 6) maxNum = 6;
            if (joysticks[i] == 6) lastcount++;
            if (joysticks[i] != maxNum)
            {
                m_disconnectedNum.Add(i + 1);
                    diffNum++;
                }
            }
        Debug.Log("mmmm" + lastcount);
            Debug.Log("llll" + m_disconnectedNum.Count);
            */
        for(int i = 0; i < IsConnectFlgs.Length; ++i)
        {
            // falseの時（i+1）= playerIDをリストに追加
            if (!IsConnectFlgs[i]) m_disconnectedNum.Add(i + 1);
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
            canvasInterruption.OnDisconnected(m_disconnectedNum);
        }
        m_disconnectedNum.Clear();
    }
    
    private bool IsAllController()
    {
        int num = 0;
        for(int i = 0; i < name.Length; ++i)
        {
            if (!string.IsNullOrEmpty(name[i]))
                num++;
        }
        if (num == 6) return true;
        return false;
    }
}
