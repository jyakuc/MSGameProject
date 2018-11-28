using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Player/Create Param ShrinkAndExtend", fileName = "PlayerShrinkAndExtend_ParamTable")]
public class PlayerShrinkAndExtend_ParamTable : ScriptableObject
{
    //[SerializeField]
    //public float AddLerpTime;
    //[SerializeField]
    //[Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    //public Vector3[] extendPower = new Vector3[(int)PlayerExtendAndShrink.EShrinkPoint.Max];
    //[SerializeField]
    //[Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    //public Vector3[] extendAddPower = new Vector3[(int)PlayerExtendAndShrink.EShrinkPoint.Max];
    //[SerializeField]
    //[Tooltip("0 RightHand\n1 LeftHand\n2 RightFoot\n3 LeftFoot")]
    //public Vector3[] extendMaxPower = new Vector3[(int)PlayerExtendAndShrink.EShrinkPoint.Max];

    [Header("Hand Init Power")]
    public float shrinkInitPower_Hand;
    public float extendInitPower_Hand;

    [Header("Hand Max Power")]
    public float shrinkMaxPower_Hand;
    public float extendMaxPower_Hand;

    [Header("Hand Add Power")]
    public float shrinkAddPower_Hand;
    public float extendAddPower_Hand;

    [Header("Foot Init Power")]
    public float shrinkInitPower_Foot;
    public float extendInitPower_Foot;

    [Header("Foot Max Power")]
    public float shrinkMaxPower_Foot;
    public float extendMaxPower_Foot;

    [Header("Foot Add Power")]
    public float shrinkAddPower_Foot;
    public float extendAddPower_Foot;

    [Header("パーツ変位スピード")]
    public Vector2 partsSpeed;
}