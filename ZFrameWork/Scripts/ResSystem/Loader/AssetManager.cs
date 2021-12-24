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
    /// ���������ڵ�
    /// </summary>
    public UnityEngine.Transform PoolRoot;
    private Dictionary<string, GameObjectLoader> pools = new Dictionary<string, GameObjectLoader>();
   
    /// <summary>
    /// ������ұ�
    /// </summary>  
    private Dictionary<GameObject, GameObjectLoader> lookup = new Dictionary<GameObject, GameObjectLoader>();
    public AssetManager()
    {
        UnityEngine.Transform poolNode = new GameObject("[Asset Pool]").transform;
        poolNode.transform.localPosition = Vector3.zero;
        poolNode.transform.localScale = Vector3.one;
        poolNode.transform.localRotation = Quaternion.identity;
        GameObject.DontDestroyOnLoad(poolNode);
        //������ʱ������ʱ���������Ļ���
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
    /// �첽����GameObject
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
            //��ӻ����
            this.pools.Add(name, loader);
            //��ӻ�����ұ�
            this.lookup.Add(obj, loader);
            onFinish.Invoke(obj);
        }
    }
    /// <summary>
    /// ͬ��ʵ����GameObject
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
            //��ӻ����
            this.pools.Add(name, loader);
            //��ӻ�����ұ�
            this.lookup.Add(obj, loader);
            return obj;
        }
    }
    /// <summary>
    ///��ȡģ����Ϣ
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
    /// ����Դ�ͷŻػ����
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
                //�ͷź�ӻ�����ұ����Ƴ�
                this.lookup.Remove(obj);
            }
        }
    }

    public void RemovePools(string name)
    {
        this.pools.Remove(name);
    }

    ///�ɶ�ʱ�����ã���ʱ�����棬һ�����Ϊ10��������һ��
    public void ReleaseAll()
    {
        foreach (var item in this.pools.Values)
        {
            item.Release();
        }
    }

    #region ����������Դ���ع���
    /// <summary>
    /// ��Դ���Ͳ��һ����
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
            //��ӻ����
            this.assets.Add(name, loader);
            return obj as T;
        }
    }

    /// <summary>
    /// �ͷ���Դ�����ü����Լ�1������Ϊ0���ͷ�Addressable
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
