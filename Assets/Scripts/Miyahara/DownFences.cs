using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownFences : MonoBehaviour {

    private bool gravityFlg;
    private float gravitySpeed = -0.1f;
    [SerializeField]
    private GameController Gb;

	// Use this for initialization
	void Start () {
        gravityFlg = false;
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Gb.StartFlg)
        {
            gravityFlg = true;
        }
        if (gravityFlg)
        {
            transform.position += new Vector3(0, gravitySpeed, 0);
            gravitySpeed -= 0.008f;
        }
        if (this.transform.position.y <= -50)
        {
            Destroy(this.gameObject);
        }
	}
}
