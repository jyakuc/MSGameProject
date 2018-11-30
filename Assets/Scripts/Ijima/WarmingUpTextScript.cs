using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmingUpTextScript : MonoBehaviour
{

    public Text targetText;
    public Text targetText2;
    public Text target2Text;
    public Text target2Text2;
    public WarmingSystem Warm;
    private int textnum;
    // Use this for initialization
    void Start()
    {
        textnum = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (textnum == 0)
        {
            SetReActive();
            targetText.text = "ようこそみんな！";
            targetText2.text = "DNCは寝相の悪さで\n相手を場外へ吹っ飛ばし、\n最後の一人を目指すスポーツだ！";
            target2Text.text = "ようこそみんな！";
            target2Text2.text = "DNCは寝相の悪さで\n相手を場外へ吹っ飛ばし、\n最後の一人を目指すスポーツだ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 65 && textnum == 1)
        {
            SetReActive();
            targetText.text = "寝る前によく体を解して\nおかないとな！";
            targetText2.text = "左スティックを左右に\n倒して寝転がろう！";
            target2Text.text = "寝る前によく体を解して\nおかないとな！";
            target2Text2.text = "左スティックを左右に\n倒して寝転がろう！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 50 && textnum == 2)
        {
            SetReActive();
            targetText.text = "よし、次は左スティックを\n上下に倒してみよう！";
            targetText2.text = "上下に倒すと、体の向きを\n変えることが出来るぞ！";
            target2Text.text = "よし、次は左スティックを\n上下に倒してみよう！";
            target2Text2.text = "上下に倒すと、体の向きを\n変えることが出来るぞ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 35 && textnum == 3)
        {
            SetReActive();
            targetText.text = "さて、\nこれで寝転がる動きは完璧だ！";
            targetText2.text = "";
            target2Text.text = "さて、\nこれで寝転がる動きは完璧だ！";
            target2Text2.text = "";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 30 && textnum == 4)
        {
            SetReActive();
            targetText.text = "次により悪い寝相のために\n腕を解そう！";
            targetText2.text = "Xボタンを押しっぱなしに\nしてみよう！";
            target2Text.text = "次により悪い寝相のために\n腕を解そう！";
            target2Text2.text = "Xボタンを押しっぱなしに\nしてみよう！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 25 && textnum == 5)
        {
            SetReActive();
            targetText.text = "よし！Xボタンを離してみよう！";
            targetText2.text = "これで右手が動くぞ！";
            target2Text.text = "よし！Xボタンを離してみよう！";
            target2Text2.text = "これで右手が動くぞ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 15 && textnum == 6)
        {
            SetReActive();
            targetText.text = "同じように、\nXYABボタンで\nそれぞれの手足を動かせるぞ！";
            targetText2.text = "ボタンを使って\n寝相の悪さで敵を吹っ飛ばせ！";
            target2Text.text = "同じように、\nXYABボタンで\nそれぞれの手足を動かせるぞ！";
            target2Text2.text = "ボタンを使って\n寝相の悪さで敵を吹っ飛ばせ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 5 && textnum == 7)
        {
            SetReActive();
            targetText.text = "さ、\nこれで寝る前の体操は終わり！";
            targetText2.text = "";
            target2Text.text = "さ、\nこれで寝る前の体操は終わり！";
            target2Text2.text = "";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 0 && textnum == 8)
        {
            SetReActive();
            targetText.text = "良く寝て、\n君の寝相を披露しておくれよ！！";
            targetText2.text = "";
            target2Text.text = "良く寝て、\n君の寝相を披露しておくれよ！！";
            target2Text2.text = "";
            textnum++;
        }
    }

    void SetReActive()
    {
        targetText.gameObject.SetActive(false);
        targetText.gameObject.SetActive(true);

        targetText2.gameObject.SetActive(false);
        targetText2.gameObject.SetActive(true);

        target2Text.gameObject.SetActive(false);
        target2Text.gameObject.SetActive(true);

        target2Text2.gameObject.SetActive(false);
        target2Text2.gameObject.SetActive(true);
        AudioManager.GetInstance.PlaySE3(AUDIO.SE_Telop);
    }
}
