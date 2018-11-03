using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour {

    private float gravitySpeed = -0.1f;
    private bool Fallflg;

    

	// Use this for initialization
	void Start () {
		Fallflg = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Fallflg)
        {
            transform.position += new Vector3(0, gravitySpeed, 0);
            gravitySpeed -= 0.008f;
        }
        if (this.transform.position.y <= -50)
        {
            Destroy(this.gameObject);
        }
	}
    public void FallFlgOn()
    {
        Fallflg = true;
    }
}
