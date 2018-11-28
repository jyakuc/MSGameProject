using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmingUpTextScript : MonoBehaviour {

    public Text targetText;
    public Text targetText2;
    public Text target2Text;
    public Text target2Text2;
    public WarmingSystem Warm;
    private int textnum;
    // Use this for initialization
    void Start () {
        textnum = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (textnum == 0) {
            targetText.text = "DNCは寝相の悪さで相手を場外へ吹っ飛ばし、";
            targetText2.text = "最後の一人を目指すスポーツだ！";
            target2Text.text = "DNCは寝相の悪さで相手を場外へ吹っ飛ばし、";
            target2Text2.text = "最後の一人を目指すスポーツだ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 55 && textnum==1)
        {
            targetText.text = "寝る前によく体を解しておかないとな！";
            targetText2.text = "左スティックを左右に倒して寝転がろう！";
            target2Text.text = "寝る前によく体を解しておかないとな！";
            target2Text2.text = "左スティックを左右に倒して寝転がろう！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 45 && textnum == 2)
        {
            targetText.text = "よし、次は左スティックを上下に倒してみよう！";
            targetText2.text = "上下に倒すと、体の向きを変えることが出来るぞ！";
            target2Text.text = "よし、次は左スティックを上下に倒してみよう！";
            target2Text2.text = "上下に倒すと、体の向きを変えることが出来るぞ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 35 && textnum == 3)
        {
            targetText.text = "さてこれで寝転がる動きは完璧だ！";
            targetText2.text = "";
            target2Text.text = "さてこれで寝転がる動きは完璧だ！";
            target2Text2.text = "";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 30 && textnum == 4)
        {
            targetText.text = "次により悪い寝相のために腕を解そう！";
            targetText2.text = "Xボタンを押しっぱなしにしてみよう！";
            target2Text.text = "次により悪い寝相のために腕を解そう！";
            target2Text2.text = "Xボタンを押しっぱなしにしてみよう！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 25 && textnum == 5)
        {
            targetText.text = "よし！xボタンを離してみよう！";
            targetText2.text = "これで右腕が動くぞ！";
            target2Text.text = "よし！xボタンを離してみよう！";
            target2Text2.text = "これで右腕が動くぞ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 20 && textnum == 6)
        {
            targetText.text = "同じように、XYABボタンでそれぞれの手足を動かせるぞ！";
            targetText2.text = "ボタンを使って寝相の悪さで敵を吹っ飛ばせ！";
            target2Text.text = "同じように、XYABボタンでそれぞれの手足を動かせるぞ！";
            target2Text2.text = "ボタンを使って寝相の悪さで敵を吹っ飛ばせ！";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 10 && textnum == 7)
        {
            targetText.text = "さ、これで寝る前の体操は終わり！";
            targetText2.text = "";
            target2Text.text = "さ、これで寝る前の体操は終わり！";
            target2Text2.text = "";
            textnum++;
        }
        if (Warm.NextSceneTimer - Time.deltaTime <= 7 && textnum == 8)
        {
            targetText.text = "良く寝て悪い寝相を披露しておくれよ！！";
            targetText2.text = "";
            target2Text.text = "良く寝て悪い寝相を披露しておくれよ！！";
            target2Text2.text = "";
            textnum++;
        }
    }
}
