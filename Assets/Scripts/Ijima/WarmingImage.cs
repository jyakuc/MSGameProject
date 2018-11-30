using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmingImage : MonoBehaviour {

    public Sprite[] LRSticks;
    public Sprite[] UDSticks;
    public Sprite[] Abutton;
    public Sprite[] Bbutton;
    public Sprite[] Xbutton;
    public Sprite[] Ybutton;
    public Sprite[] Sbutton;
    public Image[] Pannels;
    private int flame;
    private int now_st;
    private int now_a;
    private int now_b;
    private int now_x;
    private int now_y;
    private int now_s;

    // Use this for initialization
    void Start () {
        now_st = 0;
        now_a = 0;
        now_b = 0;
        now_x = 0;
        now_y = 0;
        now_s = 0;
        flame = 0;
	}
	
	// Update is called once per frame
	void Update () {
        flame++;
        if (flame >= 50000) flame = 0;
        if (flame % 60 == 0) StickAnimation();
        if (flame % 33 == 0) AButtonAnimation();
        if (flame % 36 == 0) BButtonAnimation();
        if (flame % 39 == 0) XButtonAnimation();
        if (flame % 42 == 0) YButtonAnimation();
        if (flame % 45 == 0) SButtonAnimation();
    }

    void StickAnimation()
    {
        now_st++;
        if (now_st > 3) now_st = 0;
        Pannels[0].sprite = LRSticks[now_st];
        Pannels[1].sprite = UDSticks[now_st];
    }
    void AButtonAnimation()
    {
        now_a++;
        if (now_a > 1) now_a = 0;
        Pannels[2].sprite = Abutton[now_a];
    }
    void BButtonAnimation()
    {
        now_b++;
        if (now_b > 1) now_b = 0;
        Pannels[3].sprite = Bbutton[now_b];
    }
    void XButtonAnimation()
    {
        now_x++;
        if (now_x > 1) now_x = 0;
        Pannels[4].sprite = Xbutton[now_x];
    }
    void YButtonAnimation()
    {
        now_y++;
        if (now_y > 1) now_y = 0;
        Pannels[5].sprite = Ybutton[now_y];
    }
    void SButtonAnimation()
    {
        now_s++;
        if (now_s > 1) now_s = 0;
        Pannels[6].sprite = Sbutton[now_s];
    }
}
