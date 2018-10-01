using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCollision : MonoBehaviour {
    

    private bool WobblyFlg;
    private bool gravityFlg;
    private int MaxWobbly = 10;

    private float Angle;
    private float Speed = 5.0f;
    private float Maxangle = 30.0f;
    private float Minangle = -30.0f;
    private int Count = 0;


    private float gravitySpeed = -0.1f;

    public enum Direction
    {
        Right,
        Left
    }

    public Direction Status;

    void Start()
    {
       
        WobblyFlg = false;
        gravityFlg = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "OutsideColl")
        {
            WobblyFlg = true;
            Debug.Log("hanareta");
        }
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
                gravityFlg = true;
                
            }
            if (this.transform.position.y <= -50)
            {
                Destroy(this.gameObject);
            }
        }

        if (gravityFlg)
        {
            transform.position += new Vector3(0, gravitySpeed, 0);
            gravitySpeed -= 0.008f;
        }
        
    }
}
