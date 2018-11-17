using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidFall : MonoBehaviour {

    [SerializeField]
    private GameController Gb;
    [Range(0, 60)]
    [SerializeField]
    private float SetTime = 0;
    private float NowTime = 0;
    private float gravitySpeed = -0.1f;
	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Gb.IsGameStart())
        {
            if (SetTime < NowTime)
            {
                transform.position += new Vector3(0, gravitySpeed, 0);
                gravitySpeed -= 0.008f;
            }
            NowTime += 1;
            if (this.transform.position.y <= -50)
            {
                Destroy(this.gameObject);
            }
        }


	}
}
