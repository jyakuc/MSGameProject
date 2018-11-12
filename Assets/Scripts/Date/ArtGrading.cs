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
    public void Init()
    {
        if(obj != null)
        {
            initPos = obj.position;
            initRota = obj.rotation;
            initScale = obj.localScale;
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
    ShoulderR,
    UpperL,
    UpperR,
    ArmL,
    ArmR,
    HandL,
    HandR,

    Lower,
    AssL,
    AssR,
    ThighsL,
    ThighsR,
    CalfL,
    CalfR
}

public class ArtGrading : MonoBehaviour {
    private GameObject m_armature;

    // それぞれのパーツのTransform
    [SerializeField]
    private ArtParts m_bellyParts;
    [SerializeField]
    private ArtParts m_chestParts;
    [SerializeField]
    private ArtParts m_neckParts;
    [SerializeField]
    private ArtParts m_headParts;

    [SerializeField]
    private ArtParts m_shoulderLParts;
    [SerializeField]
    private ArtParts m_upperArmL;
    [SerializeField]
    private ArtParts m_armsL;
    [SerializeField]
    private ArtParts m_handL;

    [SerializeField]
    private ArtParts m_shoulderRParts;
    [SerializeField]
    private ArtParts m_upperArmR;
    [SerializeField]
    private ArtParts m_armsR;
    [SerializeField]
    private ArtParts m_handR;

    [SerializeField]
    private ArtParts m_lowerParts;
    [SerializeField]
    private ArtParts m_assR;
    [SerializeField]
    private ArtParts m_thighsR;
    [SerializeField]
    private ArtParts m_calfR;

    [SerializeField]
    private ArtParts m_assL;
    [SerializeField]
    private ArtParts m_thighsL;
    [SerializeField]
    private ArtParts m_calfL;

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

        m_shoulderRParts.obj = m_chestParts.obj.transform.GetChild(2);
        m_upperArmR.obj = m_shoulderRParts.obj.GetChild(0);
        m_armsR.obj = m_upperArmR.obj.GetChild(0);
        m_handR.obj = m_armsR.obj.GetChild(0);

        m_lowerParts.obj = m_armature.transform.GetChild(1);
        m_assL.obj = m_lowerParts.obj.GetChild(0);
        m_thighsL.obj = m_assL.obj.GetChild(0);
        m_calfL.obj = m_thighsL.obj.GetChild(0);

        m_assR.obj = m_lowerParts.obj.GetChild(1);
        m_thighsR.obj = m_assR.obj.GetChild(0);
        m_calfR.obj = m_thighsR.obj.GetChild(0);

 

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
