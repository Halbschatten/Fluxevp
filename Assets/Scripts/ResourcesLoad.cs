using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourcesLoad : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private GameObject prefab;
#endif
    [SerializeField] private string assetBundleName = "environmentassetbundle";
    [SerializeField] private string assetName = "Environment_Prefab";

    void Awake()
    {
#if UNITY_EDITOR
        LoadFromEditor();
#else
        LoadFromAssetBundle();
#endif
    }

    private void LoadFromEditor()
    {
#if UNITY_EDITOR
        if (prefab != null)
        {
            Instantiate(prefab, transform);
        }
        else
        {
            Debug.LogError("Prefab reference is missing.");
        }
#endif
    }

    private void LoadFromAssetBundle()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        // Load the asset bundle
        var assetBundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, assetBundleName));
        yield return assetBundleRequest;

        if (assetBundleRequest.assetBundle != null)
        {
            // Load the asset from the bundle
            var assetRequest = assetBundleRequest.assetBundle.LoadAssetAsync<GameObject>(assetName);
            yield return assetRequest;

            if (assetRequest.asset != null)
            {
                // Instantiate the loaded asset
                Instantiate(assetRequest.asset, transform);
            }
            else
            {
                Debug.LogError("Failed to load asset from the asset bundle: " + assetName);
            }

            // Unload the asset bundle
            assetBundleRequest.assetBundle.Unload(false);
        }
        else
        {
            Debug.LogError("Failed to load asset bundle: " + assetBundleName);
        }
    }
}