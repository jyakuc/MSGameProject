using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    //public enum ZoomMode { NotZoom,ZoomIn,ZoomOut};
    //cameraのプレハブ読み込み
    public GameObject PrefabCamera;
    //プレイヤー用のカメラをClone格納用
    private GameObject CameraClone;
    //Clonecameraの情報保持
    private Camera P_Camera;
    //ClonecameraのTransform保持
    private Transform CameraTrans;
    //戻る用のカメラのtransformを保持
    public Transform SaveCameraPos;
    //Zoomするスピード
    public float Speed = 0.1f;
    //一定時間たつと0にするために現在のスピードを確保
    private float NowSpeed = 0;
    //Zoomにかかる最大時間
    public float ZoomMaxTime = 0.8f;
    //Zoomにかけている現在の時間
    public float ZoomNowTime = 0;
    //Zoomするかのフラグ
    private bool ZoomFlag = false;
    //Zoomしてから待機する現在の時間
    public float WaitNowTime = 0;
    //Zoomしてから待機する最大時間
    public float WaitMaxTime = 1.0f;
    //一旦置いておく
    //private ZoomMode Zoommode=ZoomMode.NotZoom;
    private void Awake()
    {

        CameraClone = Instantiate(PrefabCamera, PrefabCamera.transform.position, PrefabCamera.transform.rotation);
        P_Camera = CameraClone.GetComponent<Camera>();
        CameraTrans = CameraClone.GetComponent<Transform>();
        SaveCameraPos = CameraTrans;
        NowSpeed = Speed;
        P_Camera.depth = 0;
        WaitNowTime = 0;
        ZoomNowTime = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PickupCamera();
    }
    public void ZoomStart()
    {
        ZoomFlag = true;
    }
    public void PickupCamera()
    {
        if(ZoomFlag)
        {
            P_Camera.depth = 2;
            ZoomNowTime += Time.deltaTime;
            if (ZoomNowTime >= ZoomMaxTime)
            {
                //ZoomNowTime = ZoomMaxTime;
                NowSpeed = 0;
                WaitNowTime += Time.deltaTime;
                if(WaitNowTime>=WaitMaxTime)
                {
                    ZoomFlag = false;
                }
            }
            CameraTrans.LookAt(this.transform);
            CameraTrans.Rotate(new Vector3(-10.0f, 0.0f, 0.0f));
            CameraTrans.transform.position = Vector3.Lerp(
                SaveCameraPos.transform.position,
                this.transform.position,
                ZoomNowTime * NowSpeed
            );
        }
        else
        {
            P_Camera.depth = 0;
            CameraTrans.position=new Vector3(0,90,-60);
            NowSpeed = Speed;
            ZoomNowTime = 0;
            WaitNowTime = 0;
        }

    } 

}
