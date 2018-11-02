using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoruhoruGimic : MonoBehaviour {

    private const int MaxFloor = 7;

    private GameObject Floor;
    public Transform[] ChildFloor = new Transform[MaxFloor];

    private float SetStartTime;
    [Range(0,30)]
    [SerializeField]
    private float StartTime = 0;
    [SerializeField]
    private GameController Gb;

    private bool FallFlg;

    private int Random;
    private int min = 0;
    private int max = 8;
	// Use this for initialization
	void Start () {
        if (Gb == null)
        {
            Gb = FindObjectOfType<GameController>();
        }
        Floor = GameObject.Find("MountainStage");
        Floor.GetComponentInChildren<Transform>();
        for (int i = 0; i < MaxFloor; i++)
        {
            ChildFloor[i] = Floor.transform.GetChild(i);
        }
        SetStartTime = StartTime;
        Random = UnityEngine.Random.Range(min, max);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Floor);
        if (Gb.StartFlg)
        {
            if (Time.time > StartTime)
            {
                if (ChildFloor[Random])
                {
                    for (int i = 0; i < MaxFloor; i++)
                    {
                        if (ChildFloor[i] == null)
                            continue;
                        ChildFloor[i].GetComponent<FallFloor>().FallFlgOn();
                        StartTime = SetStartTime;
                    }
                }
                
            }
        }
        
	}
}
