#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles: MonoBehaviour
{
    [MenuItem("Assets/Build Asset Bundles")]
    static void BuildBundles()
    {
        string assetBundleDirectoryPath = "Assets/StreamingAssets";
        if (!Directory.Exists(assetBundleDirectoryPath))
        {
            Directory.CreateDirectory(assetBundleDirectoryPath);
        }
        try
        {
            // Specify the asset bundle name and the assets to include
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to build asset bundles: " + e.Message);
        }
    }
}
#endif
