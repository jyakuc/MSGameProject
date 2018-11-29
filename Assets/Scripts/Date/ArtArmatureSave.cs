using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtArmatureSave : MonoBehaviour {

    const int ChildMax = 3;
    private float[] artPoint = new float[ChildMax];
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

        // Resultに必要ないスクリプト消去
        Destroy(child.GetComponent<PlayerController>());
        Destroy(child.GetComponent<PlayerCamera>());
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

    public void ChildClear()
    {
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);
    }
}
