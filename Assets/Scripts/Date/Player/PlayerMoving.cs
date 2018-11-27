using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {

    // 移動力
    private Vector3 m_anglePower;
    private Vector3 m_movePower;
    //private Vector3 m_handPower;
    //private Vector3 m_footPower;

    // 方向転換力
    private Vector3 m_dirctionAnglePower;

    // 制御するRigidBosy
    [SerializeField]
    private Rigidbody m_rgBody;
    //[SerializeField]
    //private Rigidbody m_rgRightHand;
    //[SerializeField]
    //private Rigidbody m_rgLeftHand;
    //[SerializeField]
    //private Rigidbody m_rgRightFoot;
    //[SerializeField]
    //private Rigidbody m_rgLeftFoot;

    // 共通の値
    [SerializeField]
    private PlayerMoving_ParamTable m_paramTable;
    [SerializeField]
    private JointAddForce[] m_jointAddForces = new JointAddForce[(int)PlayerExtendAndShrink.EShrinkPoint.Max-2];

    // Use this for initialization
    void Start () {
        // アタッチ確認
        if (m_rgBody == null)       Debug.LogError("m_rgBodyがアタッチされていません。");
        //if (m_rgRightHand == null)  Debug.LogError("m_rgRightHandがアタッチされていません。");
        //if (m_rgLeftHand == null)   Debug.LogError("m_rgLeftHandがアタッチされていません。");
        //if (m_rgRightFoot == null)  Debug.LogError("m_rgRightFootがアタッチされていません。");
        //if (m_rgLeftFoot == null)   Debug.LogError("m_rgLeftFootがアタッチされていません。");

        m_anglePower = m_paramTable.angleForce.initForce;
        m_movePower = m_paramTable.moveForce.initForce;
        m_jointAddForces[0].InitParameter(m_paramTable.handForce);
        m_jointAddForces[1].InitParameter(m_paramTable.handForce);
        m_dirctionAnglePower = m_paramTable.dirAngleForce.initForce;
        //m_handPower = m_paramTable.handForce.initForce;
        //m_footPower = m_paramTable.footForce.initForce;

    }

    // 移動
    public void Move(PlayerRay.RayDirection rayDirection , bool dir_right, float value = 1)
    {
        float dir = dir_right ? 1 : -1;

        // ワールド空間のベクトルに変換
        Vector3 worldAngulerVelocity = transform.TransformDirection(m_anglePower* value * dir);
        Vector3 worldMoveVelocity = transform.TransformDirection(m_movePower * value * dir);
       // Vector3 worldHandVelocity = transform.TransformDirection(m_handPower * value);
       // Vector3 worldFootVelocity = transform.TransformDirection(m_footPower * value);

        // 移動力と回転力を適用
        m_rgBody.angularVelocity = worldAngulerVelocity;
        m_rgBody.AddRelativeForce(worldMoveVelocity);

        if (dir_right) m_jointAddForces[0].Rotation(true);
        else m_jointAddForces[1].Rotation(true);


        // 手足の力を適用
        // うつ伏せ
        /*
        if (rayDirection == PlayerRay.RayDirection.Forward)
        {
            //m_rgRightHand.AddRelativeForce(worldHandVelocity, ForceMode.Force);
            //m_rgLeftHand.AddRelativeForce(worldHandVelocity, ForceMode.Force);
            //m_rgRightFoot.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            //m_rgLeftFoot.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            //m_jointAddForces[0].Rotation(true);
            //m_jointAddForces[1].Rotation(true);
            //m_jointAddForces[0].Rotation(true);
            //m_jointAddForces[0].Rotation(false);
        }
        // 仰向き
        else if (rayDirection == PlayerRay.RayDirection.Back)
        {
            //m_rgLeftHand.AddRelativeForce(-worldHandVelocity, ForceMode.Force);
            //m_rgRightHand.AddRelativeForce(-worldHandVelocity, ForceMode.Force);
            //m_rgRightFoot.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            //m_rgLeftFoot.AddRelativeForce(worldFootVelocity, ForceMode.Force);
            //m_jointAddForces[0].Rotation(true);
            //m_jointAddForces[1].Rotation(false);
        }*/

    }

    // 向き回転
    public void Rotation(bool dir_right, float value = 1)
    {
        float dir = dir_right ? 1 : -1;
        m_rgBody.AddTorque(m_dirctionAnglePower * dir * value);
    }

    // 力の減衰処理
    public void DecayForce()
    {
        m_anglePower = Mult(m_anglePower, m_paramTable.angleForce.decayForce);
        m_movePower = Mult(m_movePower, m_paramTable.moveForce.decayForce);
    }

    // 力の初期化
    public void Init()
    {
        m_anglePower = m_paramTable.angleForce.initForce;
        m_movePower  = m_paramTable.moveForce.initForce;
        //m_handPower  = m_paramTable.handForce.initForce;
        //m_footPower  = m_paramTable.footForce.initForce;
    }

    // Vector3の掛け算
    Vector3 Mult(Vector3 a , Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
