using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AddressLoader 
{
    private string path;
    private UnityEngine.Object asset_;
    private GameObject go_;
    private System.Action<UnityEngine.Object, AddressLoader> OnFinish;
    private System.Action<IList<IResourceLocation>, AddressLoader> OnLabelFinish;
    private IList<IResourceLocation> labelLocations;
    public int Id;
    public AddressLoader(int pId, string pPath)
    {
        path = pPath;
        Id = pId;
    }
    public AddressLoader(int pId, string pLabel, System.Action<IList<IResourceLocation>, AddressLoader> pCallBack)
    {
        path = pLabel;
        OnLabelFinish = pCallBack;
        Id = pId;
    }
    public AddressLoader(int pId,string pPath, System.Action<UnityEngine.Object, AddressLoader> pCallBack)
    {
        path = pPath;
        OnFinish = pCallBack;
        Id = pId;
    }
    
    //加载资源
    public void LoadAssetAsync()
    {
        Addressables.LoadAssetAsync<UnityEngine.Object>(path).Completed += OnAssetLoadedComplete;
    }
    public void LoadAssetAyncByLabel()
    {
        //var keys = new List<object> { path };
        Addressables.LoadAssetsAsync<IResourceLocation>(path, null,Addressables.MergeMode.Union).Completed += OnLabelAssetsLoaded;
    }
    
    private void OnLabelAssetsLoaded(AsyncOperationHandle<IList<IResourceLocation>> op)
    {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            if (OnLabelFinish != null)
            {
                labelLocations = op.Result;
                OnLabelFinish(labelLocations, this);
            }
            else
            {
                UnLoadAsset();
            }
        }
    }
    public UnityEngine.Object LoadAsset()
    {
        var loader = Addressables.LoadAsset<UnityEngine.Object>(path);
        loader.Completed += (obj)=>{
            asset_ = obj.Result;
        };
        asset_ = loader.Result;
        return asset_;
    }
    public GameObject LoadPrefab()
    {
        var loader =  Addressables.Instantiate(path);
        go_ = loader.Result;
        return go_;
    }
    //加载Prefab
    public void LoadPrefabAsync()
    {
        Addressables.InstantiateAsync(path).Completed += OnGameObjectLoaderComplete;
    }
    public void UnLoadAsset(){
        if (go_ != null)
        {
            Addressables.ReleaseInstance(go_);
        }
        if (asset_ != null)
        {
            Addressables.Release<UnityEngine.Object>(asset_);
        }
        if (labelLocations != null)
        {
            Addressables.Release<IList<IResourceLocation>>(labelLocations);
        }
        labelLocations = null;
        go_ = null;
        asset_ = null;
        OnFinish = null;
        OnLabelFinish = null;
    }
    public void OnGameObjectLoaderComplete(AsyncOperationHandle<GameObject> res)
    {
        if (res.Status == AsyncOperationStatus.Succeeded)
        {
            if (OnFinish != null)
            {
                go_ = res.Result;
                OnFinish(go_, this);
            }
            else
            {
                UnLoadAsset();
            }
        }
    }
    private void OnAssetLoadedComplete(AsyncOperationHandle<UnityEngine.Object> res)
    {
        if (res.Status == AsyncOperationStatus.Succeeded)
        {
            if (OnFinish != null)
            {
                asset_ = res.Result;
                OnFinish(asset_, this);
            }
            else
            {
                UnLoadAsset();
            }
        }
    }

}
