using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class AddressablesManager : MonoBehaviour
{
    [SerializeField] AssetReference refrenceTriangle;
    [SerializeField] AssetReference refrenceGame1;


    // public void Load()
    // {
    //     // refrenceTriangle.LoadAssetAsync<GameObject>().Completed += (operation) =>
    //     // {
    //     //     if (operation.Status == AsyncOperationStatus.Succeeded)
    //     //     {
    //     //         Instantiate(operation.Result);
    //     //     }
    //     //     else
    //     //     {
    //     //         Debug.Log("Failed to load the object");
    //     //     }
    //     // };

    //     // StartCoroutine(DownloadAsset());
    // }

    // IEnumerator DownloadAsset()
    // {
    //     var loadResourceLocationsAsync = Addressables.LoadResourceLocationsAsync(refrenceGame1);

    //     yield return loadResourceLocationsAsync;
    //     var resourceLocations = loadResourceLocationsAsync.Result;
    //     var downloadDependenciesAsync = Addressables.DownloadDependenciesAsync(resourceLocations);
    //     var totalBytes = downloadDependenciesAsync.GetDownloadStatus().TotalBytes;
    //     Debug.Log("===========================" + Addressables.GetDownloadSizeAsync(resourceLocations).GetDownloadStatus().TotalBytes + " " + downloadDependenciesAsync.GetDownloadStatus().TotalBytes);

    //     do
    //     {
    //         Debug.Log("===========================" + "" + $"Downloading update {downloadDependenciesAsync.GetDownloadStatus().DownloadedBytes}B/{totalBytes}B");
    //         Debug.Log("===========================" + downloadDependenciesAsync.Status + "  " + downloadDependenciesAsync.IsDone);
    //         yield return null;
    //     } while (!downloadDependenciesAsync.IsDone && downloadDependenciesAsync.Status != AsyncOperationStatus.Failed);


    //     Debug.Log("===========================" + downloadDependenciesAsync.Status + "  " + downloadDependenciesAsync.IsDone);

    //     yield return downloadDependenciesAsync;
    //     // StartCoroutine(LoadAssets());
    // }


    IEnumerator LoadAssets()
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(refrenceGame1, LoadSceneMode.Single, false);

        while (!handle.IsDone)
        {
            Loading.instance.UpdateProgress(handle.GetDownloadStatus().Percent);
            yield return null;
        }

        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            yield return new WaitForSecondsRealtime(5);
            yield return handle.Result.ActivateAsync();
        }
    }

    public void ButtonLoad()
    {
        StartCoroutine(LoadAssets());
    }
}

// https://100-x.s3.ap-south-1.amazonaws.com/