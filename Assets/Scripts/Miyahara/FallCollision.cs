using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour {

    private const int MaxArray = 6;
    public int[] Rank = new int[MaxArray];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
            Debug.Log(other.gameObject.name);
            Rank[Rank.Length]= other.gameObject.GetComponent<PlayerController>().PlayerID;
            Destroy(other.gameObject);
            

    }
}
