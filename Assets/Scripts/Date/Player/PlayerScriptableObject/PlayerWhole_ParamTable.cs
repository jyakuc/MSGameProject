using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/Player/Create Param Whole", fileName = "PlayerWhole_ParamTable")]
public class PlayerWhole_ParamTable : ScriptableObject
{
    [SerializeField]
    [Tooltip("クリティカル発生率")]
    [Range(0, 100)]
    public int criticalProbability;
    [SerializeField]
    [Tooltip("通常HIｔ時の力")]
    [Range(0,10)]
    public float normalHitPower;
    [SerializeField]
    [Tooltip("クリティカルHit時の力")]
    [Range(0, 20)]
    public float criticalHitPower;

}