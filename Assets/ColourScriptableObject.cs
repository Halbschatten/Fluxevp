using UnityEngine;
using StarterAssets;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ColourScriptableObject : ScriptableObject
{
    [SerializeField]
    public ColourData colourData;

}