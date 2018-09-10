using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCollision : MonoBehaviour {

    public GameObject Collsion;

    private Rigidbody Rb;

    private bool WobblyFlg;

    private int MaxWobbly = 10;

    private float Angle;
    private float Speed = 5.0f;
    private float Maxangle = 30.0f;
    private float Minangle = -30.0f;
    private int Count = 0;
    

    public enum Direction
    {
        Right,
        Left
    }

    public Direction Status;

    void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
        Rb.useGravity = false;
        WobblyFlg = false;
        
    }

    void OnTriggerExit(Collider Collsion)
    {
        WobblyFlg = true;
        Debug.Log("hanareta");
    }

    void Update()
    {
        if (WobblyFlg)
        {
            switch (Status) {
                case Direction.Left:
                    Angle += Speed;
                    transform.eulerAngles = new Vector3(0, 0, Angle);
                    if (Angle >= Maxangle)
                    {
                        Debug.Log("left");
                        Status = Direction.Right;
                        Count += 1;
                    }
                    break;
                case Direction.Right:
                    Angle -= Speed;
                    transform.eulerAngles = new Vector3(0, 0, Angle);
                    if (Angle <= Minangle)
                    {
                        Debug.Log("right");
                        Status = Direction.Left;
                        Count += 1;
                    }
                    break;
            }
            if (Count >= MaxWobbly)
            {
                Rb.useGravity = true;
                Rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
        }
        
    }
}
