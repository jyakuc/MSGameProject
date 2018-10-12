using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddRelativeTorque(0.1f, 0, 0, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {

	}
}
