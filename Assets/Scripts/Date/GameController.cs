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

    [SerializeField]
    public List<GameObject> m_deleteObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_deleteCursors = new List<GameObject>();
    
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
}
