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
    [SerializeField]
    private int m_debugPlayerNum;

    private List<PlayerController> m_playerObj = new List<PlayerController>();
    [SerializeField]
    public List<GameObject> m_deleteObjects = new List<GameObject>();
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
    }

    // Update is called once per frame
    void Update()
    {
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


    public void AllDeleteObjects()
    {
        for (int i = 0; i < m_deleteObjects.Capacity; i++)
        {
            Destroy(m_deleteObjects[i]);
        }
    }

    public void AddPlayer(GameObject human)
    {
        Debug.Log(human + "追加");
        m_playerObj.Add(human.GetComponent<PlayerController>());
    }
    
    // 状態別Update関数
    void StartUpdate()
    {
        if (m_gameStartFlg) return;
        if (m_playerObj.Count != m_debugPlayerNum) return;
        for (int i = 0; i < m_debugPlayerNum; ++i)
        {
            if (!m_playerObj[i].IsWait()) return;
        }

        m_gameStartFlg = true;
        m_state = EState.Main;
        for (int i = 0; i < m_debugPlayerNum; ++i)
        {
            m_playerObj[i].PlayStart();
        }
        AllDeleteObjects();

        Debug.Log("ゲームスタート");
    }

    void MainUpdate()
    {
        int deadNum = 0;
        for(int i = 0; i < m_debugPlayerNum; ++i)
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

        if(deadNum == m_debugPlayerNum - 1)
        {
            m_state = EState.Finish;
        }
    }

    void FinishUpdate()
    {
        m_state = EState.End;
        m_gameSceneController.ChangeScene();
    }
}
