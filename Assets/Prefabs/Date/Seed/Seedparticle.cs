using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedparticle : MonoBehaviour {
    ParticleSystem ParticleData;
    float StartTime = 0;
    Transform TopPos;
    Transform Pos;
    GameObject hiyama;
    public AnimatorStateInfo aniinfo;
    public float anitime;
    bool One = false;
	// Use this for initialization
	void Start () {
        ParticleData = GetComponent<ParticleSystem>();
        hiyama = GameObject.Find("hiiyama");
        TopPos = GameObject.Find("top").GetComponent<Transform>();
        Pos = GetComponent<Transform>();
        Pos = TopPos;
        aniinfo = hiyama.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
    }
	
	// Update is called once per frame
	void Update () {
        Pos = TopPos;
        aniinfo = hiyama.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        anitime = aniinfo.normalizedTime;
        //Debug.Log(anitime);
        if (anitime>=0.75)
        {
            if(!One)
            {
                ParticleData.Play();
                One = true;
            }


        }
        else
        {
            One = false;
        }
	}
}
