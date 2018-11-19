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
    //注視するポイントのTransformを保持
    public Transform TargetTrans;
    //戻る用のカメラのtransformを保持
    private Transform SaveCameraPos;
    //Zoomするスピード
    public float Speed = 0.25f;
    //一定時間たつと0にするために現在のスピードを確保
    private float NowSpeed = 0;
    //Zoomにかかる最大時間
    public float ZoomMaxTime = 0.4f;
    //Zoomにかけている現在の時間
    public float ZoomNowTime = 0;
    //Zoomするかのフラグ
    public bool ZoomFlag = false;
    //Zoomしてから待機する現在の時間
    public float WaitNowTime = 0;
    //Zoomしてから待機する最大時間
    public float WaitMaxTime = 1.0f;
    public bool FocusMode = false;
    private GameTime gametime;
    public float test = 0;
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
        try
        {
            gametime = GameObject.Find("GameTime").GetComponent<GameTime>();

        }
        catch
        {
            gametime = null;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (!FocusMode)
        {
            PickupCamera();
        }
        else
        {
            FocusCamera();
        }
        //StalkingCamera();
    }
    public void ZoomStart()
    {
        ZoomFlag = true;
    }
    public void FocusStart()
    {
        ZoomFlag = false;
        P_Camera.depth = 0;
        CameraTrans.position = new Vector3(0, 90, -60);
        NowSpeed = Speed;
        ZoomNowTime = 0;
        WaitNowTime = 0;
        FocusMode = true;
    }
    //ヒットストップ用にズームするカメラ設定
    private void PickupCamera()
    {
        if(P_Camera!=null)
        {
            if (ZoomFlag)
            {
                P_Camera.depth = 5;
                ZoomNowTime += Time.deltaTime;
                if (ZoomNowTime >= ZoomMaxTime)
                {
                    //ZoomNowTime = ZoomMaxTime;
                    NowSpeed = 0;
                    WaitNowTime += Time.deltaTime;
                    gametime.SlowDown();
                    if (WaitNowTime >= WaitMaxTime)
                    {
                        ZoomFlag = false;
                    }
                }
                CameraTrans.LookAt(TargetTrans);
                CameraTrans.Rotate(new Vector3(-10.0f, 0.0f, 0.0f));
                CameraTrans.transform.position = Vector3.Lerp(
                    SaveCameraPos.transform.position,
                    TargetTrans.position,
                    ZoomNowTime * NowSpeed
                );
            }
            else
            {
                P_Camera.depth = 0;
                CameraTrans.position = new Vector3(0, 90, -60);
                NowSpeed = Speed;
                ZoomNowTime = 0;
                WaitNowTime = 0;
            }
        }
        

    }
    //フォーカスを当てる時用のカメラ設定
    private void FocusCamera()
    {
        P_Camera.depth = 5;
        ZoomNowTime += Time.deltaTime;
        if (ZoomNowTime >= ZoomMaxTime)
        {
            //ZoomNowTime = ZoomMaxTime;
            NowSpeed = 0;
        }
        CameraTrans.LookAt(TargetTrans);
        CameraTrans.Rotate(new Vector3(-10.0f, 0.0f, 0.0f));
        CameraTrans.transform.position = Vector3.Lerp(
            SaveCameraPos.transform.position,
            TargetTrans.position,
            ZoomNowTime * NowSpeed
        );
    }
    private void StalkingCamera()
    {
        //CameraTrans.LookAt(TargetTrans);
        CameraTrans.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        var desiredPos = TargetTrans.position - TargetTrans.forward * -10 + Vector3.up* 2;
        CameraTrans.transform.position = Vector3.Lerp(
            SaveCameraPos.transform.position ,
            desiredPos,
           1
        );
    }
    public void CameraDelete()
    {
        if (CameraClone != null)
        {
            Destroy(CameraClone);
            CameraClone = null;
        }

    }
}
