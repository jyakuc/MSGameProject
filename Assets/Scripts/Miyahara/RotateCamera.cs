using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    [SerializeField]
    private float Speed;
    private float Count;
    [SerializeField]
    private GameObject MainCamera;
    [SerializeField]
    private GameObject SubCamera;
    private GameObject Cannons;

    private float Rate;
    FadeController FadeObj;
    SceneController SceneObj;
    private bool FadeFlg;
	// Use this for initialization
	void Start () {

        Rate = 1;
        Count = 0;
        SceneObj = GameObject.FindObjectOfType<SceneController>();
        FadeObj = SceneObj.transform.Find("FadeCanvas").GetComponent<FadeController>();
        Cannons = GameObject.Find("ColosseumCannons(Clone)");
        FadeFlg = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Count < 270)
        {
            this.transform.Rotate(0, Speed*Time.deltaTime, 0);
            Count += Speed * Time.deltaTime;
        }
        else if(!FadeObj.IsFade && FadeFlg == false) //フェードイン
        {
            FadeObj.gameObject.SetActive(true);
            FadeController.Begin(FadeObj.gameObject, false, Rate);
            FadeFlg = true;
            FadeObj.m_onFinished += ChangeCamera;
        }
	}

    public void ChangeCamera()
    {
        MainCamera.SetActive(!MainCamera.activeSelf);
        SubCamera.SetActive(!SubCamera.activeSelf);
        Cannons.transform.Find("Cannon6Arc").gameObject.SetActive(true);
        Cannons.transform.Find("CannonMng").gameObject.SetActive(true);
        FadeController.Begin(FadeObj.gameObject, true, Rate);
        FadeObj.m_onFinished -= ChangeCamera;
    }

}

