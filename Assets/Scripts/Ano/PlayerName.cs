using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviour {
    //名前格納用
    public string Name;
    //最大文字数
    [Range(1,5)]
    public int Max_Character = 0;
    private int CheckCount = 0;
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        CheckCharacter();
    }
    //1文字づつ文字を吐き出す用
    public char GetNameCharacterPos(int Pos)
    {
        //最大文字数より出ていなければその位置の文字を渡す
        if(Pos< Max_Character)
        {
            return Name[Pos];
        }
        //超えている場合空文字とエラーを返す
        Debug.Log("Character Count error");
        return ' ';

    }
    //名前セット用
    void SetPlayerName(string P_Name,int Max)
    {
        Name = P_Name;
        Max_Character = Max;
    }
    //文字チェック（半角大文字英数字以外が含まれているとfarceを返す
    bool CheckCharacter()
    {
        CheckCount = 0;
        //割とループが回るが空白、0～9とA～Zまでの判定ループ
        for (char SearchChar = (char)32; SearchChar < 91; SearchChar++)
        {
            if(SearchChar==(char)33)
            {
                SearchChar = (char)48;
            }
            //文字コードが9の次が記号なのでAまでスキップ
            if(SearchChar== (char)58)
            {
                SearchChar = (char)65;
            }
            //名前が格納されているところに全部判定
            for (int i = 0; i < Max_Character; i++)
            {
                if (Name[i] == SearchChar)
                {
                    CheckCount += 1;
                }
            }
        }
        //カウントが最大文字数と同じの場合正常な名前なのでtrue
        if (CheckCount==Max_Character)
        {
            return true;
        }
        //それ以外は無効な文字が入っているのでfarce
        return false;
    }
}
