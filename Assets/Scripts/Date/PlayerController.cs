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

    private int m_playerID;
 
    // 手足制御用オブジェクト
    public GameObject m_rightHandObj;
    public GameObject m_leftHandObj;
    public GameObject m_rightFootObj;
    public GameObject m_leftFootObj;
    public GameObject[] m_centerObj;
    public GameObject m_FootObj;
    public GameObject m_shoulder_R;
    public GameObject m_partsMove;

    public string[] KeyName = new string[(int)EInput.MAX];

    [SerializeField]
    private float m_initspeed;
    [SerializeField]
    private float m_aroundSpeed;
    [SerializeField]
    private float m_aroundSpeedDecay;

    [SerializeField]
    private Vector3 m_rightHandForce;
    private Vector3 m_rightHandForceInit;
    [SerializeField]
    private Vector3 m_rightFootForce;
    private Vector3 m_rightFootForceInit;
    [SerializeField]
    private Vector3 m_leftHandForce;
    private Vector3 m_leftHandForceInit;
    [SerializeField]
    private Vector3 m_leftFootForce;
    private Vector3 m_leftFootForceInit;

    [SerializeField]
    private Vector3 m_rightForceDecay;
    [SerializeField]
    private Vector3 m_leftForceDecay;
    private State m_state;

    [SerializeField]
    private Vector2 m_handlimit_rotation;
    [SerializeField]
    private Vector2 m_shoulderlimit_rotation;

    private Rigidbody m_rightHand_rg;
    private Rigidbody m_leftHand_rg;
    private Rigidbody m_rightFoot_rg;
    private Rigidbody m_leftFoot_rg;

    bool jj = false;
    Ray ray;
    LayerMask mask;
    public RayTest rayTest;
    private RayTest.RayDirection dir;
    // Use this for initialization
    void Start ()
    {
        //for(int i=0;i<(int)EInput.MAX;++i)
        //{
        //    KeyName[i] += m_playerID.ToString();
        //}

        m_rightHand_rg = m_rightHandObj.GetComponent<Rigidbody>();
        m_leftHand_rg = m_leftHandObj.GetComponent<Rigidbody>();
        m_rightFoot_rg = m_rightFootObj.GetComponent<Rigidbody>();
        m_leftFoot_rg = m_leftHandObj.GetComponent<Rigidbody>();

        m_state = State.Idle;

        ray = new Ray(transform.position, transform.forward);
        int no = LayerMask.NameToLayer("Ground");
        mask = 1 << no;
        dir = rayTest.Dir;

        m_rightHandForceInit = m_rightHandForce;
        m_rightFootForceInit = m_rightFootForce;
        m_leftHandForceInit = m_leftHandForce;
        m_leftFootForceInit = m_leftFootForce;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //m_partsMove.transform.position = transform.position;
        //m_centerObj[0].transform.position = transform.position;

        //  float stickvalue;
        // 右スティック
        //if(stickvalue = Input.GetAxis(KeyName[(int)EInput.Horizontal]) > 0 || Input.GetKeyDown(KeyCode.D))
        //   {
        //       Move(true);
        //   }
        //   // 左スティック
        //   else if(stickvalue = Input.GetAxis(KeyName[(int)EInput.Horizontal]) < 0 || Input.GetKeyDown(KeyCode.A))
        //   {
        //       Move(false);
        //   }


        // 右スティック
        if (Input.GetKey(KeyCode.D))
        {
            Move(-1);
        }
        // 左スティック
         if (Input.GetKey(KeyCode.A))
        {
            Move(1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //m_rightHand_rg.AddRelativeForce(-m_rightHandForce, ForceMode.Force);
            m_rightHand_rg.velocity = Vector3.zero;
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

        if(m_state == State.LeftMove || m_state == State.RightMove)
        {
            //
            m_aroundSpeed *= 0.996f;
            m_rightHandForce.x = m_rightHandForce.x * m_rightForceDecay.x ;
            m_rightHandForce.z = m_rightHandForce.z * m_rightForceDecay.z ;
            m_rightFootForce.x = m_rightFootForce.x * m_rightForceDecay.x;
            m_rightFootForce.z = m_rightFootForce.z * m_rightForceDecay.z;

            m_leftHandForce.x = m_leftHandForce.x * m_leftForceDecay.x;
            m_leftHandForce.z = m_leftHandForce.z * m_leftForceDecay.z;
            m_leftFootForce.x = m_leftFootForce.x * m_leftForceDecay.x;
            m_leftFootForce.z = m_leftFootForce.z * m_leftForceDecay.z;
        }

        if(dir != rayTest.Dir)
        {
            m_state = State.Idle;
            dir = rayTest.Dir;
            m_aroundSpeed = m_initspeed;

            if (dir == RayTest.RayDirection.Back)
            {
                m_rightHandForce = m_rightHandForceInit;
                m_rightFootForce = m_rightFootForceInit;
                m_leftHandForce = m_leftHandForceInit;
                m_leftFootForce = m_leftFootForceInit;
            }else if(dir == RayTest.RayDirection.Forward)
            {
                m_rightHandForce = -m_rightHandForceInit;
                m_rightFootForce = -m_rightFootForceInit;
                m_leftHandForce = -m_leftHandForceInit;
                m_leftFootForce = -m_leftFootForceInit;
            }
        }
    }

    

    void Move(float value)
    {

        m_rightHand_rg.centerOfMass = m_centerObj[0].transform.position;
        m_rightFoot_rg.centerOfMass = m_centerObj[1].transform.position;
        m_leftHand_rg.centerOfMass = m_centerObj[0].transform.position;
        m_leftFoot_rg.centerOfMass = m_centerObj[1].transform.position;

        // 右
        if (value < 0)
        {
            //m_rightHandSpeed += m_aroundSpeed;
            //if (m_rightHandSpeed >= 30.0f && m_rightHandSpeed <= 120.0f)
            {
                //m_rightHandObj.transform.RotateAround(m_centerObj[0].transform.position, Vector3.forward, m_aroundSpeed * 2.0f);
                //m_rightFootObj.transform.RotateAround(m_centerObj[1].transform.position, Vector3.forward, m_aroundSpeed * 2.0f);
                //m_leftHandObj.transform.RotateAround(m_centerObj[0].transform.position, Vector3.forward, m_aroundSpeed * 2.0f);
                //m_leftFootObj.transform.RotateAround(m_centerObj[1].transform.position, Vector3.forward, m_aroundSpeed * 2.0f);


            }
            //if (m_rightHandSpeed <= 170.0f)
            {
              //  m_aroundSpeed = m_initspeed;
                //transform.RotateAround(m_shoulder_R.transform.position, Vector3.forward, m_aroundSpeed);
                //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 1.0f);
                //m_rightHand_rg.angularVelocity = new Vector3(0, 0, 1.0f);

            }
            //m_rightHand_rg.angularVelocity = new Vector3(0, 0, m_aroundSpeed);
            //m_rightFoot_rg.angularVelocity = new Vector3(0, 0, m_aroundSpeed);
            //m_leftHand_rg.angularVelocity = new Vector3(0, 0, m_aroundSpeed);
            //m_leftFoot_rg.angularVelocity = new Vector3(0, 0, m_aroundSpeed);
            //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, m_aroundSpeed);
            
            m_rightHand_rg.AddRelativeForce(m_rightHandForce, ForceMode.Force);
            //m_rightFoot_rg.AddRelativeForce(m_rightFootForce, ForceMode.Force);
            //m_leftHand_rg.AddRelativeForce(m_leftHandForce, ForceMode.Force);
            //m_leftFoot_rg.AddRelativeForce(m_leftFootForce, ForceMode.Force);
            if (!jj)
            {
               // m_rightHand_rg.AddRelativeTorque(1f, 0, 0.0f, ForceMode.Impulse);
                jj = true;
            }
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, m_aroundSpeed);


            m_state = State.RightMove;
        }
        // 左
        else if (value > 0)
        {
           
        }
    }
}
        
