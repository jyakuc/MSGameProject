using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAddForce : MonoBehaviour {
    public Rigidbody rigidbody;
    public Vector3 vector;
    public ForceMode forceMode;
    public Transform axis;
    public GameObject extendObj;

    public float time;
    private bool flg = false;
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
        //rigidbody.
        if (Input.GetKeyDown(KeyCode.G))
        {
            switch (forceType)
            {
                case AddForceType.AddForce:
                    rigidbody.AddForce(vc, forceMode);
                    break;
                case AddForceType.AddRelativeForce:
                    rigidbody.AddRelativeForce(vc, forceMode);
                    break;
                case AddForceType.AddExplosionForce:
                    //rigidbody.AddExplosionForce(vector, forceMode);
                    break;
            }
        }

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
                case AddForceType.AddExplosionForce:
                    //rigidbody.AddExplosionForce(vector, forceMode);
                    break;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            /*
            if (!flg)
            {
                rigidbody.AddForce(vc, ForceMode.Impulse);
                flg = true;
            }
            */
            transform.position = Vector3.Slerp(transform.position, extendObj.transform.position,Time.deltaTime * time);
           // time += Time.deltaTime;
        }
    }
}
