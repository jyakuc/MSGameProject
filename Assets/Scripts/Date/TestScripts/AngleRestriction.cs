using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleRestriction : MonoBehaviour {

    public Rigidbody rigidbody;
    public float restrictionMin;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.rotation.eulerAngles.z >= restrictionMin)
        {
            //transform.Rotate(0, 0, restrictionMin);
        }
	}
}
