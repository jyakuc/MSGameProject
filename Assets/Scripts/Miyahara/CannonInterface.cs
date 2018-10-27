using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CannonInterface : MonoBehaviour 
{

    private const int MaxPlayers = 6;

    [SerializeField]
    Cursor[] targetCursor = new Cursor[MaxPlayers];

    [SerializeField]
    CannonController cannon;

    //[SerializeField]
    //Text timeOfFlightText;

    [SerializeField]
    float defaultFireSpeed = 35;

    [SerializeField]
    float defaultFireAngle = 45;

    private float initialFireAngle;
    private float initialFireSpeed;
    private bool useLowAngle;

    private bool useInitialAngle;

    [SerializeField]
    private GameController gameController;

    void Awake()
    {
        useLowAngle = true;

        initialFireAngle = defaultFireAngle;
        initialFireSpeed = defaultFireSpeed;

        useInitialAngle = true;

        
    }

    void Update()
    {
        for (int i = 0; i < MaxPlayers; i++)
        {
            if (targetCursor[i] == null) continue;
            if (useInitialAngle)
                cannon.SetTargetWithAngle(targetCursor[i].transform.position, initialFireAngle);
            else
                cannon.SetTargetWithSpeed(targetCursor[i].transform.position, initialFireSpeed, useLowAngle);

            if (targetCursor[i].FireFlg)
            {
                if (Input.GetButtonDown("GameController_X" + (i + 1).ToString()))
                {
                    cannon.Fire(i + 1);
                    targetCursor[i].FireFlg = false;
                    gameController.DeleteCursorsIndex(i);
                }
            }

        }
        //timeOfFlightText.text = Mathf.Clamp(cannon.lastShotTimeOfFlight - (Time.time - cannon.lastShotTime), 0, float.MaxValue).ToString("F3");
    }

    public void SetInitialFireAngle(string angle)
    {
        initialFireAngle = Convert.ToSingle(angle);
    }

    public void SetInitialFireSpeed(string speed)
    {
        initialFireSpeed = Convert.ToSingle(speed);
    }

    public void SetLowAngle(bool useLowAngle)
    {
        this.useLowAngle = useLowAngle;
    }

    public void UseInitialAngle(bool value)
    {
        useInitialAngle = value;
    }
}
