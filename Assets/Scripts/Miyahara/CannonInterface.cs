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
    [SerializeField]
    private MyInputManager myInputManager;

    void Awake()
    {
        useLowAngle = true;

        initialFireAngle = defaultFireAngle;
        initialFireSpeed = defaultFireSpeed;

        useInitialAngle = true;


    }

    private void Start()
    {
        myInputManager = GameObject.FindObjectOfType<MyInputManager>();
        if (myInputManager == null)
            Debug.LogError("MyInputManagerÇ™ÉVÅ[ÉìÇ…Ç†ÇËÇ‹ÇπÇÒ");
    }

    void Update()
    {
        for (int i = 0; i < MaxPlayers; i++)
        {
            if (targetCursor[i] == null) continue;
            if (useInitialAngle)
                cannon.SetTargetWithAngle(targetCursor[i].transform.position, initialFireAngle, i);
            else
                cannon.SetTargetWithSpeed(targetCursor[i].transform.position, initialFireSpeed, useLowAngle, i);

            if (targetCursor[i].FireFlg)
            {
                if (Input.GetButtonDown("X_Player" + myInputManager.joysticks[i].ToString()))
                {
                    gameController.AddPlayer(cannon.FireHuman(i));
                    targetCursor[i].FireFlg = false;
                    DeleteCursor(i);
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

    public void DeleteCursor(int playerID)
    {
        Destroy(targetCursor[playerID].gameObject);
        Destroy(cannon.projectileArc[playerID].gameObject);
    }
}
