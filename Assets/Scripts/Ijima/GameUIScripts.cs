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
        ColsetDigit();
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < 6; i++)
        {
            BattlePoints[i] = CManager.GetPlayerBattlePoint(i+1);
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
                if (TenPlace == j)
                {
                    if (HundredPlace == 0)
                    {
                        if (TenPlace != 0) {
                           if (i < 3) PannelTenPlace[i].texture = Images[j];   //左右判定
                           else PannelTenPlace[i].texture = Images[j + 10];
                           ColsetTen(i);
                        }
                    }
                    else
                    {
                        if (i < 3) PannelTenPlace[i].texture = Images[j];   //左右判定
                        else PannelTenPlace[i].texture = Images[j + 10];
                        ColsetTen(i);
                    }
                }
                //100の位
                if (HundredPlace != 0 && HundredPlace == j)
                {
                    if (i < 3) PannelHundredPlace[i].texture = Images[j];   //左右判定
                    else PannelHundredPlace[i].texture = Images[j + 10];
                    ColsetHundred(i);
                }
            }
        }
    }

    void ColsetDigit()
    {
        //red
        PannelDigit[0].color = new Color(255, 0, 0, 1f);
        //blue
        PannelDigit[1].color = new Color(0, 0, 100, 1f);
        //yellow
        PannelDigit[2].color = new Color(255, 200, 0, 1f);
        //green
        PannelDigit[3].color = new Color(0, 255, 0, 1f);
        //magenta
        PannelDigit[4].color = new Color(255, 0, 255, 1f);
        //cian
        PannelDigit[5].color = new Color(0, 255, 255, 1f);
    }
    void ColsetTen(int i)
    {
        if(i==0) PannelTenPlace[0].color = new Color(255, 0, 0, 1f);
        if(i==1) PannelTenPlace[1].color = new Color(0, 0, 100, 1f);
        if(i==2) PannelTenPlace[2].color = new Color(255, 200, 0, 1f);
        if(i==3) PannelTenPlace[3].color = new Color(0, 255, 0, 1f);
        if(i==4) PannelTenPlace[4].color = new Color(255, 0, 255, 1f);
        if(i==5) PannelTenPlace[5].color = new Color(0, 255, 255, 1f);
    }
    void ColsetHundred(int i)
    {
        if (i == 0) PannelHundredPlace[0].color = new Color(255, 0, 0, 1f);
        if (i == 1) PannelHundredPlace[1].color = new Color(0, 0, 100, 1f);
        if (i == 2) PannelHundredPlace[2].color = new Color(255, 200, 0, 1f);
        if (i == 3) PannelHundredPlace[3].color = new Color(0, 255, 0, 1f);
        if (i == 4) PannelHundredPlace[4].color = new Color(255, 0, 255, 1f);
        if (i == 5) PannelHundredPlace[5].color = new Color(0, 255, 255, 1f);
    }

}
