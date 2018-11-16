using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdSleepGimic : MonoBehaviour {

    private GameController Gb;
    private const int MaxWall = 6;
    private const int MaxFallObj = 3;
    private GameObject ParentWall;
    private VanishWall[] ChildWall;

    private GameObject ParentIceFallpos;
    private FallFloor[] ChildIceFall;


    private int[,] FallList = 
    {
        {0,1,2},
        {0,2,1},
        {1,0,2},
        {1,2,0},
        {2,0,1},
        {2,1,0}
    };
    private int Count;
    private int FloorCount;

    private float NowTime = 0;
    [Range(0, 360)]
    [SerializeField]
    private float Interval;
    [Range(0, 360)]
    [SerializeField]
    private float SetTime;
    [Range(0, 360)]
    [SerializeField]
    private float IceFloorSetTime;

    private int RandomFloor;
    private int min = 0;
    private int max = 3;

	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Count = 0;
        FloorCount = 0;
        ParentWall = GameObject.Find("IceFieldAroundWalls");
        ChildWall = ParentWall.GetComponentsInChildren<VanishWall>();
        ParentIceFallpos = GameObject.Find("IceFieldFallObjs");
        ChildIceFall = ParentIceFallpos.GetComponentsInChildren<FallFloor>();
        RandomFloor = UnityEngine.Random.Range(min, max);
	}
	
	// Update is called once per frame
	void Update () {
        if (Gb.StartFlg)
        {
            if (SetTime < NowTime && Count < MaxWall)
            {
                ChildWall[Count].OnVanish();
                Count += 1;
                SetTime *= Count + 1;
            }
            if (IceFloorSetTime < NowTime && FloorCount < MaxFallObj)
            {
                ChildIceFall[FallList[RandomFloor, FloorCount]].OnFall();
                FloorCount += 1;
                IceFloorSetTime *= FloorCount + 1;
            }
            NowTime += Interval * Time.deltaTime;
        }

	}
}
