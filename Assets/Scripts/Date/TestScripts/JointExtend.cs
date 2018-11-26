using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointExtend : MonoBehaviour {
    public Rigidbody rigidbody;
    public GameObject shrinkObj;
    public GameObject extendObj;
    public float shrinkTime;
    public float extendTime;

    private bool ExtendMaxflg = false;
    private Rigidbody parentRigid;
    private Rigidbody childRigid;

    private bool extendFlg;
    private bool shrinkFlg;
    // Use this for initialization
    void Start () {
        parentRigid = transform.parent.GetComponent<Rigidbody>();
        childRigid = transform.GetChild(0).GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.J))
        {
            shrinkFlg = true;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            shrinkFlg = false;
            extendFlg = true;
        }
    }

    private void FixedUpdate()
    {
        if (shrinkFlg)
        {
            Vector3 diff = shrinkObj.transform.position - transform.position;
            rigidbody.velocity = diff * shrinkTime;
        }

        if (extendFlg)
        {
            //VelocityExtend();
            AddForceImpulse();
            extendFlg = false;
        }
    }

    public void ArrivalExtendObj()
    {
        if (!ExtendMaxflg) return;
        ExtendMaxflg = false;

        // バウンドを防ぐ
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        parentRigid.velocity = Vector3.zero;
        parentRigid.angularVelocity = Vector3.zero;
        childRigid.velocity = Vector3.zero;
        childRigid.angularVelocity = Vector3.zero;

        Debug.Log("伸びきった");
    }

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

    // AddForceのForceMode.Impulseで一度だけ力を加える処理
    public void AddForceImpulse()
    {
       
        Vector3 diff = extendObj.transform.position - transform.position;
        rigidbody.AddForce(diff * extendTime , ForceMode.Impulse);
        Debug.Log(diff);
        Debug.Log("離した");
    }
}
