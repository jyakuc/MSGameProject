using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum EInput
    {
        Horizontal,
        Vertical,
        A,B,X,Y,
        MAX
    }

    private int m_playerID;
 
    // 手足制御用オブジェクト
    public GameObject m_rightHandObj;
    public GameObject m_leftHandObj;
    public GameObject m_rightFootObj;
    public GameObject m_leftFootObj;
    public GameObject m_centerObj;

    public string[] KeyName = new string[(int)EInput.MAX];
	// Use this for initialization
	void Start ()
    {

        for(int i=0;i<(int)EInput.MAX;++i)
        {
            KeyName[i] += m_playerID.ToString();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        // 右スティック
	    if(Input.GetAxis(KeyName[(int)EInput.Horizontal]) > 0)
        {

        }
        // 左スティック
        else if(Input.GetAxis(KeyName[(int)EInput.Horizontal]) < 0)
        {

        }

        // 上スティック
        if(Input.GetAxis(KeyName[(int)EInput.Vertical]) > 0)
        {

        }
        // 下スティック
        else if(Input.GetAxis(KeyName[(int)EInput.Vertical]) < 0)
        {

        }

        if (Input.GetButtonDown(KeyName[(int)EInput.A]))
        {

        }
        if (Input.GetButtonDown(KeyName[(int)EInput.B]))
        {

        }
        if (Input.GetButtonDown(KeyName[(int)EInput.X]))
        {

        }
        if (Input.GetButtonDown(KeyName[(int)EInput.Y]))
        {

        }
	}
}
