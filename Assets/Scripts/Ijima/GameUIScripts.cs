using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIScripts : MonoBehaviour {

    public GameObject Canvas;
    public GameObject uiPlayer;
    public GameObject uiRank;



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

    }

}
