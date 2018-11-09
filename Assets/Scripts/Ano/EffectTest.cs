using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour {

    public GameObject Prefab;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Random.Range(-100, 100), 50, Random.Range(-100, 100));
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject NewObject;
            NewObject = (GameObject)Instantiate(Prefab, transform.position, Quaternion.identity);
            NewObject.GetComponent<Transform>().LookAt(this.transform);
        }
    }
}
