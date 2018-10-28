﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum EState
    {
        Start,
        Main,
        Finish
    }
    private EState m_state;
    private const int m_playerNum = 6;
    public int PlayerNum
    {
        get { return m_playerNum; }
    }
    [SerializeField]
    private int m_debugPlayerNum;
    public PlayerController[] m_playerContrllers;
    public StageCreate m_stageCreate;
    public GameObject m_playerModel;

    private List<PlayerController> m_playerObj = new List<PlayerController>();
    [SerializeField]
    public List<GameObject> m_deleteObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_deleteCursors = new List<GameObject>();

    private bool m_gameStartFlg;
    public bool StartFlg
    {
        get { return m_gameStartFlg; }
    }
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
    public bool DeleteCursorsIndex(int idx)
    {
        if (m_deleteCursors[idx] == null)
        {
            return false;
        }
        Destroy(m_deleteCursors[idx]);
        return true;
    }

    public void AddPlayer(GameObject human)
    {
        m_playerObj.Add(human.GetComponent<PlayerController>());
    }
    
    // 状態別Update関数
    void StartUpdate()
    {
        Debug.Log(m_gameStartFlg);
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
            m_playerContrllers[i].PlayStart();
        }
    }

    void MainUpdate()
    {
        for(int i = 0; i < m_debugPlayerNum; ++i)
        {
            if (!m_playerContrllers[i].IsDead()) return; 
        }

        m_state = EState.Finish;
    }

    void FinishUpdate()
    {
        SceneController.GetInstance.ChangeScene("TitleScene");
    }
}
