using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScripts : MonoBehaviour {

    public GameObject Canvas;
    public GameObject uiPlayer;
    public GameObject uiRank;
    public RawImage[] PannelDigit;
    public RawImage[] PannelTenPlace;
    public RawImage[] PannelHundredPlace;
    public Texture[] Images;
    private CostManager CManager;
    private int[] BattlePoints= new int[6]; //バトルポイントの格納配列



    // Use this for initialization
    void Start () {
        //PlayerUIの生成
        GameObject pPlayerUI = (GameObject)Instantiate(uiPlayer);
        pPlayerUI.transform.SetParent(Canvas.transform, false);
        //RankingUIの生成
        GameObject pRankUI = (GameObject)Instantiate(uiRank);
        pRankUI.transform.SetParent(Canvas.transform, false);

        // CostManager取得
        CManager = FindObjectOfType<CostManager>();
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < 6; i++)
        {
            BattlePoints[i] = CManager.GetPlayerBattlePoint(i+1);
            int HundredPlace = (BattlePoints[i] / 100)%10;   //100の位
            int TenPlace = (BattlePoints[i] / 10)%10;        //10の位
            int Digit = BattlePoints[i] % 10;           //1の位
            Debug.Log("位確認"+HundredPlace.ToString() + TenPlace.ToString() + Digit.ToString());

            for (int j = 0; j < 10; j++)
            {
                if (Digit == j)
                {
                    if (i < 3) PannelDigit[i].texture = Images[j];          //左右判定
                    else PannelDigit[i].texture = Images[j + 10];
                    PannelDigit[i].color = new Color(255, 255, 255, 1f);
                }
                //10の位
                if (TenPlace != 0 && TenPlace == j)
                {
                    if (i < 3) PannelTenPlace[i].texture = Images[j];   //左右判定
                    else PannelTenPlace[i].texture = Images[j + 10];
                    PannelTenPlace[i].color = new Color(255, 255, 255, 1f);
                }
                //100の位
                if (HundredPlace != 0 && HundredPlace == j)
                {
                    if (i < 3) PannelHundredPlace[i].texture = Images[j];   //左右判定
                    else PannelHundredPlace[i].texture = Images[j + 10];
                    PannelHundredPlace[i].color = new Color(255, 255, 255, 1f);
                }
               
            }
        }
    }

}
