#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MyInputManagerSetter {

	[MenuItem("Util/Reset InputManager")]
    public static void ResetInputManager()
    {
        Debug.Log("InputManager設定開始");
        MyInputManagerGenerator myInputManagerGenerator = new MyInputManagerGenerator();
        myInputManagerGenerator.Clear();

        for(int i = 0; i < 16; i++)
        {
            AddPlayerInputSettings(myInputManagerGenerator, i);
        }

        // デバッグ射出ボタン用
        for(int i = 0; i < 6; ++i)
        {
            AddDebugInjectionButton(myInputManagerGenerator, i);
        }
    }

    //プレイヤーごとの入力設定を追加する
    private static void AddPlayerInputSettings(MyInputManagerGenerator myInputManagerGenerator,int playerIndex)
    {
        string upKey = "", downKey = "", leftKey = "", rightKey = "",
               AKey = "", BKey = "", XKey = "", YKey = "" , STKey = "" ;
        GetAxisKey(out upKey, out downKey, out rightKey, out leftKey, out AKey, out BKey, out XKey, out YKey,out STKey , playerIndex);

        int joystickNum = playerIndex + 1;

        {
            var name = string.Format("Horizontal_Player{0}", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateAxis(name, joystickNum, 1));
            myInputManagerGenerator.AddAxis(InputInfo.CreateKeyAxis(name, rightKey, leftKey, "", ""));
        }
        {
            var name = string.Format("Vertical_Player{0}", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateAxis(name, joystickNum, 2));
            myInputManagerGenerator.AddAxis(InputInfo.CreateKeyAxis(name, downKey, upKey, "", ""));
        }
        {
            var name = string.Format("A_Player{0}", joystickNum);
            var button = string.Format("joystick {0} button 0", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateButton(name, button, AKey));
        }
        {
            var name = string.Format("B_Player{0}", joystickNum);
            var button = string.Format("joystick {0} button 1", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateButton(name, button, BKey));
        }
        {
            var name = string.Format("X_Player{0}", joystickNum);
            var button = string.Format("joystick {0} button 2", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateButton(name, button, XKey));
        }
        {
            var name = string.Format("Y_Player{0}", joystickNum);
            var button = string.Format("joystick {0} button 3", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateButton(name, button, YKey));
        }
        {
            var name = string.Format("ST_Player{0}", joystickNum);
            var button = string.Format("joystick {0} button 7", joystickNum);
            myInputManagerGenerator.AddAxis(InputInfo.CreateButton(name, button, STKey));
        }

    }
    // デバッグ射出ボタン追加
    private static void AddDebugInjectionButton(MyInputManagerGenerator myInputManagerGenerator,int num)
    {
        var name = string.Format("Injection_" + (num + 1));
        string key = "";
        switch (num)
        {
            case 0:
                key = "z";
                break;
            case 1:
                key = "x";
                break;
            case 2:
                key = "c";
                break;
            case 3:
                key = "v";
                break;
            case 4:
                key = "b";
                break;
            case 5:
                key = "n";
                break;
        }
        string button = "";

        myInputManagerGenerator.AddAxis(InputInfo.CreateButton(name, button, key));
    }

    // キーボード用割り当て
    private static void GetAxisKey(out string upKey,out string downKey,out string leftKey,out string rightKey,
        out string AKey,out string BKey , out string XKey,out string YKey, out string STKey ,int playerIndex)
    {
        upKey = "";
        downKey = "";
        leftKey = "";
        rightKey = "";
        AKey = "";
        BKey = "";
        XKey = "";
        YKey = "";
        STKey = "";

        switch (playerIndex)
        {
            case 0:
                upKey = "w";
                downKey = "s";
                leftKey = "a";
                rightKey = "d";
                AKey = "e";
                BKey = "r";
                XKey = "f";
                YKey = "g";
                STKey = "space";
                 break;
            case 1:
                upKey = "up";
                downKey = "down";
                leftKey = "left";
                rightKey = "right";
                AKey = "o";
                BKey = "p";
                XKey = "k";
                YKey = "l";
                STKey = "enter";
                break;
        }
    }
}
#endif