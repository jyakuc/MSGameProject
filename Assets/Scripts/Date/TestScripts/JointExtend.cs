using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointExtend : MonoBehaviour {
    public Rigidbody rigidbody;
    public GameObject extendObj;
    public float extendTime;

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

        if (Input.GetKeyDown(KeyCode.K))
            flg = false;
    }
}
