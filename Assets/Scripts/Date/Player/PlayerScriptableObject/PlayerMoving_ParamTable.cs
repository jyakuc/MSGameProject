using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Parameter/Player/Create Param Moving", fileName = "PlayerMoving_ParamTable")]
public class PlayerMoving_ParamTable : ScriptableObject
{
    [System.Serializable]
    public struct Force
    {
        [Tooltip("初期の力")]
        public Vector3 initForce;
        [Tooltip("減衰の力")]
        public Vector3 decayForce;
    }

    [SerializeField]
    [Tooltip("移動の回転力")]
    public Force angleForce;
    [SerializeField]
    [Tooltip("移動の移動力")]
    public Force moveForce;

    [SerializeField]
    [Tooltip("方向転換の回転力")]
    public Force dirAngleForce;

    [SerializeField]
    [Tooltip("移動の手の力")]
    public Vector3 handForce;
    //[SerializeField]
    //[Tooltip("移動の足の力")]
    //public Vector3 footForce;
}
