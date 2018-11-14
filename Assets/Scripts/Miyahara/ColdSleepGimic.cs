using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdSleepGimic : MonoBehaviour {

    private GameController Gb;
    private const int MaxWall = 6;
    private GameObject ParentWall;
    private VanishWall[] ChildWall;

    //private int[,] WallList = 
    //{
    //    {0,1,2,3,4,5}               
    //};
    private int Count;

    private float NowTime = 0;
    [Range(0, 360)]
    [SerializeField]
    private float Interval;
    [Range(0, 360)]
    [SerializeField]
    private float SetTime;

    

	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Count = 0;
        ParentWall = GameObject.Find("IceFieldAroundWalls");
        ChildWall = ParentWall.GetComponentsInChildren<VanishWall>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Gb.StartFlg)
        {
            if (Count >= MaxWall)
                return;
            if (SetTime < NowTime)
            {
                ChildWall[Count].OnVanish();
                NowTime = 0;
                Count += 1;
            }
            NowTime += Interval * Time.deltaTime;
        }

	}
}
