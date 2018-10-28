using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour {
    //生成するPrefab
    public GameObject HitPrefab;
    //オーディオ
    public AudioManager audio;
    //エフェクト
    private ParticleSystem HitParticle;
    //動的生成用
    private GameObject NewHitEffect;
    //キャラクターのID取得
    public PlayerController P_Controller;
    //エフェクト発生中か
    private bool HitEffectFlag = false;
    //プレイヤーID一致用
    public int PlayNum=1;
    //ちょっと多段ヒットしているので一時的に時間で制御
    private float DelayTime=2.0f;
    private float countTime = 0;
    private bool TimeFlag = false;
    void OnTriggerStay(Collider other)
    {
        //レイヤーの名前取得
        string LayerName = LayerMask.LayerToName(other.gameObject.layer);
        if(!HitEffectFlag)
        {
            //プレイヤーの体判定の部位
            if (LayerName == "Player_Chest")
            {
                //SE再生
                PlaysSe();
                //Effect生成
                CreateEffect(other.gameObject.transform);
                //エフェクト発生
                HitEffectFlag = true;
                HitParticle.Play();
                return;
            }
            //プレイヤーの体判定
            if (
                (LayerName == "Player_1") ||
                (LayerName == "Player_2") ||
                (LayerName == "Player_3") ||
                (LayerName == "Player_4") ||
                (LayerName == "Player_5") ||
                (LayerName == "Player_6"))
            {
                //自分には判定しない
                switch (LayerName)
                {
                    case "Player_1":
                        if (PlayNum != P_Controller.PlayerID)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(other.gameObject.transform);
                            //エフェクト発生
                            HitEffectFlag = true;
                            HitParticle.Play();
                            return;
                        }
                        break;
                    case "Player_2":
                        if (PlayNum != P_Controller.PlayerID)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(other.gameObject.transform);
                            //エフェクト発生
                            HitEffectFlag = true;
                            HitParticle.Play();
                            return;
                        }
                        break;
                    case "Player_3":
                        if (PlayNum != P_Controller.PlayerID)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(other.gameObject.transform);
                            //エフェクト発生
                            HitEffectFlag = true;
                            HitParticle.Play();
                            return;
                        }
                        break;
                    case "Player_4":

                        if (PlayNum != P_Controller.PlayerID)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(other.gameObject.transform);
                            //エフェクト発生
                            HitEffectFlag = true;
                            HitParticle.Play();
                            return;
                        }
                        break;
                    case "Player_5":
                        if (PlayNum != P_Controller.PlayerID)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(other.gameObject.transform);
                            //エフェクト発生
                            HitEffectFlag = true;
                            HitParticle.Play();
                            return;
                        }
                        break;
                    case "Player_6":
                        if (PlayNum != P_Controller.PlayerID)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(other.gameObject.transform);
                            //エフェクト発生
                            HitEffectFlag = true;
                            HitParticle.Play();
                            return;
                        }
                        break;
                }

            }

        }


    }
    void OnTriggerExit(Collider other)
    {
        //レイヤーの名前取得
        string LayerName = LayerMask.LayerToName(other.gameObject.layer);

        if ((LayerName == "Player_Chest") ||
            (LayerName == "Player_1") ||
            (LayerName == "Player_2") ||
            (LayerName == "Player_3") ||
            (LayerName == "Player_4") ||
            (LayerName == "Player_5") ||
            (LayerName == "Player_6"))
        {
            //時間経過でEffect再生
            if (TimeFlag)
            {
                HitEffectFlag = false;
                TimeFlag = false;
            }
        }

    }
    private void Awake()
    {

    }
    //SE再生処理
    private void PlaysSe()
    {
        //仮に使っているので新しいSEが届き次第変更
        AudioManager.GetInstance.PlaySE0(AUDIO.SE_Hit01);
    }
    //エフェクト生成処理
    private void CreateEffect(Transform trans)
    {
        NewHitEffect = (GameObject)Instantiate(HitPrefab, transform.position, Quaternion.identity);
        NewHitEffect.GetComponent<Transform>().LookAt(trans);
        HitParticle = NewHitEffect.GetComponent<ParticleSystem>();
        NewHitEffect.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }
    // Use this for initialization

    // Update is called once per frame
    void Update () {
        //再生させてから時間測定
        if (HitEffectFlag)
        {
            countTime += Time.deltaTime;
        }
        //一定時間経過で0に戻して発生可能にする
        if (countTime > DelayTime)
        {
            TimeFlag = true;
            countTime = 0;
        }
        //エフェクトが動的に作られていて再生終わっていたら消す
        if(NewHitEffect!=null)
        {
            try
            {
                if(!NewHitEffect.GetComponent<ParticleSystem>().isPlaying)
                {
                    Destroy(NewHitEffect);
                }
            }
            catch
            {

            }
        }
    }
}
