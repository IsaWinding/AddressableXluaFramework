using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class AssetManager
{
    private static AssetManager instance;
    public static AssetManager Instance { get { if (instance == null) instance = new AssetManager();return instance; } }
    /// <summary>
    /// 缓存对象根节点
    /// </summary>
    public UnityEngine.Transform PoolRoot;
    private Dictionary<string, GameObjectLoader> pools = new Dictionary<string, GameObjectLoader>();
   
    /// <summary>
    /// 缓存查找表
    /// </summary>  
    private Dictionary<GameObject, GameObjectLoader> lookup = new Dictionary<GameObject, GameObjectLoader>();
    public AssetManager()
    {
        UnityEngine.Transform poolNode = new GameObject("[Asset Pool]").transform;
        poolNode.transform.localPosition = Vector3.zero;
        poolNode.transform.localScale = Vector3.one;
        poolNode.transform.localRotation = Quaternion.identity;
        GameObject.DontDestroyOnLoad(poolNode);
        //启动定时器，定时清理缓存池里的缓存
    }

    public void LoadSceneAsync(string pSceneName,System.Action<SceneInstance> pOnFinish,System.Action<float> pOnProgress)
    {
        var handle = Addressables.LoadSceneAsync(pSceneName);
        handle.Completed += (oRes) =>{
            if (oRes.Status == AsyncOperationStatus.Succeeded){
                if (pOnFinish != null){
                    pOnFinish(oRes.Result);
                }
                Addressables.Release(handle);
                pOnProgress(oRes.PercentComplete);
            }
            else{
                pOnProgress(oRes.PercentComplete);
            }
        };
    }
    public void UnLoadScene(SceneInstance pSceneInstance)
    {
        Addressables.UnloadSceneAsync(pSceneInstance);
    }

    /// <summary>
    /// 异步加载GameObject
    /// </summary>
    /// <param name="name"></param>
    /// <param name="onFinish"></param>
    public void InstantiateAsync(string name, System.Action<GameObject> onFinish)
    {
        GameObjectLoader loader;
        if (this.pools.TryGetValue(name, out loader))
        {
            var obj = loader.Instantiate();
            this.lookup.Add(obj, loader);
            onFinish.Invoke(obj);
        }
        else
        {
            loader = new GameObjectLoader(name);
            var obj = loader.Instantiate();
            //添加缓存池
            this.pools.Add(name, loader);
            //添加缓存查找表
            this.lookup.Add(obj, loader);
            onFinish.Invoke(obj);
        }
    }
    /// <summary>
    /// 同步实例化GameObject
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Instantiate(string name)
    {
        GameObjectLoader loader;
        if (this.pools.TryGetValue(name, out loader))
        {
            var obj = loader.Instantiate();
            this.lookup.Add(obj, loader);
            return obj;
        }
        else
        {
            loader = new GameObjectLoader(name);
            var obj = loader.Instantiate();
            //添加缓存池
            this.pools.Add(name, loader);
            //添加缓存查找表
            this.lookup.Add(obj, loader);
            return obj;
        }
    }
    /// <summary>
    ///获取模板信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetTemplete(string name)
    {
        GameObjectLoader loader;
        if (this.pools.TryGetValue(name, out loader))
        {
            return loader.prefab;
        }
        return null;
    }
    /// <summary>
    /// 将资源释放回缓存池
    /// </summary>
    /// <param name="obj"></param>
    public void FreeGameObject(GameObject obj)
    {
        if (obj != null)
        {
            GameObjectLoader loader;
            if (this.lookup.TryGetValue(obj, out loader))
            {
                loader.Free(obj);
                //释放后从缓存查找表中移除
                this.lookup.Remove(obj);
            }
        }
    }

    public void RemovePools(string name)
    {
        this.pools.Remove(name);
    }

    ///由定时器调用，定时清理缓存，一般设计为10分钟清理一次
    public void ReleaseAll()
    {
        foreach (var item in this.pools.Values)
        {
            item.Release();
        }
    }

    #region 引用类型资源加载管理
    /// <summary>
    /// 资源类型查找缓存表
    /// </summary>
    private Dictionary<string, UnityObjectLoader> assets = new Dictionary<string, UnityObjectLoader>();

    public T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        UnityObjectLoader loader;
        if (this.assets.TryGetValue(name, out loader))
        {
            var obj = loader.LoadAsset<T>();
            return obj as T;
        }
        else
        {
            loader = new UnityObjectLoader(name);
            var obj = loader.LoadAsset<T>();
            //添加缓存池
            this.assets.Add(name, loader);
            return obj as T;
        }
    }

    /// <summary>
    /// 释放资源，引用计数自减1，减少为0后释放Addressable
    /// </summary>
    /// <param name="name"></param>
    public void FreeAsset(string name)
    {
        UnityObjectLoader loader;
        if (this.assets.TryGetValue(name, out loader))
        {
            loader.Free();
        }
    }
    #endregion
}
