using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtParts
{
    public Transform obj;
    public Vector3 initPos;
    public Quaternion initRota;
    public Vector3 initScale;

    public Vector3 savePos;
    public Quaternion saveRota;
    public Vector3 saveScale;

    public ArtParts(Transform _obj)
    {
        obj = _obj;
    }
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

public struct CostParts
{
    public float allCost;
    public float chestCost;
    public float headCost;
    public float rightArmCost;
    public float leftArmCost;
    public float rightFootCost;
    public float leftFootCost;
}

public class ArtGrading : MonoBehaviour {
    private GameObject m_armature;

    // それぞれのパーツのTransform
    private List<ArtParts> m_partsList = new List<ArtParts>();

    [SerializeField]
    private CostParts m_cost;
    public CostParts Cost
    {
        get { return m_cost; }
    }
	// Use this for initialization
	void Start () {
        m_armature = transform.GetChild(0).gameObject;

   
        m_partsList.Add(new ArtParts(m_armature.transform.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Belly].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Chest].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Neck].obj.GetChild(0)));

        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Chest].obj.GetChild(1)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.ShoulderL].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.UpperL].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.ArmL].obj.GetChild(0)));

        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Chest].obj.GetChild(2)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.ShoulderR].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.UpperR].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.ArmR].obj.GetChild(0)));

        m_partsList.Add(new ArtParts(m_armature.transform.GetChild(1)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Lower].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.AssL].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.ThighsL].obj.GetChild(0)));

        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.Lower].obj.GetChild(1)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.AssR].obj.GetChild(0)));
        m_partsList.Add(new ArtParts(m_partsList[(int)Parts.ThighsR].obj.GetChild(0)));
        
        for(int i = 0; i < m_partsList.Count; ++i)
        {
            m_partsList[i].Init();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        for(int i = 0; i < m_partsList.Count; ++i)
        {
            m_partsList[i].Save();
        }
    }

    // 採点
    public void ArtistGrading()
    {
        float cost = 0;
        for(int i = 0; i < m_partsList.Count; ++i)
        {
            // 座標の差異
            cost += Mathf.Abs(m_partsList[i].initPos.x - m_partsList[i].savePos.x);
            cost += Mathf.Abs(m_partsList[i].initPos.y - m_partsList[i].savePos.y);
            cost += Mathf.Abs(m_partsList[i].initPos.z - m_partsList[i].savePos.z);
            // 回転の差異
            cost += Mathf.Abs(m_partsList[i].initRota.eulerAngles.x - m_partsList[i].saveRota.eulerAngles.x);
            cost += Mathf.Abs(m_partsList[i].initRota.eulerAngles.y - m_partsList[i].saveRota.eulerAngles.y);
            cost += Mathf.Abs(m_partsList[i].initRota.eulerAngles.z - m_partsList[i].saveRota.eulerAngles.z);
            // スケールの差異
            cost += Mathf.Abs(m_partsList[i].initScale.x - m_partsList[i].saveScale.x);
            cost += Mathf.Abs(m_partsList[i].initScale.y - m_partsList[i].saveScale.y);
            cost += Mathf.Abs(m_partsList[i].initScale.z - m_partsList[i].saveScale.z);

            if (i <= (int)Parts.Chest)
                m_cost.chestCost += cost;
            else if (i <= (int)Parts.Head)
                m_cost.headCost += cost;
            else if (i <= (int)Parts.HandL)
                m_cost.leftArmCost += cost;
            else if (i <= (int)Parts.HandR)
                m_cost.rightArmCost += cost;
            else if (i <= (int)Parts.CalfL)
                m_cost.leftFootCost += cost;
            else if (i <= (int)Parts.CalfR)
                m_cost.rightFootCost += cost;
            m_cost.allCost += cost;
            cost = 0;
        }
    }
}
