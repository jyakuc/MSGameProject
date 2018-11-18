using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour {
    private enum HitSelect { Hit,Critical};
    //生成するPrefab
    public GameObject HitPrefab;
    public GameObject CriticalPrefab;
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
    public int PlayNum = 1;
    //ちょっと多段ヒットしているので一時的に時間で制御
    private float DelayTime = 2.0f;
    private float countTime = 0;

    private bool TimeFlag = false;

      // Add：弓達　バトル採点クラス保持
    public BattlePointGrading BattlePoint;

    private PlayerCamera p_camera;
    void OnTriggerStay(Collider other)
    {
        //レイヤーの名前取得
        string LayerName = LayerMask.LayerToName(other.gameObject.layer);
        //プレイヤーオブジェクトに当たったかどうか
        if (PlayerCheck(other))
        {
            //当たったオブジェクトが吹っ飛び状態なのか
            if(other.gameObject.transform.root.gameObject.GetComponent<PlayerController>().GetMyState()== PlayerController.EState.BlowAway)
            {
                return;
            }

        }
        if ((!HitEffectFlag)&&
            ((PlayerController.EState.RightMove== P_Controller.GetMyState())||
            (PlayerController.EState.LeftMove == P_Controller.GetMyState())))
        {
            //プレイヤーの体判定の部位
            if (LayerName == "Player_Chest")
            {
                //SE再生
                PlaysSe();
                //Effect生成
                CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
                            CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
                            CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
                            CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
                            CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
                            CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
                            CreateEffect(HitType(other.gameObject),other.gameObject.transform);
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
    private bool PlayerCheck(Collider col)
    {
        string LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if ((LayerName == "Player_Chest") ||
            (LayerName == "Player_1") ||
            (LayerName == "Player_2") ||
            (LayerName == "Player_3") ||
            (LayerName == "Player_4") ||
            (LayerName == "Player_5") ||
            (LayerName == "Player_6"))
        {
            return true;
        }
        return false;
    }
    // Use this for initialization
    void Start () {


  	if(BattlePoint == null)
        {
            BattlePoint = transform.root.GetComponent<BattlePointGrading>();
        }
        try
        {
            p_camera = this.transform.root.gameObject.GetComponent<PlayerCamera>();

        }
        catch
        {
            p_camera = null;
        }

    }
	
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
        if (NewHitEffect != null)
        {
            try
            {
                if (!NewHitEffect.GetComponent<ParticleSystem>().isPlaying)
                {
                    Destroy(NewHitEffect);
                }
            }
            catch
            {

            }
        }
    }
    private void HitStop()
    {
        if (p_camera==null) return;
        p_camera.ZoomStart();
    }
    //SE再生処理
    private void PlaysSe()
    {
        //仮に使っているので新しいSEが届き次第変更
        AudioManager.GetInstance.PlaySE0(AUDIO.SE_Hit01);
    }
    //Hitの種類選択
    private HitSelect HitType(GameObject HitObject)
    {
        Debug.Log(HitObject.name);
        if ((HitObject.name=="Haed")||
            (HitObject.name == "Shoulder_L") ||
            (HitObject.name == "Shoulder_R")||
            (HitObject.name == "Ass_L") ||
            (HitObject.name == "Ass_R"))
        {
            int Probability = Random.Range(0, 100);
            Debug.Log(Probability);
            if (Probability <= P_Controller.CriticalProbability)
            {
                BlowAway(HitObject,HitSelect.Critical);
                return HitSelect.Critical;
            }
        }
        BlowAway(HitObject,HitSelect.Hit);
        return HitSelect.Hit;
    }
    //吹き飛ばし処理
    private void BlowAway(GameObject HitObject, HitSelect EffectType)
    {

        //親のRigidbodyを探す
        Rigidbody HitRigid = HitObject.transform.root.gameObject.GetComponent<Rigidbody>();
        PlayerController hitPlayer = HitObject.transform.root.gameObject.GetComponent<PlayerController>();

        // 誰に吹き飛ばされたかを保持
        hitPlayer.BlowAwayNow(P_Controller.PlayerID);

        switch (EffectType)
        {
            case HitSelect.Hit:
                //AddForceを入れる（衝撃を与えるのでForceModeはImpulse
                HitRigid.AddForce(this.transform.position* P_Controller.HitPower, ForceMode.Impulse);
                break;
            case HitSelect.Critical:
                HitStop();
                //AddForceを入れる（衝撃を与えるのでForceModeはImpulse
                HitRigid.AddForce(this.transform.position * P_Controller.CriticalPower, ForceMode.Impulse);
                // Add:弓達　クリティカルヒット時得点付与
                BattlePoint.AddCriticalPoint(hitPlayer.PlayerID , P_Controller.PlayerID);
                Debug.Log("クリティカルヒット my:" + transform.root + "your:" + HitObject);
                break;
        }
    }
    //エフェクト生成処理
    private void CreateEffect(HitSelect EffectType,Transform trans)
    {
        switch(EffectType)
        {
            case HitSelect.Hit:
                NewHitEffect = (GameObject)Instantiate(HitPrefab, transform.position, Quaternion.identity);
                break;
            case HitSelect.Critical:
                NewHitEffect = (GameObject)Instantiate(CriticalPrefab, transform.position, Quaternion.identity);
                break;
        }
        NewHitEffect.GetComponent<Transform>().LookAt(trans);
        HitParticle = NewHitEffect.GetComponent<ParticleSystem>();
        //NewHitEffect.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }
}
