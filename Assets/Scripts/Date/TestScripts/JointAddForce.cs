using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAddForce : MonoBehaviour {
    public Rigidbody rigidbody;
    public Vector3 vector;
    public ForceMode forceMode;
    public Transform axis;
    public GameObject shrinkObj;
    public GameObject extendObj;
    public float shrinkTime;
    public float extendTime;

    public enum AddForceType
    {
        AddForce,
        AddRelativeForce,
        AddExplosionForce
    }
    public AddForceType forceType;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vc = axis.TransformDirection(vector);
        Debug.Log(vc); 

        if (Input.GetKey(KeyCode.H))
        {
            switch (forceType)
            {
                case AddForceType.AddForce:
                    rigidbody.AddForce(vc, forceMode);
                    break;
                case AddForceType.AddRelativeForce:
                    rigidbody.AddRelativeForce(vc, forceMode);
                    break;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            Vector3 diff = shrinkObj.transform.position - transform.position;
            rigidbody.velocity = diff * shrinkTime;
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            Vector3 diff = extendObj.transform.position - transform.position;
            //rigidbody.velocity = diff * extendTime;
            rigidbody.AddForce(diff * extendTime, ForceMode.Impulse);
        }
    }
}
