using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GameObjectLoader : BaseLoader
{
    /// <summary>
    /// 资源缓存列表
    /// </summary>
    private Stack<GameObject> caches = new Stack<GameObject>();

    /// <summary>
    /// 正在使用的列表
    /// </summary>
    private HashSet<GameObject> references = new HashSet<GameObject>();
    public GameObject prefab;

    public GameObjectLoader(string name) : base(name)
    {
        this.prefab = null;
    }
    public GameObjectLoader(GameObject obj) : base(obj.name)
    {
        this.prefab = obj;
    }
    /// <summary>
    /// 同步方法，确保已经加载好了
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(Transform parent)
    {
        GameObject obj = null;
        if (caches.Count > 0)
        {
            obj = caches.Pop();
        }
        else
        {
            obj = GameObject.Instantiate(this.prefab) as GameObject;
            obj.name = this.name;
        }
        this.references.Add(obj);
        return obj;
    }
    /// <summary>
    /// 同步方法实例化对象
    /// </summary>
    /// <returns></returns>
    public GameObject Instantiate()
    {
        if (caches.Count > 0)
        {
            var obj = caches.Pop();
            this.references.Add(obj);
            return obj;
        }
        else
        {
            if (this.prefab != null)
            {
                var obj = GameObject.Instantiate(this.prefab) as GameObject;
                obj.name = this.name;
                this.references.Add(obj);
                return obj;
            }
            else
            {
                this.prefab = base.Load<GameObject>();
                var obj = GameObject.Instantiate(this.prefab) as GameObject;
                obj.name = this.name;
                base.Release();
                this.references.Add(obj);
                return obj;
            }
        }
    }
    public void Free(GameObject obj)
    {
        this.caches.Push(obj);
        this.references.Remove(obj);
        obj.transform.SetParent(AssetManager.Instance.PoolRoot);
    }
    public override void Release()
    {
        foreach (var obj in this.caches)
        {
            GameObject.Destroy(obj.gameObject);
        }
        if (this.references.Count <= 0)
        {
            base.Release();
            AssetManager.Instance.RemovePools(this.name);
        }
    }
}
