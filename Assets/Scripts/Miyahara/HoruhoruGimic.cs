using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoruhoruGimic : MonoBehaviour {

    private const int Maxpos = 8;
    

    private FallFloor[] ChildFloor;
    private GameObject[] MeteoPos = new GameObject[Maxpos];
    //private List<int> FallList = new List<int>;
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

    private int RandomFloor;
    private int min = 0;
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
    }
	
	// Update is called once per frame
	void Update () {
       
        
        if (Gb.StartFlg)
        {

            if (ChildFloor.Length == 1)
                return;
            if (SetTime < NowTime)
            {


                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                RandomFloor = UnityEngine.Random.Range(min, ChildFloor.Length);
                //RandomFloor = 3;
                    Effect = Instantiate(FireBall, MeteoPos[RandomFloor].transform.position,Quaternion.identity);
                    Effect.GetComponent<FireBall>().GroundNumber = RandomFloor;
                    NowTime = 0;
                
            }
            //Debug.Log(NowTime);
            NowTime += Interval * Time.deltaTime;
        }
        
	}
}
