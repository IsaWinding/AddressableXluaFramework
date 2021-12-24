using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class UnityObjectLoader : BaseLoader
{
    private int objectRefCount = 0;
    private Object object_;
    public UnityObjectLoader(string name) : base(name){
        this.object_ = null;
    }
    public Object LoadAsset<T>() where T : UnityEngine.Object
    {
        if (object_ == null){
            object_ = base.Load<T>();
        }
        this.objectRefCount++;
        return object_;
    }
    public void Free()
    {
        this.objectRefCount--;
        Release();
    }
    public override void Release()
    {
        if (this.objectRefCount <= 0)
        {
            base.Release();
            object_ = null;
            AssetManager.Instance.RemovePools(this.name);
        }
    }
}
