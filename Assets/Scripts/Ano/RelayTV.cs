using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//再生状態でなくても実行されるようにする
[ExecuteInEditMode()]
public class RelayTV : MonoBehaviour {

    public Material material;
    public GameObject Warming;
    public GameObject PickUp;
    public int PlayerNum = 0;
    private bool WarmingFlag = false;
    private void Start()
    {
        if(!WarmingFlag)
        {
            Warming.SetActive(false);
        }
        else
        {
            PickUp.SetActive(false);
        }
    }
    private void Update()
    {
    }
    public void SetPlayerNum(int num)
    {
        PlayerNum = num;
    }
    public int GetPlayerNum()
    {
        return PlayerNum;
    }
    public void SetWarmingMode()
    {
        WarmingFlag = true;
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination, material);
    }
    
}
