using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdSleepGimic : MonoBehaviour {

    private GameController Gb;
    private const int MaxWall = 7;
    private const int MaxFallObj = 3;
    private const int MaxInnerWall = 5;

    private GameObject ParentWall;
    private VanishWall[] ChildWall;

    private GameObject ParentIceFallpos;
    private FallFloor[] ChildIceFall;

    private GameObject ParentInnerWall;
    private VanishWall[] ChildInnerWall;


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
    private int InnerCount;

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
    [Range(0, 360)]
    [SerializeField]
    private float InnerSetTime;

    private float SaveTime;
    private float IceSaveTime;
    private float InnerSaveTime;


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
        InnerCount = 0;
        ParentWall = GameObject.Find("IceFieldAroundWalls");
        ChildWall = ParentWall.GetComponentsInChildren<VanishWall>();
        ParentIceFallpos = GameObject.Find("IceFieldFallObjs");
        ChildIceFall = ParentIceFallpos.GetComponentsInChildren<FallFloor>();
        ParentInnerWall = GameObject.Find("IceFieldInnerWalls");
        ChildInnerWall = ParentInnerWall.GetComponentsInChildren<VanishWall>();
        RandomFloor = UnityEngine.Random.Range(min, max);
        SaveTime = SetTime;
        IceSaveTime = IceFloorSetTime;
        InnerSaveTime = SetTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Gb.IsGameStart())
        {
            if (SetTime < NowTime && Count < MaxWall)
            {
                ChildWall[Count].OnVanish();
                Count += 1;
                SetTime = SaveTime * (Count + 1);
            }
            if (InnerSetTime < NowTime && InnerCount < MaxInnerWall)
            {
                ChildInnerWall[InnerCount].OnVanish();
                InnerCount += 1;
                InnerSetTime = InnerSaveTime * (InnerCount + 1);
            }
            if (IceFloorSetTime < NowTime && FloorCount < MaxFallObj)
            {
                ChildIceFall[FallList[RandomFloor, FloorCount]].OnFall();
                FloorCount += 1;
                IceFloorSetTime = IceSaveTime * (FloorCount + 1);
            }
            
            NowTime += Interval * Time.deltaTime;
        }

	}
}
