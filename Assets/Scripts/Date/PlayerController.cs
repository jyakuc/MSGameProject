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

    // プレイヤー番号
    [SerializeField]
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

    // 回転する力（パラメータ）
    [SerializeField]
    private Force m_bodyForce;
    [SerializeField]
    private Force m_HandForce;
    [SerializeField]
    private Force m_FootForce;

    // 伸ばす力（パラメータ）
    [SerializeField]
    private Vector3 m_HandExtend;
    [SerializeField]
    private Vector3 m_FootExtendR;
    [SerializeField]
    private Vector3 m_FootExtendL;

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
        m_leftFoot_rg = m_leftFootObj.GetComponent<Rigidbody>();

        m_bodyForce.Init();
        m_HandForce.Init();
        m_FootForce.Init();

        m_state = State.Idle;
        
        dir = rayTest.Dir;

        for(EInput i = 0; i < EInput.MAX; ++i)
        {
            InputName[(int)i] += m_playerID.ToString();
        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // 右スティック
      /*  if (Input.GetAxis(""))
        {
            Move(1);
            m_state = State.RightMove;
        }*/
        // 左スティック
        else if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
            m_state = State.LeftMove;
        }
        else
        {
            m_state = State.Idle;
        }


        if (Input.GetKey(KeyCode.K))
        {
            Extend(m_rightHand_rg,m_HandExtend);
        }
        if (Input.GetKey(KeyCode.L))
        {
            Extend(m_leftHand_rg, -m_HandExtend);
        }
        if (Input.GetKey(KeyCode.I))
        {
            Extend(m_rightFoot_rg, m_FootExtendR);
        }
        if (Input.GetKey(KeyCode.O))
        {
            Extend(m_leftFoot_rg, m_FootExtendL);
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
            m_HandForce.Init();
            m_FootForce.Init();
        }
        Debug.Log(m_state);
    }

    

    void Move(float value)
    {

        //m_rightHand.cntForce.x *= value;
        //m_leftHand.cntForce.x *= value;
        Vector3 worldAngulerVelocity = transform.TransformDirection(m_bodyForce.cntForce * value);
        Vector3 worldHandVelocity = transform.TransformDirection(m_HandForce.cntForce );
        Vector3 worldFootVelocity = transform.TransformDirection(m_FootForce.cntForce);


        m_body_rg.angularVelocity = worldAngulerVelocity;
        m_body_rg.AddRelativeForce(worldAngulerVelocity.x * value, 0, worldAngulerVelocity.z);
        // 前
        if (dir == RayTest.RayDirection.Forward)
        {
            m_rightHand_rg.AddRelativeForce(worldHandVelocity, ForceMode.Force);
            m_leftHand_rg.AddRelativeForce(worldHandVelocity, ForceMode.Force);
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
            m_leftHand_rg.AddRelativeForce(-worldHandVelocity, ForceMode.Force);
            m_rightHand_rg.AddRelativeForce(-worldHandVelocity, ForceMode.Force);
            m_rightFoot_rg.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            m_leftFoot_rg.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            if (value > 0)
                Debug.Log("右移動：キャラクターの向き（後）");
            else
                Debug.Log("左移動：キャラクターの向き（後）");
        }
    }

    void Extend(Rigidbody rigidbody,Vector3 vec)
    {
        //joint.spring = 0;
        Vector3 worldRightHandVelocity = transform.TransformDirection(vec);
        Debug.Log(worldRightHandVelocity);
        rigidbody.AddForce(worldRightHandVelocity, ForceMode.Force);
    }
}
        
