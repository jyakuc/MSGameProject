using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ArtParts
{
    public Transform obj;
    public Vector3 initPos;
    public Quaternion initRota;
    public Vector3 initScale;

    public Vector3 savePos;
    public Quaternion saveRota;
    public Vector3 saveScale;
    public void Init()
    {
        if(obj != null)
        {
            initPos = obj.position;
            initRota = obj.rotation;
            initScale = obj.localScale;
        }
    }
    public void Save()
    {
        if(obj != null)
        {
            savePos = obj.position;
            saveRota = obj.rotation;
            saveScale = obj.localScale;
        }
    }
}

public enum Parts
{
    Belly,
    Chest,
    Neck,
    Head,

    ShoulderL,
    UpperL,
    ArmL,
    HandL,

    ShoulderR,
    UpperR,
    ArmR,
    HandR,

    Lower,
    AssL,
    ThighsL,
    CalfL,

    AssR,
    ThighsR,
    CalfR,
}

public class ArtGrading : MonoBehaviour {
    private GameObject m_armature;

    // それぞれのパーツのTransform
    private ArtParts m_bellyParts;
    private ArtParts m_chestParts;
    private ArtParts m_neckParts;
    private ArtParts m_headParts;
    
    private ArtParts m_shoulderLParts;
    private ArtParts m_upperArmL;
    private ArtParts m_armsL;
    private ArtParts m_handL;
    
    private ArtParts m_shoulderRParts;
    private ArtParts m_upperArmR;
    private ArtParts m_armsR;
    private ArtParts m_handR;
    
    private ArtParts m_lowerParts;
    private ArtParts m_assR;
    private ArtParts m_thighsR;
    private ArtParts m_calfR;
    
    private ArtParts m_assL;
    private ArtParts m_thighsL;
    private ArtParts m_calfL;

    [SerializeField]
    private List<ArtParts> m_partsList = new List<ArtParts>();
	// Use this for initialization
	void Start () {
        m_armature = transform.GetChild(0).gameObject;

        m_bellyParts.obj = m_armature.transform.GetChild(0);
        m_chestParts.obj = m_bellyParts.obj.GetChild(0);
        m_neckParts.obj = m_chestParts.obj.GetChild(0);
        m_headParts.obj = m_neckParts.obj.GetChild(0);
        m_partsList.Add(m_bellyParts);
        m_partsList.Add(m_chestParts);
        m_partsList.Add(m_neckParts);
        m_partsList.Add(m_headParts);

        m_shoulderLParts.obj = m_chestParts.obj.GetChild(1);
        m_upperArmL.obj = m_shoulderLParts.obj.GetChild(0);
        m_armsL.obj = m_upperArmL.obj.GetChild(0);
        m_handL.obj = m_armsL.obj.GetChild(0);
        m_partsList.Add(m_shoulderLParts);
        m_partsList.Add(m_upperArmL);
        m_partsList.Add(m_armsL);
        m_partsList.Add(m_handL);

        m_shoulderRParts.obj = m_chestParts.obj.transform.GetChild(2);
        m_upperArmR.obj = m_shoulderRParts.obj.GetChild(0);
        m_armsR.obj = m_upperArmR.obj.GetChild(0);
        m_handR.obj = m_armsR.obj.GetChild(0);
        m_partsList.Add(m_shoulderRParts);
        m_partsList.Add(m_upperArmR);
        m_partsList.Add(m_armsR);
        m_partsList.Add(m_handR);

        m_lowerParts.obj = m_armature.transform.GetChild(1);
        m_assL.obj = m_lowerParts.obj.GetChild(0);
        m_thighsL.obj = m_assL.obj.GetChild(0);
        m_calfL.obj = m_thighsL.obj.GetChild(0);
        m_partsList.Add(m_lowerParts);
        m_partsList.Add(m_assL);
        m_partsList.Add(m_thighsL);
        m_partsList.Add(m_calfL);

        m_assR.obj = m_lowerParts.obj.GetChild(1);
        m_thighsR.obj = m_assR.obj.GetChild(0);
        m_calfR.obj = m_thighsR.obj.GetChild(0);
        m_partsList.Add(m_assR);
        m_partsList.Add(m_thighsR);
        m_partsList.Add(m_calfR);

        for(int i = 0; i < m_partsList.Capacity; ++i)
        {
            m_partsList[i].Init();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        for(int i = 0; i < m_partsList.Capacity; ++i)
        {
            m_partsList[i].Save();
        }
    }
}
