#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MyInputManagerGenerator {

    SerializedObject serializedObject;
    SerializedProperty axesProperty;

    // コンストラクタ
	public MyInputManagerGenerator()
    {
        serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        axesProperty = serializedObject.FindProperty("m_Axes");
    }

    // InputManagerの要素を追加
    public void AddAxis(InputInfo info)
    {
        if (info.axis < 1) Debug.LogError("Axisは１以上に設定");
        SerializedProperty serializedProperty = serializedObject.FindProperty("m_Axes");
       
        // 要素増やす
        serializedProperty.arraySize++;
        serializedObject.ApplyModifiedProperties();

        SerializedProperty infoProperty = serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1);
        GetChildProperty(infoProperty, "m_Name").stringValue = info.name;
        GetChildProperty(infoProperty, "descriptiveName").stringValue = info.descriptiveName;
        GetChildProperty(infoProperty, "descriptiveNegativeName").stringValue = info.descriptiveNegativeName;
        GetChildProperty(infoProperty, "negativeButton").stringValue = info.negativeButton;
        GetChildProperty(infoProperty, "positiveButton").stringValue = info.positiveButton;
        GetChildProperty(infoProperty, "altNegativeButton").stringValue = info.altNegativeButton;
        GetChildProperty(infoProperty, "altPositiveButton").stringValue = info.altPositiveButton;
        GetChildProperty(infoProperty, "gravity").floatValue = info.gravity;
        GetChildProperty(infoProperty, "dead").floatValue = info.dead;
        GetChildProperty(infoProperty, "sensitivity").floatValue = info.sensitivity;
        GetChildProperty(infoProperty, "snap").boolValue = info.snap;
        GetChildProperty(infoProperty, "invert").boolValue = info.invert;
        GetChildProperty(infoProperty, "type").intValue = (int)info.type;
        GetChildProperty(infoProperty, "axis").intValue = info.axis - 1;
        GetChildProperty(infoProperty, "joyNum").intValue = info.joyNum;

        serializedObject.ApplyModifiedProperties();
    }

    // 各プロパティ名からプロパティ取得
    private SerializedProperty GetChildProperty(SerializedProperty parent,string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        } while (child.Next(false));

        return null;
    }

    // InputManagerの要素を初期化
    public void Clear()
    {
        axesProperty.ClearArray();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif