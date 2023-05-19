using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build Asset Bundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectoryPath = Application.dataPath + "Assets/StreamingAssets";
        if (!Directory.Exists(assetBundleDirectoryPath))
        {
            Directory.CreateDirectory(assetBundleDirectoryPath);
        }
        try
        {
            // Specify the asset bundle name and the assets to include
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch
        {
             
        }
    }
}