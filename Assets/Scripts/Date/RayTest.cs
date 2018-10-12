using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour {
    public enum RayDirection
    {
        NoHit,
        Forward,
        Back
    }

    [SerializeField]
    private float rayDistance;
    [SerializeField]
    private string layerMaskName;
    LayerMask mask;
    private RayDirection rayDirection;
    public RayDirection Dir
    {
        get { return rayDirection; }
    }
    
	// Use this for initialization
	void Start () {
        int no = LayerMask.NameToLayer(layerMaskName);
        mask = 1 << no ;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, rayDistance, mask))
        {
            Debug.Log("前");
            rayDirection = RayDirection.Forward;
        }
        else if( Physics.Raycast(transform.position,-transform.forward , rayDistance , mask))
        {
            Debug.Log("後ろ");
            rayDirection = RayDirection.Back;
        }
        else
        {
           //rayDirection = RayDirection.NoHit;
        }
        
	}
}
