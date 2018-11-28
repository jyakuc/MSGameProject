using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSmoke : MonoBehaviour {
    private ParticleSystem ParticleData;
    private MeshRenderer IceWall;
    //オーディオ
    public AudioManager audio;
    //1度再生されているかの判定用
    private bool PlayFlag = false;
    // Use this for initialization
    void Start () {
        ParticleData = GetComponent<ParticleSystem>();
        //自分の上の親のMeshRendererを取得
        IceWall = this.transform.parent.gameObject.GetComponent<MeshRenderer>();
        //particleのShapeをMeshRendererにし親のMeshRendererを入れる
        var ShapeMesh = ParticleData.shape;
        ShapeMesh.enabled = true;
        ShapeMesh.shapeType = ParticleSystemShapeType.MeshRenderer;
        ShapeMesh.meshRenderer = IceWall;
    }

    // Update is called once per frame
    void Update () {
        if(PlayFlag)
        {
            //SE再生処理を何回も呼びたい場合ここ
            //PlaysSe();
            if (!ParticleData.isPlaying)
            {
                //エラー回避多分必要ないかもだけど
                try
                {
                    Destroy(this.gameObject);
                }
                catch { }
            }
        }
    }
    // これ呼んだらエフェクト出し切って自然に消える
    public void EndEffect()
    {
        ParticleData.loop = false;
    }
    // これを呼んで再生してほしい
    public void PlaysEffect()
    {
        if(!PlayFlag)
        {
            ParticleData.Play();
            PlayFlag = true;
            //ここにSE再生書けば1度だけ処理を入れれるUpdataのif(PlayFlag)の中にかけばエフェクト再生中何回も処理を入れれる
            PlaysSe();
        }
    }

    private void PlaysSe()
    {
        //SEの種類弄るのはここ
        //AudioManager.GetInstance.PlaySE0(AUDIO.SE_Hit01);
    }
}
