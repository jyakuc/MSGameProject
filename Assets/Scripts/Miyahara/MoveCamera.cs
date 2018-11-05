using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject SubCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            MainCamera.SetActive(!MainCamera.activeSelf);
            SubCamera.SetActive(!SubCamera.activeSelf);
        }	
	}
}
