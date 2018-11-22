using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingDeadLine : MonoBehaviour {
    //プレイヤー座標
    public Transform[] PlayerData;
    //Stage座標
    public Transform[] StageData;
    //自分のオブジェクトのy座標を死亡Lineにする
    private Transform DeadLine;
    // Use this for initialization
    void Start () {
        DeadLine = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=1;i<=6;i++)
        {
            if(DeadCheck(i))
            {
                Respawn(i);
            }
        }
	}
    bool DeadCheck(int PlayerNum)
    {
        if(PlayerData[PlayerNum-1].position.y<=DeadLine.position.y)
        {
            return true;
        }
        return false;
    }
    void Respawn(int PlayerNum)
    {
        Vector3 RispawnPos = StageData[PlayerNum - 1].position;
        RispawnPos.y += 50;
        PlayerData[PlayerNum - 1].position = RispawnPos;
    }
}
