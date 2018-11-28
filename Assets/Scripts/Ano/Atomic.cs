using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atomic : MonoBehaviour {
    ParticleSystem MyParticle;
    //オーディオ
    public AudioManager audio;
    // Use this for initialization
    void Awake () {
        MyParticle = GetComponent<ParticleSystem>();
        //爆発が生成された時にSEを鳴らす
        PlaysSe();
    }
	
	// Update is called once per frame
	void Update () {
		if(!MyParticle.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
    private void PlaysSe()
    {
        //SEの種類弄るのはここ
        //AudioManager.GetInstance.PlaySE0(AUDIO.SE_Hit01);
    }
}
