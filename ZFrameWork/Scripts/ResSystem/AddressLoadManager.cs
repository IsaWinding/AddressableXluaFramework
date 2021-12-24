using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class AddressLoadManager 
{
    private static Dictionary<int, AddressLoader> maps = new Dictionary<int, AddressLoader>();
    private static int assetId = 0;
    public static int LoadAsync(string pPath, System.Action<UnityEngine.Object,int> pCB, bool IsGameObject = true,
        System.Action<int> pAssetIdCB = null)
    {
        assetId++;
        AddressLoader loader = new AddressLoader(assetId,pPath, (pObj,oloader) => {
            pCB.Invoke(pObj, oloader.Id);
            if(pAssetIdCB!= null)
                pAssetIdCB.Invoke(oloader.Id);
        });
        if (IsGameObject)
        {
            loader.LoadPrefabAsync();
        }
        else
        {
            loader.LoadAssetAsync();
        }
        maps.Add(assetId, loader);
        return assetId;
    }

    public static UnityEngine.Object Load(string pPath, bool IsGameObject = true, System.Action<int> pAssetIdCB = null)
    {
        assetId++;
        AddressLoader loader = new AddressLoader(assetId, pPath);
        UnityEngine.Object result = null;
        if (IsGameObject)
        {
            result = loader.LoadPrefab();
        }
        else
        {
            result = loader.LoadAsset();
        }
        maps.Add(assetId, loader);
        if (pAssetIdCB != null)
        {
            pAssetIdCB.Invoke(assetId);
        }
        //CoroutineManager.Instance.StartCoroutine(loader.LoadAsset());
        return result;
    }
    public static void UnLoadByAssetId(int pAssetId)
    {
        if (maps.ContainsKey(pAssetId))
        {
            maps[pAssetId].UnLoadAsset();
            maps.Remove(pAssetId);
        }
    }
    public static void UnLoad(AddressLoader pLoader)
    {
        pLoader.UnLoadAsset();
    }

    public static int LoadByResTypeAndName(string pResType,string pResName, System.Action<UnityEngine.Object,int> pCB, bool IsGameObject = true, System.Action<int> pAssetIdCB = null)
    {
        var editorPath = pResName;// Redmoon.Resource.Utility.BundleUtility.GetEditorPath(pResType, pResName);
        return LoadAsync(editorPath, pCB, IsGameObject, pAssetIdCB);
    }
}
