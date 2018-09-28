using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanForce : MonoBehaviour {
    public GameObject humanModel;
    public Animator animator;
    public GameObject headEnd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
       
             headEnd.GetComponent<Rigidbody>().AddForce(0, 0, -5.0f);
        }	
	}
}
