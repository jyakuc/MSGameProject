using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    //スコア格納用
    public int ScoreNum = 0;
    //最大文字数
    [Range(1, 3)]
    public int Max_Character = 0;
    //文字に変換用
    public string ScoreText;
    private int CheckCount = 0;
    // Use this for initialization
    private void Awake()
    {
        ScoreText = "   ";
        GetScoreData(ScoreNum);
        //ScoreText = ScoreNum.ToString();
    }

    // Update is called once per frame
    void Update () {
        CheckCharacter();

    }
    public void GetScoreData(int Num)
    {
        ScoreNum = Num;
        if (ScoreNum > 999)
        {
            ScoreText = "999";
        }
        else if(ScoreNum>=100)
        {
            ScoreText = ScoreNum.ToString();
        }
        else if (ScoreNum>=10)
        {
            ScoreText = ScoreNum.ToString();
            ScoreText = ScoreText.Insert(0, " ");
        }
        else if (ScoreNum>= 0)
        {
            ScoreText = ScoreNum.ToString();
            ScoreText = ScoreText.Insert(0, "  ");


        }


    }
    public char GetTextPos(int Pos)
    {
        //最大文字数より出ていなければその位置の文字を渡す
        if (Pos < Max_Character)
        {

            return ScoreText[Pos];
        }
        //超えている場合空文字とエラーを返す
        Debug.Log("Character Count error");
        return ' ';

    }
    //文字チェック（半角大文字英数字以外が含まれているとfarceを返す
    bool CheckCharacter()
    {
        CheckCount = 0;
        //0～9までの判定ループ
        for (char SearchChar = (char)32; SearchChar < 58; SearchChar++)
        {
            if (SearchChar == (char)33)
            {
                SearchChar = (char)48;
            }
            //名前が格納されているところに全部判定
            for (int i = 0; i < Max_Character; i++)
            {
                if (ScoreText[i] == SearchChar)
                {
                    CheckCount += 1;
                }
            }
        }
        //カウントが最大文字数と同じの場合正常な名前なのでtrue
        if (CheckCount == Max_Character)
        {
            return true;
        }
        //それ以外は無効な文字が入っているのでfarce
        return false;
    }
}
