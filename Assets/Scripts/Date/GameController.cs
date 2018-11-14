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
    private const int m_playerNum = 6;
    public int PlayerNum
    {
        get { return m_playerNum; }
    }

    private List<PlayerController> m_playerObj = new List<PlayerController>();
//    [SerializeField]
//    public List<GameObject> m_deleteObjects = new List<GameObject>();
    [SerializeField]
    private GameSceneController m_gameSceneController;

    private bool m_gameStartFlg;
    public bool StartFlg
    {
        get { return m_gameStartFlg; }
    }

    /// <summary>
    /// 関数群
    /// </summary>

    private void Awake()
    {
        m_gameStartFlg = false;
        m_state = EState.Start;
    }

    // Use this for initialization
    void Start()
    {
        // m_stageCreate.Create(EStageIndex.Stage_1);

        if (DebugModeGame.GetProperty().m_debugPlayerEnable)
        {
            m_state = EState.Main;
            m_gameStartFlg = true;
        }
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
        }
    }


/*    public void AllDeleteObjects()
    {
        for (int i = 0; i < m_deleteObjects.Capacity; i++)
        {
            Destroy(m_deleteObjects[i]);
        }
    }
*/
    public void AddPlayer(GameObject human)
    {
        Debug.Log(human + "追加");
        m_playerObj.Add(human.GetComponent<PlayerController>());
    }
    
    // 状態別Update関数
    void StartUpdate()
    {
        int playerNum = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum : m_playerNum;

        if (m_gameStartFlg) return;
        if (m_playerObj.Count != playerNum) return;
        for (int i = 0; i < playerNum; ++i)
        {
            if (!m_playerObj[i].IsWait()) return;
        }

        m_gameStartFlg = true;
        m_state = EState.Main;
        for (int i = 0; i < playerNum; ++i)
        {
            m_playerObj[i].PlayStart();
        }
 //       AllDeleteObjects();

        Debug.Log("ゲームスタート");
    }

    void MainUpdate()
    {
        int deadNum = 0;
        int playerNum = DebugModeGame.GetProperty().m_debugMode ? DebugModeGame.GetProperty().m_debugPlayerNum : m_playerNum;
        for (int i = 0; i < playerNum; ++i)
        {
            if(m_playerObj[i] == null)
            {
                deadNum++;
                continue;
            }
            if (m_playerObj[i].IsDead())
            {
                deadNum++;
            }
        }

        if(deadNum == playerNum - 1)
        {
            m_state = EState.Finish;
        }
    }

    void FinishUpdate()
    {
        m_state = EState.End;
        for(int i = 0; i < m_playerObj.Count; ++i)
        {
            if (m_playerObj[i] == null) continue;
            if (m_playerObj[i].GetMyState() == PlayerController.EState.Dead) continue;
            // 芸術点採点
            ArtGrading art =  m_playerObj[i].gameObject.GetComponent<ArtGrading>();
            art.ArtistGrading();
            // コストマネージャーに登録
            FindObjectOfType<CostManager>().AddCostData(m_playerObj[i].PlayerID, art.Cost);
            Debug.Log("勝者：" + m_playerObj[i].name + " 芸術ポイント：" + art.Cost.allCost);
        }


        m_gameSceneController.ChangeScene();
    }
}
