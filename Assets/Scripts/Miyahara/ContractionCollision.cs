using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractionCollision : MonoBehaviour {

    private float x, y, z;  //収縮オブジェクトのXYZ取得



	// Use this for initialization
	void Start () {
        x = 40.0f;
        y = 2.0f;
        z = 40.0f;
	}
	
	// Update is called once per frame
	void Update () {
        
        x += -0.05f;
        //y += -0.01f;
        z += -0.05f;

        if (x >= 2 && y >= 2 && z >= 2)
        {
            this.transform.localScale = new Vector3(x, 2, z);
        }
		
	}
}
