using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoruhoruGimic : MonoBehaviour {

    private const int Maxpos = 8;

    private const int MaxPattern = 10;

    private FallFloor[] ChildFloor;
    private GameObject[] MeteoPos = new GameObject[Maxpos];
    private int[,] FallList =
    {
        {0,1,2,3,4,5,6 },  //順になくなる
        {7,6,5,4,3,2,1 },
        {1,4,7,6,3,0,2 },  //縦に消える
        {5,2,0,3,6,7,4 },
        {0,5,1,7,2,4,6 },  //真ん中が残る
        {5,2,0,1,4,7,6 },
        {0,5,7,1,6,4,3 },  //端が残る
        {1,5,0,7,6,2,3 },
        {3,4,7,1,0,2,5 },  //真ん中が即消える
        {3,0,1,4,7,6,5 }
    };
    private int Count;

    private GameObject Floor;
    [SerializeField]
    public GameObject FireBall;

    private GameObject Effect;

    private float NowTime = 0;
    [Range(0, 360)]
    [SerializeField]
    private float Interval;
    [Range(0, 360)]
    [SerializeField]
    private float SetTime;
    
    private GameController Gb;

    private int RandomList;
    private int min = 0;
    private int max = 9;
    // Use this for initialization
    
    void Start () {
        GameObject ParentMeteopos = GameObject.Find("MeteoPositions");
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Floor = GameObject.Find("MountainStage");
        Effect = null;
        for(int i = 0; i < Maxpos; i++)
        {
            MeteoPos[i] = ParentMeteopos.transform.GetChild(i).gameObject;
        }
        ChildFloor = Floor.GetComponentsInChildren<FallFloor>();
        Count = 0;
        RandomList = UnityEngine.Random.Range(min, max);
    }
	
	// Update is called once per frame
	void Update () {
       
        
        if (Gb.StartFlg)
        {

            if (Count >= 7)
                return;
            if (SetTime < NowTime)
            {

                Effect = Instantiate(FireBall, MeteoPos[FallList[RandomList,Count]].transform.position,Quaternion.identity);
                Effect.GetComponent<FireBall>().GroundNumber = FallList[RandomList, Count];
                NowTime = 0;
                Count += 1;
            }
            //Debug.Log(NowTime);
            NowTime += Interval * Time.deltaTime;
        }
        
	}
}
