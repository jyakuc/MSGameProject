using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum EInput
    {
        Horizontal,
        Vertical,
        A, B, X, Y,
        MAX
    }

    public enum EState
    {
        Init,
        Wait,
        Idle,
        BlowAway,
        Atack,
        RightMove,
        LeftMove,
        Dead,
        Win
    }

    // プレイヤー番号
    [SerializeField]
    private int m_playerID;
    public int PlayerID
    {
        get { return m_playerID; }
    }
    /*
    // 手足のMaterial取得
    public Material Hand_R;
    public Material Hand_L;
    public Material Calf_R;
    public Material Calf_L;
    // デフォルトの肌の色を保存用
    private Color SaveColor;
    */
    private MyInputManager myInputManager;
    private string[] InputName = new string[(int)EInput.MAX];
    private EState m_state;
    private float FlayTime_Max = 1;
    //飛んでる経過時間
    private float FlayNowTime = 0;
    private bool[] m_isInputFlg = new bool[(int)EInput.MAX];
    // 攻撃された相手のプレイヤーID
    private int m_hitReceivePlayerID;
    public int HitReceivePlayerID
    {
        get { return m_hitReceivePlayerID; }
    }

    private CrushPointManager m_crushPointManager;

    private PlayerExtendAndShrink m_extendAndShrink;
    private PlayerMoving m_moving;
    private PlayerCamera P_Camera;
    [SerializeField]
    private PlayerRay m_ray;

    // Use this for initialization
    void Start()
    {
        /*
        // デフォルトの肌の色の数値を代入する
        SaveColor = new Color(1.0f, 0.78f, 0.55f, 1.0f);
        // 色が変わった状態でHumanが無くなるとMaterialが変更されてままになるので生成時に初期色入れておく
        Hand_R.color = SaveColor;
        Hand_L.color = SaveColor;
        Calf_R.color = SaveColor;
        Calf_L.color = SaveColor;
        */
        m_state = EState.Init;
        m_hitReceivePlayerID = PlayerID;

        InputName[0] = "Horizontal_Player";
        InputName[1] = "Vertical_Player";
        InputName[2] = "A_Player";
        InputName[3] = "B_Player";
        InputName[4] = "X_Player";
        InputName[5] = "Y_Player";

        myInputManager = FindObjectOfType<MyInputManager>();
        if (myInputManager == null) Debug.LogError("MyInputManagerがシーンに存在しません");

        m_crushPointManager = FindObjectOfType<CrushPointManager>();
        if (m_crushPointManager == null) Debug.LogError("CrushPointManagerがシーンに存在しません。");

        // 手足伸ばすスクリプト
        m_extendAndShrink = GetComponent<PlayerExtendAndShrink>();
        if (m_extendAndShrink == null) Debug.LogError("PlayerExtendAndShrinkをアタッチしてください。");
        // 移動処理スクリプト
        m_moving = GetComponent<PlayerMoving>();
        if (m_moving == null) Debug.LogError("PlayerMovingをアタッチしてください。");
        // プレイヤーカメラスクリプト
        P_Camera = GetComponent<PlayerCamera>();
        if (P_Camera == null) Debug.LogError("PlayerCameraをアタッチしてください。");
        // 地面とのRayの処理スクリプト
        if (m_ray == null) Debug.LogError("PlayerRayをアタッチしてください。");

        if (!DebugModeGame.GetProperty().m_debugMode) return;
        // デバッグモードONの時の設定
        if (DebugModeGame.GetProperty().m_debugPlayerEnable)
        {
            m_state = EState.Idle;
        }
        if (DebugModeGame.GetProperty().m_controllerDisable)
        {
            myInputManager.joysticks[m_playerID - 1] = m_playerID;
        }
    }

    public EState GetMyState()
    {
        return m_state;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_state == EState.Init ||
            m_state == EState.Dead ||
            m_state == EState.Wait ||
            m_state == EState.Win) return;

        if (m_state == EState.BlowAway)
        {
            FlayNowTime += Time.deltaTime;
            if (FlayNowTime >= FlayTime_Max)
            {
                FlayNowTime = 0;
                m_state = EState.Idle;
            }
            return;
        }
        // 入力値
        float lsh = Input.GetAxis(InputName[(int)EInput.Horizontal] + myInputManager.joysticks[m_playerID - 1].ToString());
        float lsv = Input.GetAxis(InputName[(int)EInput.Vertical] + myInputManager.joysticks[m_playerID - 1].ToString());


        if (lsh == 0.0f)
        {
            m_state = EState.Idle;
            m_isInputFlg[(int)EInput.Vertical] = m_isInputFlg[(int)EInput.Horizontal] = false;
        }
        else if (lsh > 0.0f)
        {
            m_moving.Move(m_ray.Dir, true);
            m_state = EState.RightMove;
            m_isInputFlg[(int)EInput.Horizontal] = true;
        }
        // 左方向
        else if (lsh < 0.0f)
        {
            m_moving.Move(m_ray.Dir, false);
            m_state = EState.LeftMove;
            m_isInputFlg[(int)EInput.Horizontal] = true;
        }

        if (lsv > 0.0f)
        {
            m_moving.Rotation(true);
            m_isInputFlg[(int)EInput.Vertical] = true;
        }
        else if (lsv < 0.0f)
        {
            m_moving.Rotation(false);
            m_isInputFlg[(int)EInput.Vertical] = true;
        }


        // 右手の伸縮
        if (Input.GetButton(InputName[(int)EInput.A] + myInputManager.joysticks[m_playerID - 1]))
        {
            m_extendAndShrink.StartShrink(PlayerExtendAndShrink.EShrinkPoint.RightHand);
            m_isInputFlg[(int)EInput.A] = true;
        }
        else 
        {
            if(m_isInputFlg[(int)EInput.A])
            {
                m_extendAndShrink.StartExtend(PlayerExtendAndShrink.EShrinkPoint.RightHand);
                m_isInputFlg[(int)EInput.A] = false;
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }
        // 左手の伸縮
        if (Input.GetButton(InputName[(int)EInput.B] + myInputManager.joysticks[m_playerID - 1]))
        {
            m_extendAndShrink.StartShrink(PlayerExtendAndShrink.EShrinkPoint.LeftHand);
            m_isInputFlg[(int)EInput.B] = true;
        }
        else
        {
            if (m_isInputFlg[(int)EInput.B])
            {
                m_extendAndShrink.StartExtend(PlayerExtendAndShrink.EShrinkPoint.LeftHand);
                m_isInputFlg[(int)EInput.B] = false;
                Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBBBBB");
            }
        }
        // 右足の伸縮
        if (Input.GetButton(InputName[(int)EInput.X] + myInputManager.joysticks[m_playerID - 1]))
        {
            m_extendAndShrink.StartShrink(PlayerExtendAndShrink.EShrinkPoint.RightFoot);
            m_isInputFlg[(int)EInput.X] = true;
        }
        else
        {
            if (m_isInputFlg[(int)EInput.X])  m_extendAndShrink.StartExtend(PlayerExtendAndShrink.EShrinkPoint.RightFoot);
            m_isInputFlg[(int)EInput.X] = false;
            Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        }
        // 左足の伸縮
        if (Input.GetButton(InputName[(int)EInput.Y] + myInputManager.joysticks[m_playerID - 1]))
        {
            m_extendAndShrink.StartShrink(PlayerExtendAndShrink.EShrinkPoint.LeftFoot);
            m_isInputFlg[(int)EInput.Y] = true;
        }
        else
        {
            if (m_isInputFlg[(int)EInput.Y]) m_extendAndShrink.StartExtend(PlayerExtendAndShrink.EShrinkPoint.LeftFoot);
            m_isInputFlg[(int)EInput.Y] = false;
            Debug.Log("YYYYYYYYYYYYYYYYYYYYYYYYYYYY");
        }

        // 回転減衰
        if (m_state == EState.LeftMove || m_state == EState.RightMove)
            m_moving.DecayForce();

        if (m_ray.IsDIffDirection())
        {
            m_state = EState.Idle;
            m_ray.InitDiffDirection();

            m_moving.Init();
        }
    }

    public void Dead()
    {
        // 自分が獲得したポイントをCostManager登録
        if (m_state == EState.Dead) return;
        m_state = EState.Dead;
        GetComponent<Rigidbody>().isKinematic = true;
        P_Camera.CameraDelete();
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void PlayStart()
    {
        m_state = EState.Idle;
    }

    // フラグ取得関数
    public bool IsDead()
    {
        if (m_state == EState.Dead) return true;
        return false;
    }

    public bool IsInit()
    {
        if (m_state == EState.Init) return true;
        return false;
    }

    public bool IsWait()
    {
        if (m_state == EState.Wait) return true;
        return false;
    }
    //吹っ飛び状態にする
    public void BlowAwayNow(int hitReceiveID)
    {
        m_hitReceivePlayerID = hitReceiveID;
        m_state = EState.BlowAway;
    }
    // 入力フラグ（クリティカルヒット用）
    public bool IsInputFlagParts(EInput eInput)
    {
        return m_isInputFlg[(int)eInput];
    }
    // 入力フラグ（クリティカルヒット用）
    public bool IsInputAllFlags()
    {
        for (int i = 0; i < m_isInputFlg.Length; ++i)
        {
            if (m_isInputFlg[i]) return true;
        }
        return false;
    }

    // 最後のアタックしたPlayerID
    public int LastAttackPlayerID()
    {
        return m_hitReceivePlayerID;
    }

    // 勝利
    public void Win()
    {
        m_state = EState.Win;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //吹っ飛び中に地面に当たると吹っ飛び状態解除

        if (DebugModeGame.GetProperty().m_debugMode && DebugModeGame.GetProperty().m_debugPlayerEnable) return;
        if (m_state != EState.Init) return;
        if (LayerMask.LayerToName(other.gameObject.layer) != "Ground") return;
        m_state = EState.Wait;

        //        Destroy(GetComponent<BoxCollider>());
    }
}

