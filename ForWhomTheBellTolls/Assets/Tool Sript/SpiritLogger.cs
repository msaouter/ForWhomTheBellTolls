#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpiritLogger : EditorWindow
{
    string csvFileName = "CSV File Name";
    bool addInScene;
    int defaltTime = 10;

    /* mettre ce que vous voulez a la place de CSVSpiritGenerator*/
    [MenuItem("Window/CSVSpiritGenerator")]
    public static void ShowWindow()
    {
        // Mettre le nom de votre classe 
        EditorWindow.GetWindow(typeof(SpiritLogger));
    }

    private void OnGUI()
    {
        GUILayout.Label("File to generate from :");
        csvFileName = EditorGUILayout.TextField("CSV File Name in the Resources Folder", csvFileName);
        defaltTime = EditorGUILayout.IntField("Time by defalt", defaltTime);
        addInScene = EditorGUILayout.Toggle("Add in scene", addInScene);
        SpiritGenerator generator = (SpiritGenerator)ScriptableObject.CreateInstance("SpiritGenerator");
        if (GUILayout.Button("Generate"))
        {
            generator.Generate(csvFileName, addInScene, defaltTime);
        }
    }
}
#endif
