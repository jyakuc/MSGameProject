using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Parameter/Player/Create Param Ray", fileName = "PlayerRay_ParamTable")]
public class PlayerRay_ParamTable : ScriptableObject
{
    // Rayの距離
    [SerializeField]
    public float rayDistance;
    // Rayの判定を行うレイヤー名
    [SerializeField]
    public string layerMaskName;
}

