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

    public void SetRank(int playerID)
    {

        if (rank < 4)
        {
            Pannel[playerID - 1].GetComponent<Image>().sprite = Image[rank];
            Pannel[playerID - 1].GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            rank++;
        }
        
    }

    // 順位表示初期化
    public void Init()
    {
        for(int i = 0; i < 6; ++i)
        {
            Pannel[i].GetComponent<Image>().sprite = null;
            Pannel[i].GetComponent<Image>().color = new Color(255, 255, 255, 0f);
        }
        rank = 0;
    }
}
