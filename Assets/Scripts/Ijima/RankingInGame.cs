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
       
    }

    public void SetRank(int playerID)
    {

        if (rank < 4)
        {
            Pannel[playerID - 1].GetComponent<Image>().sprite = Image[rank];
            Pannel[playerID - 1].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            rank++;
        }
        
    }
}
