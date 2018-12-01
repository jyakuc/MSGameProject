using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtArmatureSave : MonoBehaviour {
    [SerializeField]
    private Vector3 FixationPos;
    [SerializeField]
    private Vector3 FixationRotation;

    const int ChildMax = 3;
    private int[] childPlayerID = new int[ChildMax];
    private float[] artPoint = new float[ChildMax];
    private PlayerCamera destroyCamera;
    void Awake()
    {
        // ResultSceneにもってく
        DontDestroyOnLoad(gameObject);


    }

    public void InContainer(GameObject child,float point)
    {
        if (transform.childCount == ChildMax) return;
        artPoint[transform.childCount] = point;
        child.transform.parent = transform;
        childPlayerID[transform.childCount - 1] = child.GetComponent<PlayerController>().PlayerID;

        child.transform.position = FixationPos;
        child.transform.rotation = Quaternion.Euler(FixationRotation);
        // Resultに必要ないスクリプト消去
        destroyCamera = child.GetComponent<PlayerCamera>();
        Destroy(child.GetComponent<PlayerController>());
        //Destroy(child.GetComponent<PlayerCamera>());
        Destroy(child.GetComponent<PlayerExtendAndShrink>());
        Destroy(child.GetComponent<PlayerMoving>());
    }

    // 芸術ポイントが一番高いChild取得
    public Transform ComparisonArtPoint()
    {
        int maxChildNum = 0;

        maxChildNum = artPoint[0] > artPoint[1]? 0 : 1;
        maxChildNum = artPoint[maxChildNum] > artPoint[2] ? maxChildNum : 2;

        return transform.GetChild(maxChildNum);
    }
    
    // PlayerIDのArmature取得
    public Transform GetPlayerIDArmature(int playerID)
    {
        int childId = -1;
        for(int i = 0; i < childPlayerID.Length; ++i)
        {
            if (childPlayerID[i] == playerID)
                childId = i;
        }
        if (childId != -1)
            return transform.GetChild(childId);
        return null;
    }

    public void SetActives(bool active,int childNum)
    {
        if (childNum >= 3) return;
        transform.GetChild(childNum).gameObject.SetActive(active);
    }

    public void SetAllActives(bool active)
    {
        for(int i=0;i<transform.childCount;++i)
            transform.GetChild(i).gameObject.SetActive(active);
    }

    public void WinnerCameraDestroy()
    {
        destroyCamera.CameraDelete();
        Destroy(destroyCamera);
    }

    public void ChildClear()
    {
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);
    }
}
