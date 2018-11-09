using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCreateTest : MonoBehaviour {
    public GameObject Prefab;
    float NowTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject CreateObject;
        this.transform.position=new Vector3(Random.Range(-30,30),20, Random.Range(-30, 30));
        NowTime += Time.deltaTime;
        if (NowTime>=2)
        {
            CreateObject = (GameObject)Instantiate(Prefab, transform.position, Quaternion.identity);
            CreateObject.GetComponent<Transform>().LookAt(this.transform.position);
            NowTime = 0;
        }

    }
}
