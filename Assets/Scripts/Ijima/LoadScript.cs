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
    public Image[] PlayerNumber;
    public Image[] Startbutton;
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
        Init();
        flame = 0;
        now_s = 0;

        PointColorSet();
        RankSet();
        PointSet();
    }

    void Update()
    {
        flame++;
        if (flame % 90 == 0) SButtonAnimation();
        if (flame >= 50000) flame = 0;
    }

    //勝利プレイヤー判定と点数格納
    public void Init()
    {
        for (int i = 0; i < 6; i++)
        {
            Rank[i] = 0;
            BattlePoint[i] = CManager.GetPlayerCost(i + 1).critical + CManager.GetPlayerCost(i + 1).crush;
        }
        for (int i = 0; i < 6; i++)
        {
            for (int j = i + 1; j < 6; ++j)
            {
                if (BattlePoint[i] <= BattlePoint[j])
                {
                    Rank[i]++;  //順位の格納
                }
            }
        }
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
            int HundredPlace = (BattlePoint[i] / 100) % 10;   //100の位
            int TenPlace = (BattlePoint[i] / 10) % 10;        //10の位
            int Digit = BattlePoint[i] % 10;                //1の位

            for (int j = 0; j < 10; j++)
            {
                //battle
                if (Digit == j)
                {
                    PannelDigit[Rank[i]].texture = Images[j];
                }
                //10の位
                if (TenPlace == j)
                {
                    if (HundredPlace == 0)
                    {
                        if (TenPlace != 0) PannelTenPlace[Rank[i]].texture = Images[j];
                        else PannelTenPlace[Rank[i]].color = SetColors[6]; //桁消去
                    }
                    else
                    {
                        PannelTenPlace[Rank[i]].texture = Images[j];
                    }
                }
                //100の位
                if (HundredPlace != 0)
                {
                    if (HundredPlace == j)
                    {
                        PannelHundredPlace[Rank[i]].texture = Images[j];
                    }
                }
                else
                {
                    PannelHundredPlace[Rank[i]].color = SetColors[6];//桁消去
                }

            }
        }
    }

    void SButtonAnimation()
    {
        now_s++;
        if (now_s > 1) now_s = 0;
        Startbutton[0].sprite = Sbutton[now_s]; //Ready前こっちだけ
        Startbutton[1].sprite = Sbutton[now_s]; //Ready後こっちだけにしたい
    }
}

