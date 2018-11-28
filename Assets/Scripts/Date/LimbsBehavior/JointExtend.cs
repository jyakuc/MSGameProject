using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointExtend : MonoBehaviour {
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private GameObject shrinkObj;
    [SerializeField]
    private GameObject extendObj;

    private float shrinkPower;
    private float extendPower;

    private bool ExtendMaxflg = false;
    private Rigidbody parentRigid;
    private Rigidbody childRigid;

    private bool extendFlg;
    private bool shrinkFlg;

    // 手足の認識
    [SerializeField]
    [Header("Hand = true , Foot = false")]
    private bool isHandFlg;
    public bool IsHand { get { return isHandFlg; } }

    // Use this for initialization
    void Start () {
        parentRigid = transform.parent.GetComponent<Rigidbody>();
        childRigid = transform.GetChild(0).GetComponent<Rigidbody>();
	}
	
    private void FixedUpdate()
    {
        if (shrinkFlg)
        {
            Vector3 diff = shrinkObj.transform.position - transform.position;
            rigidbody.velocity = diff * shrinkPower;
        }

        if (extendFlg)
        {
            //VelocityExtend();
            AddForceImpulse();
            extendFlg = false;
        }
    }

    // Shrinkパラメータを設定
    public void SetShrinkParameters(float init,float max,float add)
    {
        shrinkPower = init;
    }
    // Extendパラメータを設定
    public void SetExtendParameters(float init, float max, float add)
    {
        extendPower = init;
    }

    // 縮む処理開始
    public void StartShrink()
    {
        shrinkFlg = true;
    }

    // 伸ばす処理開始
    public void StartExtend()
    {
        shrinkFlg = false;
        extendFlg = true;
    }

    /*
    // velocityを書き換えて伸ばす処理
    public void VelocityExtend()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            ExtendMaxflg = true;
        }

        if (ExtendMaxflg)
        {
            Vector3 diff = extendObj.transform.position - transform.position;
            rigidbody.velocity = diff * extendTime;
        }
    }
    */

    // AddForceのForceMode.Impulseで一度だけ力を加える処理
    public void AddForceImpulse()
    {
       
        Vector3 diff = extendObj.transform.position - transform.position;
        rigidbody.AddForce(diff * extendPower, ForceMode.Impulse);
    }
}
