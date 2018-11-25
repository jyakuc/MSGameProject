using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointExtend : MonoBehaviour {
    public Rigidbody rigidbody;
    public GameObject extendObj;
    public float extendTime;
    public float permissionValue;

    private bool flg = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.J))
        {


            flg = true;
        }

        if (flg)
        {
            Vector3 diff = extendObj.transform.position - transform.position;
            rigidbody.velocity = diff * extendTime;
            //rigidbody.AddForce(diff * extendTime, ForceMode.Impulse);
        }
        float distance = Vector3.Distance(extendObj.transform.position, transform.position);

        //ArrivalExtendObj();
            
        if (Input.GetKeyDown(KeyCode.K))
            flg = false;
    }

    public void ArrivalExtendObj()
    {
        if (!flg) return;

        flg = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        Debug.Log("伸びきった");

        
    }
}
