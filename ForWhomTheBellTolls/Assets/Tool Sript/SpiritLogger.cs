using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/*
public enum CardTypeSupported
{
    Fate,
    Chalenge,
    Object,
    Person,
    Place
}*/
public class SpiritLogger : EditorWindow
{
    string csvFileName = "CSV File Name";
    bool addInScene;

    [MenuItem("Window/CSVSpiritGenerator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SpiritLogger));
    }

    private void OnGUI()
    {
        GUILayout.Label("File to generate from :");
        csvFileName = EditorGUILayout.TextField("CSV File Name in the Resources Folder", csvFileName);
        //cardType = (CardTypeSupported)EditorGUILayout.EnumPopup("Card type of the file :", cardType);
        addInScene = EditorGUILayout.Toggle("Add in scene", addInScene);
        CardGenerator generator = (CardGenerator)ScriptableObject.CreateInstance("CardGenerator");
        if (GUILayout.Button("Generate"))
        {
            generator.Generate(csvFileName, cardType, addInScene);
        }
    }

    
}
