#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterArmourColour_Editor))]
public class CharacterArmourColourEditor : Editor
{
    private SerializedProperty colourDataListProperty;
    private SerializedProperty selectedColourIndexProperty;

    private void OnEnable()
    {
        colourDataListProperty = serializedObject.FindProperty("colourDataList");
        selectedColourIndexProperty = serializedObject.FindProperty("selectedColourIndex");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();

        CharacterArmourColour_Editor spawner = (CharacterArmourColour_Editor)target;

        selectedColourIndexProperty.intValue = EditorGUILayout.Popup("Selected Colour", selectedColourIndexProperty.intValue, GetColourNames(spawner.colourDataList));

        EditorGUILayout.Space();

        EditorGUILayout.Space();

        if (GUILayout.Button("Create New Armour Colour"))
        {
            spawner.SpawnArmourColour();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Set Selected Colour as the Character Armour Colour"))
        {
            spawner.SetColourToCharacterArmourFromList();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private string[] GetColourNames(List<ColourScriptableObject> colourList)
    {
        string[] colourNames = new string[colourList.Count];
        for (int i = 0; i < colourList.Count; i++)
        {
            colourNames[i] = colourList[i].name;
        }
        return colourNames;
    }
}
#endif