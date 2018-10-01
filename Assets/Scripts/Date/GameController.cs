using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int m_players;
    public int Players
    {
        get { return m_players; }
    }

    public PlayerController[] m_playerCtrllers;
    public StageCreate m_stageCreate;
    public GameObject m_playerModel;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
       // m_stageCreate.Create(EStageIndex.Stage_1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerCreate(EStageIndex _stage)
    {

        for (int i = 0; i < m_players; ++i) {
            m_playerCtrllers[i] = Instantiate<GameObject>(m_playerModel).GetComponent<PlayerController>();
        }
    }
}
