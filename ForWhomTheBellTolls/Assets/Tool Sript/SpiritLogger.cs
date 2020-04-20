using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpiritLogger : EditorWindow
{
    string csvFileName = "CSV File Name";
    bool addInScene;
    int defaltTime = 10;

    [MenuItem("Window/CSVSpiritGenerator")]
    public static void ShowWindow()
    {
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
