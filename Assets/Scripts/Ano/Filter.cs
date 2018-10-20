using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour {
    //サイズ変更適用Transform
    Transform Trans;
    //サイズ減少用変数
    [Range(0, 20)]
    public float Speed = 0;
    //サイズ限界値変数
    [Range(0, 5000)]
    public float LimitSize = 0;
    // Use this for initialization
    void Start () {
        Trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Trans.localScale.x<= LimitSize)
        {
            Trans.localScale = new Vector3(LimitSize, LimitSize, Trans.localScale.z);
        }
        else
        {
            Trans.localScale = new Vector3(Trans.localScale.x-Speed, Trans.localScale.y - Speed, Trans.localScale.z);

        }
    }
}
