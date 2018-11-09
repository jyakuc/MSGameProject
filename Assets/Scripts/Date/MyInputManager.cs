using UnityEngine;

public class MyInputManager:MonoBehaviour {

    public string[] name;
    public PlayerController[] input = new PlayerController[6];
    public int[] joysticks = new int[6];
    [SerializeField]
    private string controllerName;

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
            joysticks[i] = i+1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!DebugModeGame.GetProperty().m_controllerEnable) return;
        var stick = Input.GetJoystickNames();
        //Debug.Log("コントローラー接続台数:" + stick.Length);

        int num = 0;
        int directNum = 0;
        for(int i = 0; i < stick.Length; ++i)
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
        Debug.Log("動的接続:" + directNum);
        if (directNum == 6)
            m_isAllConnectFlag = true;
        else
            m_isAllConnectFlag = false;
    }
    
}
