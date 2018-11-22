using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingSystem : MonoBehaviour {
    public PlayerController[] PlayersController;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 6; i++)
        {
            PlayerIdleCheck(i);
        }
    }
    void PlayerIdleCheck(int Num)
    {
        if ((PlayersController[Num].GetMyState() == PlayerController.EState.Init)|| 
            (PlayersController[Num].GetMyState() == PlayerController.EState.Wait))
        {
            PlayersController[Num].PlayStart();
        }
    }
}
