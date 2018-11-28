using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepEffectCreater : MonoBehaviour {
    public GameObject Prefab;
    private GameObject CreateObject;
	// Use this for initialization
	void Start () {
        CreateObject = (GameObject)Instantiate(Prefab, this.transform.position, Quaternion.identity);

    }
	
	// Update is called once per frame
	void Update () {
        CreateObject.transform.position = this.transform.position;
	}
}
