using StarterAssets;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;

public class CharacterArmourColour_Editor : MonoBehaviour
{
    // The GameObject to instantiate.
    public ColourScriptableObject entityToSpawn;

    [Header("Set true if the starting colour should be random")]
    // Random Colour Of the Character Armour when start
    public bool randomColourOnStart = true;


    [Header("Set the Name of The Colour and Colour Value Before creating it")]
    [Tooltip("Set the Name of The Colour and Colour value")]
    // Colour Of the Character Armour
    public ColourData _colour;

    [SerializeField]
    [Header("For Editing existing colours on the list, right click the Colour Scriptable Object inside the list, then select Properties")]
    public List<ColourScriptableObject> colourDataList = new List<ColourScriptableObject>();

    [Header("Variables Used to find the Material")]
    // Specify the name of the Tag that contains the material you want to change
    [Tooltip("Tag containing the Player")]
    public string targetTag = "Player";

    [HideInInspector]
    [SerializeField]
    private int selectedColourIndex; // Index of the selected ColourScriptableObject in colourDataList
    GameObject _characterArmourObj;

    private void Awake()
    {
        colourDataList = new List<ColourScriptableObject>(); // Initialize the colourDataList

        FindCharacterArmour();

        // Generate a random color
        UnityEngine.Color randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);

        if (randomColourOnStart)
        {
            SetColourToCharacterArmour(randomColor);
        }

        

    }


    private void SpawnArmourColour()
    {
        FindCharacterArmour();
        // Creates an instance of the ScriptableObject
        ColourScriptableObject currentEntity = ScriptableObject.CreateInstance<ColourScriptableObject>();

        // Set the ColourData for the new entity
        currentEntity.colourData = new ColourData(_colour.Name, _colour.Colour);

        // Sets the name of the instantiated entity.
        currentEntity.name = _colour.Name;

        colourDataList.Add(currentEntity);
    }

    private void FindCharacterArmour()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject taggedObject in taggedObjects)
        {
            Transform[] children = taggedObject.GetComponentsInChildren<Transform>(true);

            foreach (Transform child in children)
            {
                if (child.name == "Armature_Mesh") // Replace "Armature_Mesh" with the appropriate name of the object containing the material
                {
                    // Store the reference to the character armour object
                    _characterArmourObj = child.gameObject;
                    Debug.Log("Found Armour object with name: " + _characterArmourObj.name + " in " + taggedObject.name);
                    return;
                }
            }
        }

        Debug.LogWarning("No object found with name: " + "Armature_Mesh"); // Replace "Armature_Mesh" with the appropriate name of the object containing the material
    }

    public void SetColourToCharacterArmourFromList()
    {
        FindCharacterArmour();
        SkinnedMeshRenderer _mesh = _characterArmourObj.GetComponent<SkinnedMeshRenderer>();

        // Check if SkinnedMeshRenderer component exists
        if (_mesh != null)
        {
            // Retrieve all materials from the SkinnedMeshRenderer
            Material[] materials = _mesh.sharedMaterials;

            // Loop through each material and modify the base color
            foreach (Material material in materials)
            {
                // Check if the material has the URP Lit shader
                if (material.shader.name.Contains("Universal Render Pipeline/Lit"))
                {
                    // Set the new base color to the material's shader properties
                    material.SetColor("_BaseColor", colourDataList[selectedColourIndex].colourData.Colour);
                }
                else
                {
                    Debug.LogWarning("Material: " + material.name + " does not use the URP Lit shader.");
                }
            }
        }
    }

    private void SetColourToCharacterArmour(UnityEngine.Color _colour)
    {
        SkinnedMeshRenderer _mesh = _characterArmourObj.GetComponent<SkinnedMeshRenderer>();

        // Check if SkinnedMeshRenderer component exists
        if (_mesh != null)
        {
            // Retrieve all materials from the SkinnedMeshRenderer
            Material[] materials = _mesh.materials;

            // Loop through each material and modify the base color
            foreach (Material material in materials)
            {
                // Check if the material has the URP Lit shader
                if (material.shader.name.Contains("Universal Render Pipeline/Lit"))
                {
                    // Set the new base color to the material's shader properties
                    material.SetColor("_BaseColor", _colour);
                }
                else
                {
                    Debug.LogWarning("Material: " + material.name + " does not use the URP Lit shader.");
                }
            }
        }
    }


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


}
