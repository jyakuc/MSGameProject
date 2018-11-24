using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointAddForce : MonoBehaviour {
    public Rigidbody rigidbody;
    public Vector3 vector;
    public ForceMode forceMode;
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            switch (forceType)
            {
                case AddForceType.AddForce:
                    rigidbody.AddForce(vector, forceMode);
                    break;
                case AddForceType.AddRelativeForce:
                    rigidbody.AddRelativeForce(vector, forceMode);
                    break;
                case AddForceType.AddExplosionForce:
                    //rigidbody.AddExplosionForce(vector, forceMode);
                    break;
            }
        }
	}
}
