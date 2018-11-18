using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ResultScripts : MonoBehaviour {

    public GameObject[] Human = new GameObject[6];
    public RawImage[] PannelDigit;
    public RawImage[] PannelTenPlace;
    public RawImage[] PannelHundredPlace;
    public Texture[] Images;
    private CostManager CManager;
    public int SceneChangeTime;     //遅延フレーム数
    private bool SceneChangeFlg;
    private int flame;
    private bool OneKeyFlag = false;
    private bool ResultFinishFlg;
    private int[] ArtisticPoint = new int[6]; //アートポイントの格納配列
    private int[] BattlePoint = new int[6]; //バトルポイントの格納配列
    private int[] TotalPoint = new int[6]; //バトルポイントの格納配列
    private int VictoryNum; //yuusyo
    private Color[] SetColors = new Color[7];

    private void Start()
    {
        CManager = FindObjectOfType<CostManager>();
        Init();
        SceneChangeFlg = false;
        ResultFinishFlg = false;
        flame = 0;
        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_TITLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
        VictoryCharSet();
    }

    void Update()
    {
        //仮置き1000フレ後入力受付
        if (ResultFinishFlg == false)
        {
            flame++;
            if (flame >= 1000)
            {
                ResultFinishFlg = true;
                flame = 0;
            }
        }
        ///////////
        if ((Input.anyKeyDown == true) && (!OneKeyFlag) && (ResultFinishFlg == true))
        {
            SceneChangeFlg = true;
            OneKeyFlag = true;
            AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
        }
        if (SceneChangeFlg == true)
        {
            flame++;
            if (flame == SceneChangeTime)
            {
                SceneController.GetInstance.ChangeScene("TitleScene", 2);
            }
        }
    }

    //勝利プレイヤー判定と点数格納
    void Init()
    {
        int max=0;
        for (int i = 0; i < 6; i++)
        {
            ArtisticPoint[i] = CManager.GetPlayerArtPoint(i+1);
            BattlePoint[i] = CManager.GetPlayerBattlePoint(i+1);
            TotalPoint[i] = ArtisticPoint[i] + BattlePoint[i];


            TotalPoint[i] = ArtisticPoint[i] + BattlePoint[i];

            //1位のプレイヤー判定
            if (TotalPoint[i] >= max)
            {
                max = TotalPoint[i];
                VictoryNum = i;
            }
        }
    }
    void VictoryCharSet()
    {
        //勝利キャラの表示
        Human[VictoryNum].SetActive(true);
        PointColorSet();
        PointSet();
    }

    void PointColorSet()
    {
        //red
        SetColors[0] = new Color(255, 0, 0, 1f);
        //blue
        SetColors[1] = new Color(0, 0, 100, 1f);
        //yellow
        SetColors[2] = new Color(255, 200, 0, 1f);
        //green
        SetColors[3] = new Color(0, 255, 0, 1f);
        //magenta
        SetColors[4] = new Color(255, 0, 255, 1f);
        //cian
        SetColors[5] = new Color(0, 255, 255, 1f);
        //alfa0
        SetColors[6] = new Color(255, 255, 255, 0);

        for (int i = 0; i < 3; i++){
            PannelDigit[i].color = SetColors[VictoryNum];
            PannelTenPlace[i].color = SetColors[VictoryNum];
            PannelHundredPlace[i].color = SetColors[VictoryNum];
        }
        PannelDigit[3].color = SetColors[VictoryNum];
    }

    void PointSet()
    {
        //プレイヤーナンバー
        PannelDigit[3].texture = Images[VictoryNum + 11];

        int bHundredPlace = (BattlePoint[VictoryNum] / 100) % 10;   //100の位
        int bTenPlace = (BattlePoint[VictoryNum] / 10) % 10;        //10の位
        int bDigit = BattlePoint[VictoryNum] % 10;                //1の位
        int aHundredPlace = (ArtisticPoint[VictoryNum] / 100) % 10;   //100の位
        int aTenPlace = (ArtisticPoint[VictoryNum] / 10) % 10;        //10の位
        int aDigit = ArtisticPoint[VictoryNum] % 10;                //1の位
        int tHundredPlace = (TotalPoint[VictoryNum] / 100) % 10;   //100の位
        int tTenPlace = (TotalPoint[VictoryNum] / 10) % 10;        //10の位
        int tDigit = TotalPoint[VictoryNum] % 10;                //1の位

        //点数のセット
        for (int j = 0; j < 10; j++)
        {
            //total
            if (tDigit == j)
            {
                PannelDigit[0].texture = Images[j];
            }
            //10の位
            if (tTenPlace == j)
            {
                if (tHundredPlace == 0)
                {
                    if (tTenPlace != 0) PannelTenPlace[0].texture = Images[j];
                    else PannelTenPlace[0].color = SetColors[6];
                }
                else
                {
                    PannelTenPlace[0].texture = Images[j];
                }
            }
            //100の位
            if (tHundredPlace != 0 && tHundredPlace == j)
            {
                PannelHundredPlace[0].texture = Images[j];
            }
            else
            {
                PannelHundredPlace[0].color = SetColors[6];//桁消去
            }

            //battle
            if (bDigit == j)
            {
                PannelDigit[1].texture = Images[j + 10];          //左右判定
            }
            //10の位
            if (bTenPlace == j)
            {
                if (bHundredPlace == 0)
                {
                    if (bTenPlace != 0) PannelTenPlace[1].texture = Images[j+10];   //左右判定
                    else PannelTenPlace[1].color = SetColors[6]; //桁消去
                }
                else
                {
                    PannelTenPlace[1].texture = Images[j+10];   //左右判定
                }
            }
            //100の位
            if (bHundredPlace != 0 && bHundredPlace == j)
            {
                PannelHundredPlace[1].texture = Images[j+10];   //左右判定
            }
            else
            {
                PannelHundredPlace[1].color = SetColors[6];//桁消去
            }

            //Art
            if (aDigit == j)
            {
                PannelDigit[2].texture = Images[j + 10];          //左右判定
            }
            //10の位
            if (aTenPlace == j)
            {
                if (aHundredPlace == 0)
                {
                    if (aTenPlace != 0) PannelTenPlace[2].texture = Images[j + 10];   //左右判定
                    else PannelTenPlace[2].color = SetColors[6]; //桁消去
                }
                else
                {
                    PannelTenPlace[2].texture = Images[j + 10];   //左右判定
                }
            }
            //100の位
            if (aHundredPlace != 0 && bHundredPlace == j)
            {
                PannelHundredPlace[2].texture = Images[j + 10];   //左右判定
            }
            else
            {
                PannelHundredPlace[2].color = SetColors[6];//桁消去
            }

        }
    }
}
