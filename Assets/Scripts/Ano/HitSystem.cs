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
    private PlayerController P_Controller;
    //エフェクト発生中か
    public bool HitEffectFlag = false;
    //ちょっと多段ヒットしているので一時的に時間で制御
    private float DelayTime = 0.5f;
    private float countTime = 0;

    private bool TimeFlag = false;

      // Add：弓達　バトル採点クラス保持
    public BattlePointGrading BattlePoint;

    [SerializeField]
    private PlayerWhole_ParamTable m_paramTable;
    private GameTime gametime;

    private PlayerCamera p_camera;
    private bool CriticalFlag = false;
    //Critical用のオブジェクト保存
    private GameObject SaveHitObject;
    void OnTriggerStay(Collider other)
    {
        //レイヤーの名前取得
        string LayerName = LayerMask.LayerToName(other.gameObject.layer);
        //プレイヤーオブジェクトに当たったかどうか
        if (PlayerCheck(other))
        {
            try
            {
                //当たったオブジェクトが吹っ飛び状態なのか
                if (other.gameObject.transform.root.gameObject.GetComponent<PlayerController>().GetMyState() == PlayerController.EState.BlowAway)
                {
                    return;
                }
            }
            catch
            {
                return;
            }


        }
        //それ以外はプレイヤーではないのでこの時点で処理は終了させる
        else
        {
            return;
        }

        //判定を取らないプレイヤーのオブジェクトなのか判定
        if (!OnHitObject(other.name))
        {
            return;
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
                (LayerName == "Player_01") ||
                (LayerName == "Player_02") ||
                (LayerName == "Player_03") ||
                (LayerName == "Player_04") ||
                (LayerName == "Player_05") ||
                (LayerName == "Player_06") ||
                (LayerName == "SandBack"))
            {
                HitSelect HitTypeCheck;
                try
                {
                    //自分には判定しないために相手のオブジェクトのプレイヤーIDを取得
                    if(P_Controller.PlayerID!= other.gameObject.transform.root.GetComponent<PlayerController>().PlayerID)
                    {
                        HitTypeCheck = HitType(other.gameObject);
                        //エフェクト発生
                        HitEffectFlag = true;
                        if (HitSelect.Hit == HitTypeCheck)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(HitTypeCheck, other.gameObject.transform);
                            HitParticle.Play();
                        }
                    }
                }
                catch
                {
                    if((LayerName == "SandBack"))
                    {
                        HitTypeCheck = HitType(other.gameObject);
                        if (HitSelect.Hit == HitTypeCheck)
                        {
                            //SE再生
                            PlaysSe();
                            //Effect生成
                            CreateEffect(HitTypeCheck, other.gameObject.transform);
                            HitParticle.Play();
                        }
                    }
                }

            }

        }

    }
    //void OnTriggerExit(Collider other)
    //{
    //    //レイヤーの名前取得
    //    string LayerName = LayerMask.LayerToName(other.gameObject.layer);

    //    if ((LayerName == "Player_Chest") ||
    //        (LayerName == "Player_1") ||
    //        (LayerName == "Player_2") ||
    //        (LayerName == "Player_3") ||
    //        (LayerName == "Player_4") ||
    //        (LayerName == "Player_5") ||
    //        (LayerName == "Player_6") ||
    //        (LayerName == "SandBack"))
    //    {

    //    }

    //}
    private bool PlayerCheck(Collider col)
    {
        string LayerName = LayerMask.LayerToName(col.gameObject.layer);
        if ((LayerName == "Player_01") ||
            (LayerName == "Player_02") ||
            (LayerName == "Player_03") ||
            (LayerName == "Player_04") ||
            (LayerName == "Player_05") ||
            (LayerName == "Player_06") ||
            (LayerName == "SandBack"))
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

        P_Controller = transform.root.GetComponent<PlayerController>();

    }
	void CriticalCreate()
    {
        if(CriticalFlag)
        {
            //SE再生
            PlaysSe();
            //Effect生成
            BlowAway(SaveHitObject, HitSelect.Critical);
            CreateEffect(HitSelect.Critical, SaveHitObject.transform);
            HitParticle.Play();
            CriticalFlag = false;
            SaveHitObject = null;
        }

    }
	// Update is called once per frame
	void Update () {
        //再生させてから時間測定
        if (HitEffectFlag)
        {
            countTime += Time.deltaTime;
            this.transform.root.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if(p_camera!=null)
            {
                if(p_camera.GetZoomEndFlag())
                {
                    CriticalCreate();
                }
            }
        }
        //一定時間経過で0に戻して発生可能にする
        if (countTime > DelayTime)
        {
            TimeFlag = true;
            countTime = 0;
        }
        //時間経過でEffect再生
        if (TimeFlag)
        {
            HitEffectFlag = false;
            TimeFlag = false;
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
            (HitObject.name == "Belly") ||
            (HitObject.name == "Lower"))
        {
            int Probability = Random.Range(0, 100);
            Debug.Log(Probability);
            if (Probability <= m_paramTable.criticalProbability)
            {
                CriticalFlag = true;
                SaveHitObject = HitObject;
                HitStop();
                //BlowAway(HitObject,HitSelect.Critical);
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
        //サンドバック用点数など加算しないようにする
        if (HitObject.transform.root.gameObject.name== "SandBack(Clone)")
        {
            switch (EffectType)
            {
                case HitSelect.Hit:
                    //AddForceを入れる（衝撃を与えるのでForceModeはImpulse
                    HitRigid.AddForce((HitRigid.position - this.transform.position).normalized * (m_paramTable.normalHitPower*20), ForceMode.Impulse);
                    break;
                case HitSelect.Critical:

                    //AddForceを入れる（衝撃を与えるのでForceModeはImpulse
                    HitRigid.AddForce((HitRigid.position - this.transform.position).normalized *( m_paramTable.criticalHitPower*20), ForceMode.Impulse);
                    break;
            }
            return;
        }
        // 誰に吹き飛ばされたかを保持
        hitPlayer.BlowAwayNow(P_Controller.PlayerID);
        switch (EffectType)
        {
            case HitSelect.Hit:
                //AddForceを入れる（衝撃を与えるのでForceModeはImpulse
                HitRigid.AddForce((HitRigid.position - this.transform.position).normalized * (m_paramTable.normalHitPower*20), ForceMode.Impulse);
                break;
            case HitSelect.Critical:

                //AddForceを入れる（衝撃を与えるのでForceModeはImpulse
                HitRigid.AddForce((HitRigid.position - this.transform.position).normalized * (m_paramTable.criticalHitPower*20), ForceMode.Impulse);
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
    private bool OnHitObject(string Name)
    {
        bool Flag = false;
        //Hit判定を取る部位の名前はTrueにするそれ以外Falseにする
        switch (Name)
        {
            case "Belly":
                Flag = true;
                break;
            case "Chest":
                Flag = true;
                break;
            case "Head":
                Flag = true;
                break;
            case "Shoulder_L":
                Flag = true;
                break;
            case "UpperArms_L":
                Flag = true;
                break;
            case "Arms_L":
                Flag = true;
                break;
            case "Hand_L":
                Flag = true;
                break;
            case "Shoulder_R":
                Flag = true;
                break;
            case "UpperArms_R":
                Flag = true;
                break;
            case "Arms_R":
                Flag = true;
                break;
            case "Hand_R":
                Flag = true;
                break;
            case "Lower":
                Flag = true;
                break;
            case "Ass_L":
                Flag = true;
                break;
            case "Thighs_L":
                Flag = true;
                break;
            case "Ass_R":
                Flag = true;
                break;
            case "Thighs_R":
                Flag = true;
                break;
        }

        return Flag;
    }
}
