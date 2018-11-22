using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayersNumberText : MonoBehaviour {
    public RelayTV Relay;
    private Text text;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        //プレイヤーの色にする
        switch(Relay.GetPlayerNum())
        {
            case 1:
                //赤色
                text.color = new Color(1.0f, 0.0f, 0.0f);
                break;
            case 2:
                //青色
                text.color = new Color(0.0f, 0.0f, 1.0f);
                break;
            case 3:
                //黄色
                text.color = new Color(1.0f, 1.0f, 0.0f);
                break;
            case 4:
                //緑色
                text.color = new Color(0.0f, 1.0f, 0.0f);
                break;
            case 5:
                //紫色
                text.color = new Color(1.0f, 0.0f, 1.0f);
                break;
            case 6:
                //水色
                text.color = new Color(0.0f, 1.0f, 1.0f);
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        text.text = Relay.GetPlayerNum().ToString() + "P選手";

    }
}
