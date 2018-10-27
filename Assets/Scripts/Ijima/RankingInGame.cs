using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingInGame : MonoBehaviour
{

    public enum RankingType
    {
        E_6th = 0,
        E_5th = 1,
        E_4th = 2,
        E_3rd = 3
    }

    [Header("MustSetting")]
    [Tooltip("セットしてね")]
    public Image[] Pannel;
    public Sprite[] Image;
    private short rank = 0;
    private short player = 0;
    private int time = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////////////
        //仮の表示
        if (rank < 4)
        {
            time++;
        }
        if (time == 100)
        {
            SetRank();
            player++;
            rank++;
            time = 0;
        }
        ///////////////////////////////////////
    }

    void SetRank()
    {
        Pannel[player].GetComponent<Image>().sprite = Image[rank];
        Pannel[player].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
    }
}
