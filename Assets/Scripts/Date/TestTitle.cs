using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTitle : MonoBehaviour {
    [SerializeField, Tooltip("フェードの速さ"), Range(0.01f, 0.05f)]
    private float speed;

    public int SceneChangeTime;     //遅延フレーム数
    private bool SceneChangeFlg;
    private int flame;
    //private bool OneKeyFlag = false;
    private Animator TitleAnim;
    private SpriteMask fadeMask;

    public Canvas canvas;
    private Camera mainCamera;

    private void Start(){
        SceneChangeFlg = false;
        flame = 0;
        AudioManager.GetInstance.PlayBGM(AUDIO.BGM_TITLE, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
        TitleAnim = canvas.GetComponent<Animator>();
        fadeMask = GameObject.Find("CircleFadeMask").GetComponent<SpriteMask>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        fadeMask.alphaCutoff = 1;
        TitleAnim.SetBool("start", false);
    }

    void Update()
    {
        AnimatorStateInfo state = TitleAnim.GetCurrentAnimatorStateInfo(0);
        if(state.IsName("PressStart") == true)
        {
            if (canvas.worldCamera != mainCamera)
                canvas.worldCamera = mainCamera;
            if (Input.anyKeyDown == true)
            {
                SceneChangeFlg = true;
                //OneKeyFlag = true;
                TitleAnim.SetBool("start", true);
                AudioManager.GetInstance.PlaySE0(AUDIO.SE_Decision);
            }
        }
        if (SceneChangeFlg == true){
            flame++;
            if(flame > SceneChangeTime/4)
                fadeMask.alphaCutoff -= speed;
        }

        if (flame == SceneChangeTime){

            SceneController.GetInstance.ChangeScene("IntroMovieScene", 2);

        }
    }
}
