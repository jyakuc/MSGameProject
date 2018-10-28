using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCollision : MonoBehaviour {

    public List <int> Rank = new List<int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
            return;
        Debug.Log(other.gameObject.name);
        Rank.Add(other.gameObject.transform.root.GetComponent<PlayerController>().PlayerID);
        other.gameObject.transform.root.GetComponent<PlayerController>().Dead();
        Destroy(other.gameObject.transform.root.gameObject);

    }
}
