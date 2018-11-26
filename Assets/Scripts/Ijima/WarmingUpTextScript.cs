using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarmingUpTextScript : MonoBehaviour {

    public Text targetText;
    public Text targetText2;
    public Text target2Text;
    public Text target2Text2;
    private WarmingSystem Warm;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Warm.NextSceneTimer - Time.deltaTime==Warm.NextSceneTimer) {
            targetText.text = "DNCは寝相の悪さで相手を場外へ吹っ飛ばし、";
            targetText2.text = "最後の一人を目指すスポーツだ！";
            target2Text.text = "DNCは寝相の悪さで相手を場外へ吹っ飛ばし、";
            target2Text2.text = "最後の一人を目指すスポーツだ！";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 33)
        {
            targetText.text = "寝る前によく体を解しておかないとな！";
            targetText2.text = "左スティックを左右に倒して寝転がろう！";
            target2Text.text = "寝る前によく体を解しておかないとな！";
            target2Text2.text = "左スティックを左右に倒して寝転がろう！";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 28)
        {
            targetText.text = "よし、次は左スティックを上下に倒してみよう！";
            targetText2.text = "上下に倒すと、体の向きを変えることが出来るぞ！";
            target2Text.text = "よし、次は左スティックを上下に倒してみよう！";
            target2Text2.text = "上下に倒すと、体の向きを変えることが出来るぞ！";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 23)
        {
            targetText.text = "さてこれで寝転がる動きは完璧だ！";
            targetText2.text = "";
            target2Text.text = "さてこれで寝転がる動きは完璧だ！";
            target2Text2.text = "";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 18)
        {
            targetText.text = "次により悪い寝相のために腕を解そう！";
            targetText2.text = "Xボタンを押しっぱなしにしてみよう！";
            target2Text.text = "次により悪い寝相のために腕を解そう！";
            target2Text2.text = "Xボタンを押しっぱなしにしてみよう！";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 13)
        {
            targetText.text = "よし！xボタンを離してみよう！";
            targetText2.text = "これで右腕が動くぞ！";
            target2Text.text = "よし！xボタンを離してみよう！";
            target2Text2.text = "これで右腕が動くぞ！";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 8)
        {
            targetText.text = "同じように、XYABボタンでそれぞれの手足を動かせるぞ！";
            targetText2.text = "ボタンを使って寝相の悪さで敵を吹っ飛ばせ！";
            target2Text.text = "同じように、XYABボタンでそれぞれの手足を動かせるぞ！";
            target2Text2.text = "ボタンを使って寝相の悪さで敵を吹っ飛ばせ！";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 3)
        {
            targetText.text = "さ、これで寝る前の体操は終わり！";
            targetText2.text = "";
            target2Text.text = "さ、これで寝る前の体操は終わり！";
            target2Text2.text = "";
        }
        if (Warm.NextSceneTimer - Time.deltaTime == 0)
        {
            targetText.text = "良く寝て悪い寝相を披露しておくれよ！！";
            targetText2.text = "";
            target2Text.text = "良く寝て悪い寝相を披露しておくれよ！！";
            target2Text2.text = "";
        }
    }
}
