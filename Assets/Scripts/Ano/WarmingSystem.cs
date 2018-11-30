using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmingSystem : MonoBehaviour {
    public enum EInput
    {
        Horizontal,
        Vertical,
        A, B, X, Y,
        Start,
        MAX,
    }
    public enum EInputButton
    {
        A, B, X, Y,
        MAX,
    }
    public struct Button
    {
        public bool stay;   // 押し続けてる
        public bool dowm;   // 押された瞬間
        public bool up;     // 離した瞬間
    };
    public PlayerController[] PlayersController;
    private MyInputManager MyInput;
    //オーディオ
    public AudioManager audio;
    private bool[] PlayerReadys = new bool[6];
    [Range(0,60)]
    public float NextSceneTimer=20;
    public float CountTimer = 0;
    bool NextFlag = false;
    private string[] InputName = new string[(int)EInput.MAX];
    // Use this for initialization
    void Start () {
        MyInput = FindObjectOfType<MyInputManager>();
        InputName[0] = "Horizontal_Player";
        InputName[1] = "Vertical_Player";
        InputName[2] = "A_Player";
        InputName[3] = "B_Player";
        InputName[4] = "X_Player";
        InputName[5] = "Y_Player";
        InputName[6] = "ST_Player";
        for (int i = 0; i < 6; i++) PlayerReadys[i] = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(!NextFlag)
        {
            NextSceneTimer -= Time.deltaTime;
            if (AllReadyCheck())
            {
                NextFlag = true;
                SceneController.GetInstance.ChangeScene("GameScene", 2);
            }
            //if ((NextSceneTimer <=0 ) ||(AllReadyCheck()))
            //{
            //    NextFlag = true;
            //    SceneController.GetInstance.ChangeScene("GameScene", 2);
            //}

        }

        for (int i = 0; i < 6; i++)
        {
            PlayerIdleCheck(i);
            ReadyInput(i);
        }


    }
    bool AllReadyCheck()
    {
        for(int i=0;i<6;i++)
        {
            if(!PlayerReadys[i])
            {
                return false;
            }
        }
        return true;
    }
    void ReadyInput(int Num)
    {
        if (Input.GetButtonDown(InputName[(int)EInput.Start] + MyInput.joysticks[Num].ToString()))
        {
            PlayerReadys[Num] = !PlayerReadys[Num];
            PlaysSe();
            Debug.Log("押されたよ" + MyInput.joysticks[Num]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1+ Num))
        {
            PlayerReadys[Num] = !PlayerReadys[Num];
            PlaysSe();
            Debug.Log("押されたよ" + MyInput.joysticks[Num]);
        }
    }
    public bool GetReadyFlag(int PlayerNum)
    {
        return PlayerReadys[PlayerNum];
    }
    void PlayerIdleCheck(int Num)
    {
        if ((PlayersController[Num].GetMyState() == PlayerController.EState.Init)|| 
            (PlayersController[Num].GetMyState() == PlayerController.EState.Wait))
        {
            PlayersController[Num].PlayStart();
        }
        //もしReadyになって止めるならここに書く
    }
    //SE再生処理
    private void PlaysSe()
    {
        //仮に使っているので新しいSEが届き次第変更
        AudioManager.GetInstance.PlaySE0(AUDIO.SE_Hit01);
    }
}
