using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
    //エフェクト
    private ParticleSystem MyParticle;
    public GameObject NextEffect;
    //動的生成用
    private GameObject NewHitEffect;
    public GameObject Rock;
    private ParticleSystem AtomicEffect;
    private Rigidbody Myrigidbody;
    private CapsuleCollider MyCollider;
    void OnTriggerEnter(Collider other)
    {
        MyParticle.Stop();
        //Effect生成
        Rock.SetActive(false);
        Myrigidbody.useGravity = false;
        MyCollider.isTrigger = false;
        NewHitEffect = (GameObject)Instantiate(NextEffect, transform.position, Quaternion.identity);
        NewHitEffect.GetComponent<Transform>().LookAt(MyParticle.transform);
        AtomicEffect = NewHitEffect.GetComponent<ParticleSystem>();
        NewHitEffect.GetComponent<Transform>().localScale = MyParticle.transform.localScale;

        AtomicEffect.Play();


    }
    // Use this for initialization
    void Awake () {
        MyParticle = GetComponent<ParticleSystem>();
        Myrigidbody = GetComponent<Rigidbody>();
        MyCollider = GetComponent<CapsuleCollider>();

    }
	
	// Update is called once per frame
	void Update () {
        if(!MyParticle.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
}
