using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class TestGameAddressUpdate : MonoBehaviour
{
    public string Ip = "192.168.1.27";
    public int port = 101;
    public GameObject root;
    private void Start()
    {
        Debug.LogError(" Start >>>1");
        //ddressables.InternalIdTransformFunc = InternalIdTransformFunc;
        DownLoadAsset();
        //OnLoadFinish();
    }
    private string InternalIdTransformFunc(UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation location)
    {
        //判定是否是一个AB包的请求
        if (location.Data is AssetBundleRequestOptions)
        {
            //PrimaryKey是AB包的名字
            //path就是StreamingAssets/Bundles/AB包名.bundle,其中Bundles是自定义文件夹名字,发布应用程序时,复制的目录
            string InternalId_ = location.InternalId;
            //InternalId_ = InternalId_.Replace("192.168.1.126", "192.168.1.27");
            //Debug.LogError("InternalId_"+ InternalId_);
            return InternalId_;
        }
        else
        {
            string InternalId_ = location.InternalId;
            //InternalId_ = InternalId_.Replace("192.168.1.126", "192.168.1.27");
            return InternalId_;
        }
    }
    private void OnLoadFinish()
    {
        Debug.LogError(" OnLoadFinish >>>1");
        AddressLoadManager.LoadAsync("Assets/_Bundles/UIPrefabs/Test/TestPanel.prefab", (go, oAId) => {
            var go_ = go as GameObject;
            go_.transform.SetParent(root.transform,false);
        });
        //this.transform.FindChild();
    }

    public void DownLoadAsset()
    {
        Debug.LogError(" DownLoadAsset >>>1");
        checkUpdate();
    }
    //IEnumerator download()
    //{
    //    //初始化Addressable
    //    var init = Addressables.InitializeAsync();
    //    yield return init;
    //    List<object> list = new List<object>();
    //    list.Add("Cube");
    //    list.Add("Sphere");
    //    var downloadsize = Addressables.GetDownloadSizeAsync(list);
    //    yield return downloadsize;
    //    Debug.Log("start download:" + downloadsize.Result);
    //    var download = Addressables.DownloadDependenciesAsync(list, Addressables.MergeMode.None);
    //    yield return download;
    //    Debug.Log("download finish");
    //}
    IEnumerator Initialize(System.Action pOnFinish)
    {
        Debug.LogError(" Initialize >>>1");
        //初始化Addressable
        var init = Addressables.InitializeAsync();
        yield return init;
        //Addressables.InternalIdTransformFunc = InternalIdTransformFunc;
        pOnFinish.Invoke();
    }
    IEnumerator checkUpdateSizeEx(System.Action<long, List<string>> pOnFinish)
    {
        Debug.LogError(" checkUpdateSizeEx >>>1");
        long sizeLong = 0;
        AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        List<string> catalogs = new List<string>();
        if (checkHandle.Status == AsyncOperationStatus.Succeeded)
        {
            catalogs = checkHandle.Result;
            Debug.Log("download start catalogs catalogs Count  is :" + catalogs.Count);
        }
        IEnumerable<IResourceLocator> locators = Addressables.ResourceLocators;
        List<string> keys = new List<string>();
        //暴力遍历所有的key
        foreach (var locator in locators)
        {
            foreach (var key in locator.Keys)
            {
                keys.Add(key.ToString());
            }
        }

        Debug.Log("download start catalogs key is :" + keys.ToString());
        //var sizeHandle = Addressables.GetDownloadSizeAsync(keys);
        //yield return sizeHandle;
        //long totalDownloadSize = sizeHandle.Result;
        //if (totalDownloadSize > 0)
        //{
        //    var downloadHandle = Addressables.DownloadDependenciesAsync(keys, Addressables.MergeMode.Union);
        //    while (!downloadHandle.IsDone)
        //    {
        //        float percent = downloadHandle.PercentComplete;
        //        Debug.Log($"已经下载：{(int)(totalDownloadSize * percent)}/{totalDownloadSize}");
        //    }
        //}
        var handle1 = Addressables.GetDownloadSizeAsync(keys);
        yield return handle1;
        long downloadSize = handle1.Result;
        Debug.Log("download start handle1 downloadSize is :" + downloadSize);
        sizeLong = handle1.Result;
        var handle2 = Addressables.GetDownloadSizeAsync(catalogs);
        yield return handle2;
        long downloadSize2 = handle2.Result;
        Debug.Log("download start handle1 downloadSize2 is :" + downloadSize2);
        Addressables.Release(checkHandle);
        pOnFinish.Invoke(sizeLong, catalogs);
        //if (downloadSize > 0)
        //{
        //    yield return Addressables.DownloadDependenciesAsync(keys, Addressables.MergeMode.Union, true);
        //}
    }
    IEnumerator checkUpdateSize(System.Action<long,List<string>> pOnFinish)
    {
        Debug.LogError(" checkUpdateSize >>>1");
        long sizeLong = 0;
        AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        List<string> catalogs = new List<string>();
        if (checkHandle.Status == AsyncOperationStatus.Succeeded)
        {
            catalogs = checkHandle.Result;
            Debug.Log("download start catalogs count is :" + catalogs.Count);

            foreach (var temp in catalogs)
            {
                Debug.Log("download start catalogs file  is " + temp);
            }
            var downloadsize = Addressables.GetDownloadSizeAsync(catalogs);
            yield return downloadsize;
            if (downloadsize.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("start download:" + downloadsize.Result);
                sizeLong += downloadsize.Result;
            }
        }
        Debug.Log("download start catalogs sizeLong is :" + sizeLong);
        Addressables.Release(checkHandle);
        pOnFinish.Invoke(sizeLong, catalogs);

    }
    IEnumerator DoUpdate(List<string> catalogs,System.Action pOnFinish)
    {
        Debug.LogError(" DoUpdate >>>1");
        var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
        yield return updateHandle;
        var locators = updateHandle.Result;

        foreach (var locator in locators)
        {
            foreach (var key in locator.Keys)
            {
                Debug.Log($"update {key}");
            }
        }

        Addressables.Release(updateHandle);
        pOnFinish();
    }
    void checkUpdate()
    {
        Debug.LogError(" checkUpdate >>>1");
        StartCoroutine(Initialize(()=> {
            StartCoroutine(checkUpdateSizeEx((oSize,oList)=> {
                if (oList.Count > 0)
                {
                    StartCoroutine(DoUpdate(oList,()=> {
                        OnLoadFinish();
                    }));
                }
                else
                {
                    OnLoadFinish();
                }
            }));
        }));

       // //初始化Addressable
       // var init = Addressables.InitializeAsync();
       // yield return init;
       // Addressables.InternalIdTransformFunc = InternalIdTransformFunc;
        
       //// Addressables.LoadContentCatalogAsync("http://192.168.1.27/Android/catalog_2021.04.16.07.30.40.hash");
       // //开始连接服务器检查更新
       // AsyncOperationHandle<List<string>> checkHandle = Addressables.CheckForCatalogUpdates(false);
       // //检查结束，验证结果 
       // yield return checkHandle;
       // if (checkHandle.Status == AsyncOperationStatus.Succeeded)
       // {
       //     List<string> catalogs = checkHandle.Result;
       //     Debug.Log("download start catalogs count is :"+ catalogs.Count);
       //     if (catalogs != null && catalogs.Count > 0)
       //     {
       //         foreach(var temp in catalogs)
       //         {
       //             Debug.Log("download start catalogs file  is " + temp);
       //             var downloadsize = Addressables.GetDownloadSizeAsync(temp);
       //             yield return downloadsize;
       //             Debug.Log("start download:" + downloadsize.Result);
       //         }
       //         var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
       //         yield return updateHandle;
       //         Debug.Log("download finish");
       //     }
       //     else
       //     {
       //         Debug.Log("dont need download");
       //     }
       // }
       // Addressables.Release(checkHandle);
       // OnLoadFinish();
    }

}
