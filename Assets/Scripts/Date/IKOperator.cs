using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKOperator : MonoBehaviour {

    private Vector3 m_oldPos;
    private Quaternion m_oldRota;
    private int frameCount;
    [SerializeField]
    private int saveFrameCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;
        if(saveFrameCount < frameCount)
        {
            m_oldPos = transform.position;
            m_oldRota = transform.rotation;
            frameCount = 0;
        }
	}

    private void OnTriggerExit(Collider other)
    {
 
        // IK制御オブジェクトが範囲外に出たとき元に戻す
        if (other.transform.tag == "IKRangeCollider")
        {
            Debug.Log("離れた");
            transform.position = m_oldPos;
            transform.rotation = m_oldRota;
        }
    }
}
