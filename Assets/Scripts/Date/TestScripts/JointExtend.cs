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
    // Use this for initialization
    void Start () {
        parentRigid = transform.parent.GetComponent<Rigidbody>();
        childRigid = transform.GetChild(0).GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            Vector3 diff = shrinkObj.transform.position - transform.position;
            rigidbody.velocity = diff * shrinkTime;
            Debug.Log(diff);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
           ExtendMaxflg = true;
        }

        if (ExtendMaxflg)
        {
            Vector3 diff = extendObj.transform.position - transform.position;
            rigidbody.velocity = diff * extendTime;
        }

        //ArrivalExtendObj();
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
}
