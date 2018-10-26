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
    public int PlayerID
    {
        get { return m_playerID; }
    }

    // 生存フラグ
    private bool m_lifeFlg;
    public bool LifeFlag
    {
        get { return m_lifeFlg; }
    }

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

    private string[] InputName = new string[(int)EInput.MAX];

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

    // 移動する力（パラメータ）
    [SerializeField]
    private Force m_bodyMoveForce;

    // 向きを変える力（パラメータ）
    [SerializeField]
    private float m_directionAngle;

    private State m_state;
    
    // 向き
    private RayTest.RayDirection dir;
    public RayTest rayTest;

    // パーツスケール
    private PartsScale m_partsScale;
    // Use this for initialization
    void Start ()
    {
       
        m_body_rg = m_bodyObj.GetComponent<Rigidbody>();
        m_rightHand_rg = m_rightHandObj.GetComponent<Rigidbody>();
        m_leftHand_rg = m_leftHandObj.GetComponent<Rigidbody>();
        m_rightFoot_rg = m_rightFootObj.GetComponent<Rigidbody>();
        m_leftFoot_rg = m_leftFootObj.GetComponent<Rigidbody>();

        m_bodyForce.Init();
        m_HandForce.Init();
        m_FootForce.Init();
        m_bodyMoveForce.Init();

        m_state = State.Idle;
        m_lifeFlg = true ;
        
        dir = rayTest.Dir;

        InputName[0] = "GameController_Hori" + m_playerID.ToString();
        InputName[1] = "GameController_Vert" + m_playerID.ToString();
        InputName[2] = "GameController_A" + m_playerID.ToString();
        InputName[3] = "GameController_B" + m_playerID.ToString();
        InputName[4] = "GameController_X" + m_playerID.ToString();
        InputName[5] = "GameController_Y" + m_playerID.ToString();

        m_partsScale = GetComponent<PartsScale>();

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!m_lifeFlg) return;
        // 右方向
        float lsh = Input.GetAxis(InputName[(int)EInput.Horizontal]);
 //       Debug.Log("横" + lsh);
        float lsv = Input.GetAxis(InputName[(int)EInput.Vertical]);
 //       Debug.Log("縦" + lsv);

        if (lsh == 0.0f)
        {
            m_state = State.Idle;
        }
        else if (lsh > 0.0f)
        {
            Move(1);
            m_state = State.RightMove;
        }
        // 左方向
        else if (lsh < 0.0f)
        {
            Move(-1);
            m_state = State.LeftMove;
        }

        if(lsv > 0.0f)
        {
            Direction(1);
        }
        else if(lsv < 0.0f)
        {
            Direction(-1);
        }

        // 手足の処理
        Limbs();

        // 回転減衰
        if (m_state == State.LeftMove || m_state == State.RightMove)
        {
            m_bodyForce.cntForce.x *= m_bodyForce.decayForce.x;
            m_bodyForce.cntForce.y *= m_bodyForce.decayForce.y;
            m_bodyForce.cntForce.z *= m_bodyForce.decayForce.z;
            m_bodyMoveForce.cntForce.x *= m_bodyMoveForce.decayForce.x;
        }
        else
        {
            m_bodyForce.Init();
            m_bodyMoveForce.Init();
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

    void Limbs()
    {
        // 右手
        if (Input.GetButtonDown(InputName[(int)EInput.A]))
        {
            Debug.Log(InputName[(int)EInput.A] + m_playerID);
            Extend(m_rightHand_rg, m_HandExtend);
            m_partsScale.PartsVariationSetting(PartsScale.Parts.RightHand, PartsScale.SubTraction.AddChange, m_partsScale.Speed);
        }
        else if (Input.GetButtonUp(InputName[(int)EInput.A]))
        {
            m_partsScale.PartsVariationSetting(PartsScale.Parts.RightHand, PartsScale.SubTraction.SubChange, m_partsScale.Speed);
        }
        // 左手
        if (Input.GetButtonDown(InputName[(int)EInput.B]))
        {
            Extend(m_leftHand_rg, -m_HandExtend);
            m_partsScale.PartsVariationSetting(PartsScale.Parts.LeftHand, PartsScale.SubTraction.AddChange, m_partsScale.Speed);
        }
        else if (Input.GetButtonUp(InputName[(int)EInput.B]))
        {
            m_partsScale.PartsVariationSetting(PartsScale.Parts.LeftHand, PartsScale.SubTraction.SubChange, m_partsScale.Speed);
        }
        // 右足
        if (Input.GetButtonDown(InputName[(int)EInput.X]))
        {
            Extend(m_rightFoot_rg, m_FootExtendR);
            m_partsScale.PartsVariationSetting(PartsScale.Parts.RightCalf, PartsScale.SubTraction.AddChange, m_partsScale.Speed);
        }
        else if (Input.GetButtonUp(InputName[(int)EInput.X]))
        {
            m_partsScale.PartsVariationSetting(PartsScale.Parts.RightCalf, PartsScale.SubTraction.SubChange, m_partsScale.Speed);
        }
        // 左足
        if (Input.GetButtonDown(InputName[(int)EInput.Y]))
        {
            Extend(m_leftFoot_rg, m_FootExtendL);
            m_partsScale.PartsVariationSetting(PartsScale.Parts.LeftCalf, PartsScale.SubTraction.AddChange, m_partsScale.Speed);
        }
        else if (Input.GetButtonUp(InputName[(int)EInput.Y]))
        {
            m_partsScale.PartsVariationSetting(PartsScale.Parts.LeftCalf, PartsScale.SubTraction.SubChange, m_partsScale.Speed);
        }
    }

    void Move(float value)
    {

        //m_rightHand.cntForce.x *= value;
        //m_leftHand.cntForce.x *= value;
        Vector3 worldAngulerVelocity = transform.TransformDirection(m_bodyForce.cntForce * value);
        Vector3 worldMoveVelocity = transform.TransformDirection(m_bodyMoveForce.cntForce * value);
        Vector3 worldHandVelocity = transform.TransformDirection(m_HandForce.cntForce );
        Vector3 worldFootVelocity = transform.TransformDirection(m_FootForce.cntForce);


        m_body_rg.angularVelocity = worldAngulerVelocity;
        m_body_rg.AddRelativeForce(worldMoveVelocity);
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

    void Direction(float value)
    {
        
        m_body_rg.AddTorque(0.0f, m_directionAngle * value ,0.0f);
    }

    void Extend(Rigidbody rigidbody,Vector3 vec)
    {
        //joint.spring = 0;
        Vector3 worldRightHandVelocity = transform.TransformDirection(vec);
       // Debug.Log(worldRightHandVelocity);
        rigidbody.AddForce(worldRightHandVelocity, ForceMode.Force);
    }


    public void PlayStart()
    {
        m_lifeFlg = true;
    }
    public void Dead()
    {
        m_lifeFlg = false;
    }
}
        
