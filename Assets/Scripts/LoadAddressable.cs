using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAddressable : MonoBehaviour
{
    public List<string> keys = new List<string>() { "character" };
    public UnityEngine.UI.Image charImage;    

    // Operation handle used to load and release assets
    AsyncOperationHandle<IList<Sprite>> loadHandle;

    // Load Addressables by Label
    public void GUI_Fetch()
    {
        float x = 0, z = 0;
        loadHandle = Addressables.LoadAssetsAsync<Sprite>(
            keys, // Either a single key or a List of keys 
            addressable =>
            {
                //Gets called for every loaded asset
                if (addressable != null)
                {
                    charImage.sprite = addressable;
                }
            }, Addressables.MergeMode.Union, // How to combine multiple labels 
            false); // Whether to fail if any asset fails to load
        loadHandle.Completed += LoadHandle_Completed;
    }

    private void LoadHandle_Completed(AsyncOperationHandle<IList<Sprite>> operation)
    {
        if (operation.Status != AsyncOperationStatus.Succeeded)
            Debug.LogWarning("Some assets did not load.");
    }

    private void OnDestroy()
    {
        // Release all the loaded assets associated with loadHandle
        Addressables.Release(loadHandle);
    }
}
