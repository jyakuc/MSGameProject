using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScripts : MonoBehaviour {

    public GameObject Canvas;
    public GameObject uiPlayer;
    public GameObject uiRank;
    public RawImage[] Pannel;
    public Texture[] Images;
    private BattlePointGrading battlePoint;
    private int test=0;



    // Use this for initialization
    void Start () {
        //PlayerUIの生成
        GameObject pPlayerUI = (GameObject)Instantiate(uiPlayer);
        pPlayerUI.transform.SetParent(Canvas.transform, false);
        //RankingUIの生成
        GameObject pRankUI = (GameObject)Instantiate(uiRank);
        pRankUI.transform.SetParent(Canvas.transform, false);
    }

    // Update is called once per frame
    void Update () {
        //test = battlePoint.GetAllPoint();
        int test100 = test / 100;
        int test10 = test / 10;
        int test1 = test % 10;
        if (test1 == 0)
        {
            Pannel[0].texture = Images[test1];
            Pannel[0].color = new Color(255, 255, 255, 1f);
        }
        if (test10 == 0)
        {
            Pannel[1].texture = Images[test10];
            Pannel[1].color = new Color(255, 255, 255, 1f);
        }
        if (test100 == 0)
        {
            Pannel[2].texture = Images[test100];
            Pannel[2].color = new Color(255, 255, 255, 1f);
        }
    }

}
