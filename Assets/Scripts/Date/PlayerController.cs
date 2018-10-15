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

    public enum State
    {
        RightMove,
        LeftMove,
        Idle
    }

    [System.Serializable]
    struct Force
    {
        public Vector3 cntForce;
        public Vector3 maxForce;
        public Vector3 decayForce;
        public void Init()
        {
            cntForce = maxForce;
        }
    }

    private int m_playerID;

    // 手足制御用オブジェクト
    public GameObject m_bodyObj;
    public GameObject m_rightHandObj;
    public GameObject m_leftHandObj;
    public GameObject m_rightFootObj;
    public GameObject m_leftFootObj;

    // 手足制御オブジェクト（Rigidbody）
    private Rigidbody m_body_rg;
    private Rigidbody m_rightHand_rg;
    private Rigidbody m_leftHand_rg;
    private Rigidbody m_rightFoot_rg;
    private Rigidbody m_leftFoot_rg;

    public string[] InputName = new string[(int)EInput.MAX];

    // 力（パラメータ）
    [SerializeField]
    private Force m_bodyForce;
    [SerializeField]
    private Force m_rightHand;
    [SerializeField]
    private Force m_leftHand;
    [SerializeField]
    private Force m_rightFoot;
    [SerializeField]
    private Force m_leftFoot;

    // ベクトルの軸
   // [SerializeField]
   // private Transform m_shoulderR;

   // [SerializeField]
   // private GameObject m_shoulder_R;

    private State m_state;
    
    private RayTest.RayDirection dir;
    public RayTest rayTest;
    // Use this for initialization
    void Start ()
    {
        //for(int i=0;i<(int)EInput.MAX;++i)
        //{
        //    KeyName[i] += m_playerID.ToString();
        //}

        m_body_rg = m_bodyObj.GetComponent<Rigidbody>();
        m_rightHand_rg = m_rightHandObj.GetComponent<Rigidbody>();
        m_leftHand_rg = m_leftHandObj.GetComponent<Rigidbody>();
        m_rightFoot_rg = m_rightFootObj.GetComponent<Rigidbody>();
        m_leftFoot_rg = m_leftHandObj.GetComponent<Rigidbody>();

        m_bodyForce.Init();
        m_rightHand.Init();
        m_rightFoot.Init();
        m_leftHand.Init();
        m_leftFoot.Init();

        m_state = State.Idle;
        
        dir = rayTest.Dir;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // 右スティック
        if (Input.GetKey(KeyCode.D))
        {
            Move(1);
            m_state = State.RightMove;
        }
        // 左スティック
        else if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
            m_state = State.LeftMove;
        }

        else if(!Input.GetKey(KeyCode.D) )
        {
            //m_state = State.Idle;
            m_bodyForce.Init();
            m_rightHand.Init();
            m_rightFoot.Init();
            m_leftHand.Init();
            m_leftFoot.Init();
        }
        else if(!Input.GetKey(KeyCode.A))
        {
            m_bodyForce.Init();
            m_rightHand.Init();
            m_rightFoot.Init();
            m_leftHand.Init();
            m_leftFoot.Init();
        }

        if (Input.GetKey(KeyCode.K))
        {
            Extend(m_rightHand_rg,m_rightHandObj.GetComponent<SpringJoint>(),new Vector3(20,0,0));
        }
        else
        {
           // m_rightHandObj.GetComponent<SpringJoint>().spring = 20;
        }


        if (Input.GetKey(KeyCode.L))
        {
            Extend(m_leftHand_rg, m_leftHandObj.GetComponent<SpringJoint>(),new Vector3(-20,0,0));
        }
        else
        {
           // m_leftHandObj.GetComponent<SpringJoint>().spring = 20;
        }
        if (Input.GetKey(KeyCode.O))
        {

        }
        if (Input.GetKey(KeyCode.P))
        {

        }
       // if(Input)
         
         // 回転減衰
        if(m_state == State.LeftMove || m_state == State.RightMove)
        {
            m_bodyForce.cntForce.x *= m_bodyForce.decayForce.x;
            m_bodyForce.cntForce.y *= m_bodyForce.decayForce.y;
            m_bodyForce.cntForce.z *= m_bodyForce.decayForce.z;


        }
        else
        {
            m_bodyForce.Init();
        }

        // キャラクターの向き
        if(dir != rayTest.Dir)
        {
            // パラメータ初期化
            dir = rayTest.Dir;
            m_state = State.Idle;
            m_bodyForce.Init();
            m_rightHand.Init();
            m_rightFoot.Init();
            m_leftHand.Init();
            m_leftFoot.Init();
        }
        Debug.Log(m_state);
    }

    

    void Move(float value)
    {

        //m_rightHand.cntForce.x *= value;
        //m_leftHand.cntForce.x *= value;
        Vector3 worldAngulerVelocity = transform.TransformDirection(m_bodyForce.cntForce * value);
        Vector3 worldRightHandVelocity = transform.TransformDirection(m_rightHand.cntForce );
        Vector3 worldLeftHandVelocity = transform.TransformDirection(m_leftHand.cntForce );
        Vector3 worldFootVelocity = transform.TransformDirection(m_rightFoot.cntForce);


        m_body_rg.angularVelocity = worldAngulerVelocity;
        // 前
        if (dir == RayTest.RayDirection.Forward)
        {
            m_rightHand_rg.AddRelativeForce(worldRightHandVelocity, ForceMode.Force);
            m_leftHand_rg.AddRelativeForce(worldLeftHandVelocity, ForceMode.Force);

            // m_shoulder_R.transform.Rotate(10.0f, 0, 0);
            // m_rightHand_rg.AddRelativeForce(-m_rightHand.cntForce * value, ForceMode.Force);
            // m_rightFoot_rg.AddRelativeForce(m_rightFoot.cntForce * value, ForceMode.Force);

            m_rightFoot_rg.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            m_leftFoot_rg.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            if (value > 0)
                Debug.Log("右移動：キャラクターの向き（前）");
            else
                Debug.Log("左移動：キャラクターの向き（前）");
        }
        // 後ろ
        else if (dir == RayTest.RayDirection.Back)
        {
            m_leftHand_rg.AddRelativeForce(-worldLeftHandVelocity, ForceMode.Force);
            m_rightHand_rg.AddRelativeForce(-worldRightHandVelocity, ForceMode.Force);
            // m_leftFoot_rg.AddRelativeForce(m_leftFoot.cntForce * value, ForceMode.Force);
            // m_rightHand_rg.AddRelativeForce(m_rightHand.cntForce * value, ForceMode.Force);


            // m_rightFoot_rg.AddRelativeForce(m_rightFoot.cntForce * value, ForceMode.Force);

            m_rightFoot_rg.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            m_leftFoot_rg.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            if (value > 0)
                Debug.Log("右移動：キャラクターの向き（後）");
            else
                Debug.Log("左移動：キャラクターの向き（後）");
        }
    }

    void Extend(Rigidbody rigidbody,SpringJoint joint,Vector3 vec)
    {
        joint.spring = 0;
        Vector3 worldRightHandVelocity = transform.TransformDirection(vec);
        Debug.Log(worldRightHandVelocity);
        rigidbody.AddForce(worldRightHandVelocity, ForceMode.Force);
    }
}
        
