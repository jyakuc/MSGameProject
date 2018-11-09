using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoruhoruGimic : MonoBehaviour {

    private FallFloor[] ChildFloor;
    
    private GameObject Floor;

    private float NowTime = 0;
    [Range(0,120)]
    [SerializeField]
    private float SetTime = 0;
    [SerializeField]
    private GameController Gb;

    private int RandomFloor;
    private int min = 0;
	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Floor = GameObject.Find("MountainStage");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Floor);
        
        if (Gb.StartFlg)
        {
            
            if (SetTime < NowTime)
            {
               ChildFloor = Floor.GetComponentsInChildren<FallFloor>();
                   if (ChildFloor.Length == 0)
                       return;
                       RandomFloor = UnityEngine.Random.Range(min,ChildFloor.Length);
                       ChildFloor[RandomFloor].FallFlgOn();
                       Debug.Log(RandomFloor);
                       NowTime = 0;
            }
            NowTime += 1;
        }
        
	}
}
