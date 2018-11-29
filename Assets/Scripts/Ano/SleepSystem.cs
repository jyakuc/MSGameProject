using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSystem : MonoBehaviour {
    private StageCreate StageData;
    StageCreate.SelectingStage StageList;
	// Use this for initialization
	void Start () {
        try
        {
            StageData = GameObject.Find("StageManager").GetComponent<StageCreate>();
            StageList = StageData.Stages;
        }
        catch
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(StageList!= StageData.Stages)
        {
            Destroy(this.gameObject);
        }
	}
}
