using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressUpdateFlow : MonoBehaviour
{
    private long totalDownLoadSize;
   // public List<string> needLoadKeys = new List<string>();
    private List<string> GetAllKeys()
    {
        IEnumerable<IResourceLocator> locators = Addressables.ResourceLocators;
        var result = new List<string>();
        foreach (var locator in locators)
        {
            foreach (var key in locator.Keys)
            {
                result.Add(key.ToString());
            }
        }
        return result;
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Initialize(() =>
        //{
        //    var keys = GetAllKeys();
        //    CheckUpdateFullOneByOne(keys, (oSize, oKeys) =>
        //    {
        //        OnCheckFinish(oSize, oKeys);
        //    });
        //}));
        StartCoroutine(CheckUpdateFull((oSize, oKeys) =>
        {
            OnCheckFinish(oSize, oKeys);
        }));
    }
    private void OnCheckFinish(long pSize,List<string> pResKeys)
    {
        Debug.LogError("oSize" + pSize);
        Debug.LogError("oKeys" + pResKeys.Count);
        if (pSize > 0)
        {
            DoUpdateByKeys(pResKeys, () =>
            {
                Debug.LogError("下载更新资源完成");
                OnUpdateFinsih();
            });
        }
        else
        {
            Debug.LogError("不需要下载更新资源！！");
            OnUpdateFinsih();
        }
    }
    private void OnUpdateFinsih()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("1_GameMain");
    }
    public IEnumerator StartUpdate(string pKey)
    {
        string key = pKey;
        // Clear all cached AssetBundles
        // WARNING: This will cause all asset bundles to be re-downloaded at startup every time and should not be used in a production game
        // Addressables.ClearDependencyCacheAsync(key);

        //Check the download size
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
        yield return getDownloadSize;
        Debug.Log("资源大小" + getDownloadSize.Result);
        //If the download size is greater than 0, download all the dependencies.
        if (getDownloadSize.Result > 0)
        {
            AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(key);
            yield return downloadDependencies;
        }
    }
    //初始化Addressable
    IEnumerator Initialize(System.Action pOnFinish){
        Debug.Log(" Initialize >>>1");
        yield return Addressables.InitializeAsync();
        //var checkHandle = Addressables.CheckForCatalogUpdates(false);
        //yield return checkHandle;
        //Addressables.Release(checkHandle);
        pOnFinish.Invoke();
    }
    private void CheckUpdateFullOneByOne(List<object> pResKeys, System.Action<long,List<object>> onComplete)
    {
        var resultKeys = new List<object>();
        long totalSize = 0;
        int finishCount = 0;
        int totalCount = pResKeys.Count;
        for (int i = 0; i < pResKeys.Count; i++)
        {
            GetDownLoadSize(pResKeys[i], (oSize,oKey) =>
            {
                finishCount++;
                totalSize += oSize;
                if (oSize > 0)
                    resultKeys.Add(oKey);
                if (finishCount >= totalCount)
                    onComplete.Invoke(totalSize, resultKeys);
            }, () =>
            {
                finishCount++;
                if (finishCount >= totalCount)
                    onComplete.Invoke(totalSize, resultKeys);
            });
        }
    }
    IEnumerator CheckUpdateFull(System.Action<long, List<string>> onComplete)
    {
        totalDownLoadSize = 0;
        yield return Addressables.InitializeAsync();
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var keys = GetAllKeys();
        if (checkHandle.Status == AsyncOperationStatus.Succeeded)
        {
            List<string> catalogs = checkHandle.Result;
            Debug.Log("catalogs.Count ：" + catalogs.Count);
            if (catalogs != null && catalogs.Count > 0)
            {
                var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
                yield return updateHandle;
                if (updateHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var sizeHandle = Addressables.GetDownloadSizeAsync(catalogs);
                    yield return sizeHandle;
                    if (sizeHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        totalDownLoadSize = sizeHandle.Result;
                        Debug.Log("totalDownLoadSize ：" + totalDownLoadSize);
                        //Debug.Log("DownLoad Size：" + (totalDownLoadSize / 1024.0f / 1024.0f).ToString("0.00"));
                    }
                    onComplete.Invoke(totalDownLoadSize, catalogs);
                    Addressables.Release(sizeHandle);
                }
                Addressables.Release(updateHandle);
            }
            else
            {
                var sizeHandle = Addressables.GetDownloadSizeAsync(keys);
                yield return sizeHandle;
                if (sizeHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    totalDownLoadSize = sizeHandle.Result;
                    Debug.Log("totalDownLoadSize ：" + totalDownLoadSize);
                    //Debug.Log("DownLoad Size：" + (totalDownLoadSize / 1024.0f / 1024.0f).ToString("0.00"));
                }
                onComplete.Invoke(totalDownLoadSize, keys);
                Addressables.Release(sizeHandle);
            }
        }
        else
        {
            onComplete.Invoke(totalDownLoadSize, keys);
        }
        Addressables.Release(checkHandle);
    }
    private void DoUpdateByKeys(List<string> pResKeys,System.Action onComplete)
    {
        if (pResKeys.Count < 1){
            onComplete.Invoke();
            return;
        }
        int downFinishCount = 0;
        int totalCount = pResKeys.Count;
        for (int i = 0; i < pResKeys.Count; i++)
        {
            DownLoad(pResKeys[i], ()=> {
                downFinishCount++;
                if (downFinishCount >= totalCount)
                    onComplete.Invoke();
            },()=> {
                downFinishCount++;
                if (downFinishCount >= totalCount)
                    onComplete.Invoke();
            });
        }
    }
    /// <summary>
    /// 获取下载的资源大小
    /// </summary>
    public void GetDownLoadSize(object key, System.Action<long,string> onComplete, System.Action onFailed = null)
    {
        var sizeHandle = Addressables.GetDownloadSizeAsync(key.ToString());
        sizeHandle.Completed += (result) => {

            if (result.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                var totalDownLoadSize = sizeHandle.Result;
                if (onComplete != null)
                {
                    onComplete(totalDownLoadSize, key.ToString());
                }
            }
            else
            {
                if (onFailed != null)
                {
                    onFailed();
                }
            }
            Addressables.Release(sizeHandle);
        };
    }
    /// <remarks>下载指定资源</remarks>
    public AsyncOperationHandle DownLoad(object key, System.Action onComplete, System.Action onFailed = null)
    {
        var downLoadHandle = Addressables.DownloadDependenciesAsync(key.ToString(), true);
        downLoadHandle.Completed += (result) =>
        {
            if (result.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                if (onComplete != null)
                {
                    onComplete();
                }
            }
            else
            {
                if (onFailed != null)
                {
                    onFailed();
                }
            }
        };
        return downLoadHandle;
    }
}
