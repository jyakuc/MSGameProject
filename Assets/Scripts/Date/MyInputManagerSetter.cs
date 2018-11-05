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
    }

    //プレイヤーごとの入力設定を追加する
    private static void AddPlayerInputSettings(MyInputManagerGenerator myInputManagerGenerator,int playerIndex)
    {
        string upKey = "", downKey = "", leftKey = "", rightKey = "", AKey = "", BKey = "", XKey = "", YKey = "";
        GetAxisKey(out upKey, out downKey, out rightKey, out leftKey, out AKey, out BKey, out XKey, out YKey, playerIndex);

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
    }

    // キーボード用割り当て
    private static void GetAxisKey(out string upKey,out string downKey,out string leftKey,out string rightKey,
        out string AKey,out string BKey , out string XKey,out string YKey,int playerIndex)
    {
        upKey = "";
        downKey = "";
        leftKey = "";
        rightKey = "";
        AKey = "";
        BKey = "";
        XKey = "";
        YKey = "";

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
                break;
        }
    }
}
#endif