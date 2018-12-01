using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadScript : MonoBehaviour
{

    public RawImage[] PannelDigit;
    public RawImage[] PannelTenPlace;
    public RawImage[] PannelHundredPlace;
    public RawImage[] PannelThousandPlace;
    public Image[] PlayerNumber;
    public Image Startbutton;
    public Texture[] Images;
    public Sprite[] ImagesNumber;
    public Sprite[] Sbutton;
    private CostManager CManager;
    private int[] BattlePoint = new int[6]; //バトルポイントの格納配列
    private int[] Rank = new int[6];
    private Color[] SetColors = new Color[7];
    private int flame;
    private int now_s;

    private void Start()
    {
        CManager = FindObjectOfType<CostManager>();
 //       Init();
        flame = 0;
        now_s = 0;

 //       PointColorSet();
 //       RankSet();
 //       PointSet();
    }

    void Update()
    {
        flame++;
        if (flame % 90 == 0) SButtonAnimation();
        if (flame >= 50000) flame = 0;
    }

    //勝利プレイヤー判定と点数格納
    public void UpdateDisplay()
    {
        for (int i = 0; i < 6; i++)
        {
            Rank[i] = 0;
            BattlePoint[i] = CManager.GetPlayerCost(i + 1).critical + CManager.GetPlayerCost(i + 1).crush + CManager.GetPlayerCost(i+1).rank;
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; ++j)
            {
                if (BattlePoint[i] < BattlePoint[j])
                {
                    Rank[i]++;  //順位の格納
                }
            }
        }
        for(int i = 0; i < Rank.Length; ++i)
        {
            int num = 0;
            for (int j = 0; j < Rank.Length; ++j)
            {
                if (i == j) continue;
                if (Rank[i] == Rank[j])
                {
                    num++;
                    Rank[j]+=num;
                }
            }

        }
        PointSet();
        RankSet();
    }

    public void Init()
    {
        for(int i = 0; i < Rank.Length; ++i)
        {
            Rank[i] = i;
        }
        PointSet();
        RankSet();
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

    }

    void RankSet()
    {
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 6; j++)
            {
                if (Rank[i] == j)
                {
                    PlayerNumber[j].sprite = ImagesNumber[i]; //プレイヤランクセット
                }
            }
        }
    }

    void PointSet()
    {
        for (int i = 0; i < 6; i++)
        {
            int ThousandPlace = (BattlePoint[i] / 1000) % 10;   //1000の位
            int HundredPlace = (BattlePoint[i] / 100) % 10;   //100の位
            int TenPlace = (BattlePoint[i] / 10) % 10;        //10の位
            int Digit = BattlePoint[i] % 10;                //1の位

            for (int j = 0; j < 10; j++)
            {
                //battle
                if (Digit == j)
                {
                    PannelDigit[Rank[i]].texture = Images[j];
                    PannelDigit[Rank[i]].color = Color.white;
                }
                //10の位
                if (TenPlace == j)
                {
                    if (HundredPlace == 0)
                    {
                        if (TenPlace != 0)
                        {
                            PannelTenPlace[Rank[i]].texture = Images[j];
                            PannelTenPlace[Rank[i]].color = Color.white;
                        }
                        else if (PannelTenPlace[Rank[i]].texture == Images[0] && HundredPlace == 0)
                        {
                            PannelTenPlace[Rank[i]].color = SetColors[6]; //桁消去
                        }
                    }
                    else
                    {
                        PannelTenPlace[Rank[i]].texture = Images[j];
                        PannelTenPlace[Rank[i]].color = Color.white;
                    }
                }
                //100の位
                if (HundredPlace == j)
                {
                    if (ThousandPlace == 0)
                    {
                        if (HundredPlace != 0)
                        {
                            PannelHundredPlace[Rank[i]].texture = Images[j];
                            PannelHundredPlace[Rank[i]].color = Color.white;
                        }
                        else if (PannelHundredPlace[Rank[i]].texture == Images[0] && ThousandPlace == 0)
                        {
                            PannelHundredPlace[Rank[i]].color = SetColors[6]; //桁消去
                        }
                    }
                    else
                    {
                        PannelHundredPlace[Rank[i]].texture = Images[j];
                        PannelHundredPlace[Rank[i]].color = Color.white;
                    }
                }
                //1000の位
                if (ThousandPlace == j)
                {
                    if (ThousandPlace != 0) PannelThousandPlace[Rank[i]].texture = Images[j];
                    else PannelThousandPlace[Rank[i]].color = SetColors[6];//桁消去
                }

            }
        }
    }

    void SButtonAnimation()
    {
        now_s++;
        if (now_s > 1) now_s = 0;
        Startbutton.sprite = Sbutton[now_s]; //Ready前こっちだけ
    }
}

