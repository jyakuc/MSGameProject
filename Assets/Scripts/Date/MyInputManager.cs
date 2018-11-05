using UnityEngine;

public class MyInputManager:MonoBehaviour {
    public string[] name;
    public PlayerController[] input = new PlayerController[6];
    [SerializeField]
    private string controllerName;
	// Use this for initialization
	void Start () {
        name = new string[10];
	}
	
	// Update is called once per frame
	void Update () {
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
                    input[directNum - 1].JoystickNum = i+1;
                }

            }

        }
        Debug.Log("動的接続:" + directNum);
    }
}
