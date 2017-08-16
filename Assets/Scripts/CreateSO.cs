using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateSO  {

    [MenuItem("Assets/Create/My Scriptable Object")]
    public static void CreateMyAsset()
    {
        StructureScriptableObject asset = ScriptableObject.CreateInstance<StructureScriptableObject>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObject/StructureScriptableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
     }
}