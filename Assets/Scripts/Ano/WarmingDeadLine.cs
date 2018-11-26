using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingDeadLine : MonoBehaviour {
    //プレイヤー座標
    public GameObject[] PlayerData;
    //Stage座標
    public Transform[] StageData;
    public GameObject SandPrefab;
    //サンドバック座標
    public GameObject[] SandData=new GameObject[6];
    //自分のオブジェクトのy座標を死亡Lineにする
    private Transform DeadLine;
    // Use this for initialization
    void Start () {
        DeadLine = GetComponent<Transform>();
        for(int i=0;i<6;i++)
        {
            SandBackRespawn(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=1;i<=6;i++)
        {
            if(DeadCheck(i))
            {
                Respawn(i);
            }
            if(SandBackDeadCheck(i))
            {
                SandBackRespawn(i-1);
            }
        }
	}
    bool DeadCheck(int PlayerNum)
    {
        if(PlayerData[PlayerNum-1].transform.position.y<=DeadLine.position.y)
        {
            return true;
        }
        return false;
    }
    void Respawn(int PlayerNum)
    {
        Vector3 RispawnPos = StageData[PlayerNum - 1].position;
        RispawnPos.y += 50;
        PlayerData[PlayerNum - 1].transform.position = RispawnPos;
        PlayerData[PlayerNum - 1].GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    bool SandBackDeadCheck(int PlayerNum)
    {
        if (SandData[PlayerNum - 1].transform.position.y <= DeadLine.position.y)
        {
            Destroy(SandData[PlayerNum - 1]);
            SandData[PlayerNum - 1] = null;
            return true;
        }
        return false;
    }
    void SandBackRespawn(int PlayerNum)
    {
        if(SandData[PlayerNum]==null)
        {
            switch (PlayerNum)
            {
                case 0:
                    SandData[PlayerNum] = (GameObject)Instantiate(SandPrefab, new Vector3(Random.Range(-257.0f,-243.0f), 5.0f, 50.0f), SandPrefab.transform.rotation);
                    break;
                case 1:
                    SandData[PlayerNum] = (GameObject)Instantiate(SandPrefab, new Vector3(Random.Range(-7.0f,7.0f), 5.0f, 50.0f), SandPrefab.transform.rotation);
                    break;
                case 2:
                    SandData[PlayerNum] = (GameObject)Instantiate(SandPrefab, new Vector3(Random.Range(243.0f,257.0f), 5.0f, 50.0f), SandPrefab.transform.rotation);
                    break;
                case 3:
                    SandData[PlayerNum] = (GameObject)Instantiate(SandPrefab, new Vector3(Random.Range(-257.0f, -243.0f), 5.0f, -250.0f), SandPrefab.transform.rotation);
                    break;
                case 4:
                    SandData[PlayerNum] = (GameObject)Instantiate(SandPrefab, new Vector3(Random.Range(-7.0f, 7.0f), 5.0f, -250.0f), SandPrefab.transform.rotation);
                    break;
                case 5:
                    SandData[PlayerNum] = (GameObject)Instantiate(SandPrefab, new Vector3(Random.Range(257.0f, 243.0f), 5.0f, -250.0f), SandPrefab.transform.rotation);
                    break;
            }
        }
        //Vector3 RispawnPos = StageData[PlayerNum - 1].position;
        //Rigidbody Rigid = SandData[PlayerNum - 1].GetComponent<Rigidbody>();
        //Rigid.velocity = Vector3.zero;
        //RispawnPos.y += 50;
        //SandData[PlayerNum - 1].transform.position = RispawnPos;
    }
}
