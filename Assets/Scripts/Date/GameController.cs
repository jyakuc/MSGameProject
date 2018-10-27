using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int m_playerNum = 6;
    public int PlayerNum
    {
        get { return m_playerNum; }
    }

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
    }

    // Use this for initialization
    void Start()
    {
        // m_stageCreate.Create(EStageIndex.Stage_1);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_gameStartFlg);
        if (m_gameStartFlg) return;
        if (m_playerObj.Count != m_playerNum) return;
        for (int i = 0; i < m_playerNum; ++i)
        {
            if (!m_playerObj[i].LifeFlag) return;
        }

        m_gameStartFlg = true;

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
}
