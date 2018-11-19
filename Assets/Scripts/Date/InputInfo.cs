using System.Collections;
using UnityEngine;
//using UnityEditor;

// 軸タイプ
public enum AxisType
{
    KeyOrMouseButton = 0,
    MouseMovement,
    JoystickAxis
}

public class InputInfo 
{
    // デフォルト設定
    public string name = "";
    public string descriptiveName = "";
    public string descriptiveNegativeName = "";
    public string negativeButton = "";
    public string positiveButton = "";
    public string altNegativeButton = "";
    public string altPositiveButton = "";

    public float gravity = 0;
    public float dead = 0;
    public float sensitivity = 1;

    public bool snap = false;
    public bool invert = false;

    public AxisType type = AxisType.KeyOrMouseButton;
    public int axis = 1;
    public int joyNum = 0;

    /// <summary>
    /// ボタン(ON or OFF)設定（JoyName = 0：キーのみ、JoyName = 1～16：コントローラー）
    /// </summary>
    /// <param name="_name">ボタンのInput</param>
    /// <param name="_positiveButton">ボタン（Joystick）</param>
    /// <param name="_altPositiveButton">キー</param>
    /// <returns></returns>
    public static InputInfo CreateButton(string _name, string _joystickName, string _keyName, int _joyNum = 0)
    {
        /*string positiveJoystickName = "";

        // キーボードのみの設定
        if (_joyNum == 0)
            positiveJoystickName = _positiveButton;
        else
            positiveJoystickName = "joystick " + _joyNum + " " + _positiveButton;
            */
        var info = new InputInfo();
        info.name = _name;
        info.positiveButton = _joystickName;
        info.altPositiveButton = _keyName;
        info.gravity = 0;
        info.dead = 0.2f;
        info.sensitivity = 1;
        info.type = AxisType.KeyOrMouseButton;
        info.joyNum = _joyNum;

        return info;
    }

    /// <summary>
    /// コントローラーアナログ入力設定
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_joystickNum"></param>
    /// <param name="_axisNum"></param>
    /// <returns></returns>
    public static InputInfo CreateAxis(string _name, int _joystickNum, int _axisNum)
    {
        var info = new InputInfo();
        info.name = _name;
        info.gravity = 0;
        info.dead = 0.2f;
        info.sensitivity = 1;
        info.type = AxisType.JoystickAxis;
        info.axis = _axisNum;
        info.joyNum = _joystickNum;

        if (_name.Contains("Vertical")) info.invert = true;
        return info;
    }

    /// <summary>
    /// キー（+と－の設定）設定
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_negativeButton"></param>
    /// <param name="_positiveButton"></param>
    /// <returns></returns>
    public static InputInfo CreateKeyAxis(string _name, string _negativeButton, string _positiveButton,
        string _altNegativeButton, string _altPositiveButton)
    {
        var info = new InputInfo();
        info.name = _name;
        info.negativeButton = _negativeButton;
        info.positiveButton = _positiveButton;
        info.altNegativeButton = _altNegativeButton;
        info.altPositiveButton = _altPositiveButton;
        info.gravity = 3;
        info.sensitivity = 1;
        info.dead = 0.001f;
        info.type = AxisType.KeyOrMouseButton;

        return info;
    }


}
