using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterRandamPos : MonoBehaviour {
    private Transform Trans;
    [Range(-5,0)]
    public float MinPos_X = 0;
    [Range(-5, 0)]
    public float MinPos_Y = 0;
    [Range(0, 5)]
    public float MaxPos_X = 0;
    [Range(0, 5)]
    public float MaxPos_Y = 0;
    private Vector2 RandamPos;
    public bool RandamMode = false;
    // Use this for initialization
    void Start () {
		if(RandamMode)
        {
            Trans = GetComponent<Transform>();
            Trans.position = new Vector3(Random.Range(MinPos_X, MaxPos_X), 0, Random.Range(MinPos_Y, MaxPos_Y));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
