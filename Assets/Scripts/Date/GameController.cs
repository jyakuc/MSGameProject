using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum EState
    {
        Start,
        Main,
        Finish,
        End
    }
    private EState m_state;
    public EState GameState
    {
        get { return m_state; }
    }

    private const int m_playerNum = 6;
    public int PlayerNum
    {
        get { return m_playerNum; }
    }
    private List<PlayerController> m_playerObj = new List<PlayerController>();
    private GameSceneController m_gameSceneController;
    private CostManager m_costManager;
    [SerializeField]
    private GameUIScripts m_gameUIScripts;

    private PlayerController m_winnerPlayer;
    /// <summary>
    /// 関数群
    /// </summary>

    // Layer　ID
    //　TODO : レイヤーの整理したほうがいいかも～～
    int playerLayer1;
    int playerLayer2;
    int playerLayer3;
    int playerLayer4;
    int playerLayer5;
    int playerLayer6;
    int[] PlayerLayers;

    private void Awake()
    {
        m_state = EState.Start;
        // メンバで初期化入れたら怒られたので
        playerLayer1 = LayerMask.NameToLayer("Player_01");
        playerLayer2 = LayerMask.NameToLayer("Player_02");
        playerLayer3 = LayerMask.NameToLayer("Player_03");
        playerLayer4 = LayerMask.NameToLayer("Player_04");
        playerLayer5 = LayerMask.NameToLayer("Player_05");
        playerLayer6 = LayerMask.NameToLayer("Player_06");

        PlayerLayers = new int[] { playerLayer1, playerLayer2, playerLayer3, playerLayer4, playerLayer5, playerLayer6};

    }

    // Use this for initialization
    void Start()
    {
        if (DebugModeGame.GetProperty().m_debugPlayerEnable)
        {
            m_state = EState.Main;
        }
        m_gameSceneController = gameObject.GetComponent<GameSceneController>();
        m_costManager = FindObjectOfType<CostManager>();

        SetPlayerBothCollider(false); // プレイヤー同士の当たり判定無効化
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugModeGame.GetProperty().m_debugPlayerEnable) return;

        Debug.Log(m_state);
        switch (m_state)
        {
            case EState.Start:
                StartUpdate();
                break;
            case EState.Main:
                MainUpdate();
                break;
            case EState.Finish:
                FinishUpdate();
                break;
            case EState.End:
                RestartConfirmation();
                break;
        }
    }

    // プレイヤー同士のコライダーの設定 false 無効　true 有効
    private void SetPlayerBothCollider(bool enable)
    {
        foreach (var id1 in PlayerLayers)
        {
            foreach (var id2 in PlayerLayers)
            {
                Physics.IgnoreLayerCollision(id1, id2, !enable);
            }
        }
    }

    public bool IsGameStart()
    {
        if (m_state == EState.Main) return true;
        return false;
    }

    // 参加プレイヤー追加
    public void AddPlayer(GameObject human)
    {
        Debug.Log(human + "追加");
        m_playerObj.Add(human.GetComponent<PlayerController>());
    }
    
    // 状態別Update関数
    void StartUpdate()
    {
        int playerNum = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum : m_playerNum;
        
        if (m_playerObj.Count != playerNum) return;
        for (int i = 0; i < playerNum; ++i)
        {
            if (!m_playerObj[i].IsWait() && !m_playerObj[i].IsDead()) return;
        }

        m_state = EState.Main;
        SetPlayerBothCollider(true);    // プレイヤー同士の当たり判定有効化

        for (int i = 0; i < playerNum; ++i)
        {
            m_playerObj[i].PlayStart();
        }

        Debug.Log("ゲームスタート");
    }

    void MainUpdate()
    {
        int deadNum = 0;
        int playerNum = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum : m_playerNum;
        for (int i = 0; i < playerNum; ++i)
        {
            #region 旧Dead
            //if(m_playerObj[i] == null)
            //{
            //    deadNum++;
            //    continue;
            //}
            //if (m_playerObj[i].IsDead())
            //{
            //    deadNum++;
            //}
            #endregion
            if(!m_playerObj[i].gameObject.activeSelf)
            {
                deadNum++;
            }
        }

        if (deadNum >= playerNum - 1)
        {
            m_state = EState.Finish;
        }
        
    }

    void FinishUpdate()
    {
        m_state = EState.End;

        // 勝者の芸術ポイント保存
        for(int i = 0; i < m_playerObj.Count; ++i)
        {
            if (m_playerObj[i] == null) continue;
            if (m_playerObj[i].GetMyState() == PlayerController.EState.Dead) continue;
            m_playerObj[i].Win();
            m_winnerPlayer = m_playerObj[i];
            // 芸術点採点
            ArtGrading art =  m_playerObj[i].gameObject.GetComponent<ArtGrading>();
            BattlePointGrading battlePoint = m_playerObj[i].gameObject.GetComponent<BattlePointGrading>();
            art.ArtistGrading();
            // コストマネージャーに登録
            FindObjectOfType<CostManager>().SaveArtCostData(m_playerObj[i].PlayerID, art.Cost);
            Debug.Log("勝者：" + m_playerObj[i].name + " 芸術ポイント：" + art.Cost.allCost);

            // 勝者を殺すプログラム
            //m_playerObj[i].Dead();
        }

        m_gameSceneController.ChangeScene();
    }


    // リスタートかどうか確認
    void RestartConfirmation()
    {
        if (!m_gameSceneController.IsRestart()) return;
        if (!m_gameSceneController.IsRestartReady()) return;

        // UIの初期化
        m_gameUIScripts.Init();
        // ポイントリスト初期化
        m_costManager.Init();
        // プレイヤリスト初期化
        if (m_winnerPlayer)
        {
            m_winnerPlayer.Dead();
        }

        m_playerObj.Clear();
        
        m_gameSceneController.ReStart();
        m_state = EState.Start;

        SetPlayerBothCollider(false); // プレイヤー同士の当たり判定無効化

    }
}
