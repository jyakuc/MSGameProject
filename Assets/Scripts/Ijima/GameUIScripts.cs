using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScripts : MonoBehaviour {

    public GameObject Canvas;
    public RawImage[] PannelDigit;
    public RawImage[] PannelTenPlace;
    public RawImage[] PannelHundredPlace;
    public Texture[] Images;
    private CostManager CManager;
    private int[] BattlePoints= new int[6]; //バトルポイントの格納配列



    // Use this for initialization
    void Start () {
        // CostManager取得
        CManager = FindObjectOfType<CostManager>();
        Colorset();
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < 6; i++)
        {
            BattlePoints[i] = CManager.GetPlayerBattlePoint(i+1);
            BattlePoints[i] = 123;
            int HundredPlace = (BattlePoints[i] / 100)%10;   //100の位
            int TenPlace = (BattlePoints[i] / 10)%10;        //10の位
            int Digit = BattlePoints[i] % 10;                //1の位
            Debug.Log("位確認"+HundredPlace.ToString() + TenPlace.ToString() + Digit.ToString());

            for (int j = 0; j < 10; j++)
            {
                if (Digit == j)
                {
                    if (i < 3) PannelDigit[i].texture = Images[j];          //左右判定
                    else PannelDigit[i].texture = Images[j + 10];
                }
                //10の位
                if (TenPlace != 0 && TenPlace == j)
                {
                    if (i < 3) PannelTenPlace[i].texture = Images[j];   //左右判定
                    else PannelTenPlace[i].texture = Images[j + 10];
                }
                //100の位
                if (HundredPlace != 0 && HundredPlace == j)
                {
                    if (i < 3) PannelHundredPlace[i].texture = Images[j];   //左右判定
                    else PannelHundredPlace[i].texture = Images[j + 10];
                }
               
            }
        }
    }

    void Colorset()
    {
        //red
        PannelDigit[0].color = new Color(255, 0, 0, 1f);
        PannelTenPlace[0].color = new Color(255, 0, 0, 1f);
        PannelHundredPlace[0].color = new Color(255, 0, 0, 1f);
        //blue
        PannelDigit[1].color = new Color(0, 0, 100, 1f);
        PannelTenPlace[1].color = new Color(0, 0, 100, 1f);
        PannelHundredPlace[1].color = new Color(0, 0, 100, 1f);
        //yellow
        PannelDigit[2].color = new Color(255, 200, 0, 1f);
        PannelTenPlace[2].color = new Color(255, 200, 0, 1f);
        PannelHundredPlace[2].color = new Color(255, 200, 0, 1f);
        //green
        PannelDigit[3].color = new Color(0, 255, 0, 1f);
        PannelTenPlace[3].color = new Color(0, 255, 0, 1f);
        PannelHundredPlace[3].color = new Color(0, 255, 0, 1f);
        //magenta
        PannelDigit[4].color = new Color(255, 0, 255, 1f);
        PannelTenPlace[4].color = new Color(255, 0, 255, 1f);
        PannelHundredPlace[4].color = new Color(255, 0, 255, 1f);
        //cian
        PannelDigit[5].color = new Color(0, 255, 255, 1f);
        PannelTenPlace[5].color = new Color(0, 255, 255, 1f);
        PannelHundredPlace[5].color = new Color(0, 255, 255, 1f);
    }
}
