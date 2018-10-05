using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("GameController_A1"))
        {
            Debug.Log ("A Button");
        }
        if (Input.GetButtonDown("GameController_B1"))
        {
            Debug.Log ("B Button");
        }
        if (Input.GetButtonDown("GameController_X1"))
        {
            Debug.Log ("X Button");
        }
        if (Input.GetButtonDown("GameController_Y1"))
        {
            Debug.Log ("Y Button");
        }

        //L Stick
        float lsh = Input.GetAxis ("GameController_Hori1");
        float lsv = Input.GetAxis ("GameController_Vert1");
        if(( lsh != 0) || (lsv != 0 )){
            Debug.Log ("GameController_Hori1:"+lsh+","+lsv );
        }
    }
}