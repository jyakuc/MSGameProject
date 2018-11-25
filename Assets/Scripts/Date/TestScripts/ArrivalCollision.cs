using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalCollision : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        JointExtend otherJoint = other.GetComponent<JointExtend>();
        if (otherJoint == null) return;
        otherJoint.ArrivalExtendObj();
    }
}
