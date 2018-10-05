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
    public GameObject m_FootObj;

    public string[] KeyName = new string[(int)EInput.MAX];

    [SerializeField]
    private float m_aroundSpeed;
    [SerializeField]
    private Vector2 m_handlimit_rotation;
    [SerializeField]
    private Vector2 m_shoulderlimit_rotation;
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
        m_centerObj.transform.position = transform.position;

        // 右スティック
	    //if(Input.GetAxis(KeyName[(int)EInput.Horizontal]) > 0 || Input.GetKeyDown(KeyCode.D))
     //   {
     //       Move(true);
     //   }
     //   // 左スティック
     //   else if(Input.GetAxis(KeyName[(int)EInput.Horizontal]) < 0 || Input.GetKeyDown(KeyCode.A))
     //   {
     //       Move(false);
     //   }

        // 右スティック
        if (Input.GetKey(KeyCode.D))
        {
            Move(true,true);
        }
        // 左スティック
         if (Input.GetKey(KeyCode.A))
        {
            Move(false,true);
        }

        // 右スティック
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(true,false);
        }
        // 左スティック
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(false,false);
        }
        /*
        // 上スティック
        if (Input.GetAxis(KeyName[(int)EInput.Vertical]) > 0)
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

        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_aroundSpeed *= -1;
        }
	}

    void Move(bool leftFlg , bool ddd)
    {
        m_rightHandObj.GetComponent<Rigidbody>().centerOfMass = m_centerObj.transform.position;
        // float angle = m_aroundSpeed * Time.deltaTime;

        Debug.Log("Move");
        if (leftFlg)
        {
            Vector3 rota_leftHand = m_rightHandObj.transform.localRotation.eulerAngles;
            float y = Mathf.Clamp(m_rightHandObj.transform.localRotation.eulerAngles.y, m_handlimit_rotation.x, m_handlimit_rotation.y);
            Debug.Log(rota_leftHand);
            if (ddd)
                m_rightHandObj.transform.RotateAround(m_centerObj.transform.position, transform.up, m_aroundSpeed * Time.deltaTime);


            if (!ddd)
            {
                Debug.Log(rota_leftHand.y);
                m_rightHandObj.transform.RotateAround(m_centerObj.transform.position, -transform.up, m_aroundSpeed * Time.deltaTime);
            }
            //            m_rightFootObj.transform.RotateAround(m_centerObj.transform.position, transform.up, m_aroundSpeed * 0.6f * Time.deltaTime);
            //m_rightHandObj.GetComponent<Rigidbody>().centerOfMass = m_centerObj.transform.position;
            //m_rightHandObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, m_aroundSpeed * Time.deltaTime, 0);
            m_rightHandObj.transform.localRotation = Quaternion.Euler(m_rightHandObj.transform.localRotation.x,y, m_rightHandObj.transform.localRotation.z);
        }
        else
        {
            Vector3 rota_rightHand = m_rightHandObj.transform.localRotation.eulerAngles;
            Debug.Log(rota_rightHand);
            if (rota_rightHand.y > m_handlimit_rotation.x && ddd)
                m_leftHandObj.transform.RotateAround(m_centerObj.transform.position, transform.up, -m_aroundSpeed * Time.deltaTime);
            if (rota_rightHand.y < m_handlimit_rotation.y && !ddd)
                m_leftHandObj.transform.RotateAround(m_centerObj.transform.position, -transform.up, -m_aroundSpeed * Time.deltaTime);
            //            m_leftFootObj.transform.RotateAround(m_centerObj.transform.position, transform.up, m_aroundSpeed * 0.6f * Time.deltaTime);

            //m_leftHandObj.GetComponent<Rigidbody>().centerOfMass = m_centerObj.transform.position;
            //m_leftHandObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, m_aroundSpeed * Time.deltaTime, 0);
        }
        m_rightFootObj.GetComponent<Rigidbody>().useGravity = false;
        m_leftFootObj.GetComponent<Rigidbody>().useGravity = false;
        m_rightFootObj.GetComponent<Rigidbody>().useGravity = true;
        m_leftFootObj.GetComponent<Rigidbody>().useGravity = true;
    }
}
        
