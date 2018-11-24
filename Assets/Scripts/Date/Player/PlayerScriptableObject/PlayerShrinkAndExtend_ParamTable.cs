using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Player/Create Param ShrinkAndExtend", fileName = "PlayerShrinkAndExtend_ParamTable")]
public class PlayerShrinkAndExtend_ParamTable : ScriptableObject
{
    [SerializeField]
    public float AddLerpTime;
    [SerializeField]
    [Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    public Vector3[] extendPower = new Vector3[(int)PlayerExtendAndShrink.EShrinkPoint.Max];
    [SerializeField]
    [Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    public Vector3[] extendAddPower = new Vector3[(int)PlayerExtendAndShrink.EShrinkPoint.Max];
    [SerializeField]
    [Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    public Vector3[] extendMaxPower = new Vector3[(int)PlayerExtendAndShrink.EShrinkPoint.Max];
    
}