using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdSleepGimic : MonoBehaviour {

    private GameController Gb;
    private const int MaxWall = 7;
    private const int MaxFallObj = 3;
    private const int MaxInnerWall = 5;
    private const int MaxGroundFall = 4;

    private GameObject ParentWall;
    private VanishWall[] ChildWall;

    private GameObject ParentIceFallpos;
    private VanishWall[] ChildIceFall;

    private GameObject ParentInnerWall;
    private VanishWall[] ChildInnerWall;

    private GameObject ParentGroundFall;
    private VanishWall[] ChildGroundWall;

    private int[,] FallList = 
    {
        {0,1,2},
        {0,2,1},
        {1,0,2},
        {1,2,0},
        {2,0,1},
        {2,1,0}
    };
    private int[,] GroundVanishList = 
    {
        {0,1},
        {0,2},
        {1,3},
        {2,3}
    };
    private int Count;
    private int FloorCount;
    private int InnerCount;
    private int GroundCount;


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

    private int RandomGroundFloor;
    private int minGround = 0;
    private int maxGround = 2;

	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Count = 0;
        FloorCount = 0;
        InnerCount = 0;
        GroundCount = 0;


        ParentWall = GameObject.Find("IceFieldAroundWalls");
        ChildWall = ParentWall.GetComponentsInChildren<VanishWall>();
        ParentIceFallpos = GameObject.Find("IceFieldFallObjs");
        ChildIceFall = ParentIceFallpos.GetComponentsInChildren<VanishWall>();
        ParentInnerWall = GameObject.Find("IceFieldInnerWalls");
        ChildInnerWall = ParentInnerWall.GetComponentsInChildren<VanishWall>();
        ParentGroundFall = GameObject.Find("IceFields");
        ChildGroundWall = ParentGroundFall.GetComponentsInChildren<VanishWall>();


        RandomFloor = UnityEngine.Random.Range(min, max);
        RandomGroundFloor = UnityEngine.Random.Range(minGround, maxGround);

        SaveTime = SetTime;
        IceSaveTime = IceFloorSetTime;
        InnerSaveTime = InnerSetTime;
        
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
                ChildIceFall[FallList[RandomFloor, FloorCount]].OnVanish();
                FloorCount += 1;
                IceFloorSetTime = IceSaveTime * (FloorCount + 1);
            }
            if (ChildWall[6] == null && GroundCount < 2)
            {
                ChildGroundWall[GroundVanishList[RandomGroundFloor, GroundCount]].OnVanish();
                GroundCount += 1;
            }

            NowTime += Interval * Time.deltaTime;
        }

	}
}
