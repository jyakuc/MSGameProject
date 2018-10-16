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
        if (Input.GetKey(KeyCode.A) || lsh < 0)
        {
            Debug.Log("left move");
        }
        if (Input.GetKey(KeyCode.D) || lsh > 0)
        {
            Debug.Log("right move");
        }
        if (Input.GetKey(KeyCode.W) || lsv > 0)
        {
            Debug.Log("advance move");
        }
        if (Input.GetKey(KeyCode.S) || lsv < 0)
        {
            Debug.Log("back move");
        }
        if(( lsh != 0) || (lsv != 0 )){
            Debug.Log ("GameController_Hori1:"+lsh+","+lsv );
        }

        if (Input.GetKey(KeyCode.I) || lsh < 0)
        {
            Debug.Log("push I");
        }
        if (Input.GetKey(KeyCode.O) || lsh > 0)
        {
            Debug.Log("push O");
        }
        if (Input.GetKey(KeyCode.K) || lsv > 0)
        {
            Debug.Log("push K");
        }
        if (Input.GetKey(KeyCode.L) || lsv < 0)
        {
            Debug.Log("push L");
        }


        if (Input.GetKey(KeyCode.LeftArrow) || lsh < 0)
        {
            Debug.Log("left move");
        }
        if (Input.GetKey(KeyCode.RightArrow) || lsh > 0)
        {
            Debug.Log("right move");
        }
        if (Input.GetKey(KeyCode.UpArrow) || lsv > 0)
        {
            Debug.Log("advance move");
        }
        if (Input.GetKey(KeyCode.DownArrow) || lsv < 0)
        {
            Debug.Log("back move");
        }
    }
}