using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreCharacter : MonoBehaviour {
    //文字の数もし上限する場合変更する
    public enum CharacterPos { first, second, third};
    //このスプリプトは何個目の文字か判定する用
    public CharacterPos C_Pos;
    //文字格納用
    public char ScoreText;
    //プレイヤーネームスプリプト用
    public Score score;
    //GUIのImageのマテリアル変更用
    private Image image;
    const int Separated = 6;
    private void Awake()
    {
        //Imageを参照
        image = GetComponent<Image>();

    }
    // Use this for initialization
    void Start () {
        //Scoreスプリプトから自分の文字を取得
        ScoreText = score.GetTextPos((int)C_Pos);
        //文字をUVで設定
        UVTexCharacter();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void UVTexCharacter()
    {
        //UV変更用にMaterialを新しく作る
        Material material = new Material(Shader.Find("GUI/Text Shader"));
        //UV座標設定
        material.SetTextureScale("_MainTex", new Vector2(1.0f / 6.0f, 1.0f / 6.0f));
        material.SetTextureOffset("_MainTex", FontPosGet());

        //ImageのMaterialに新しく作ったMaterialを入れる
        image.material = material;


    }
    Vector2 FontPosGet()
    {
        //文字の0から9までの数値がアルファベットより前なので後扱いにする為に用意
        int Normalization = ScoreText;
        //文字の0から9まで判定
        if ((Normalization >= 48) && (Normalization <= 57))
        {
            Normalization += 43;
        }
        //空白と空文字の場合
        if ((Normalization == 0) || (Normalization == 32))
        {
            return new Vector2(1, 1);
        }
        //今後改良予定今は間に合わせ処理にする
        Normalization -= 64;
        int WidthPos = 0;
        Vector2 CalculatedOffset = Vector2.zero;
        while (Normalization >= 1)
        {
            if ((Normalization <= Separated) && (Normalization >= 1))
            {
                if (WidthPos == 0)
                {
                    CalculatedOffset = new Vector2(WidthPos, ((float)Separated - (float)Normalization) / (float)Separated);
                }
                else
                {
                    CalculatedOffset = new Vector2((float)WidthPos / (float)Separated, ((float)Separated - (float)Normalization) / (float)Separated);
                }


            }
            Normalization -= Separated;
            WidthPos += 1;
        }
        return CalculatedOffset;


    }
}
