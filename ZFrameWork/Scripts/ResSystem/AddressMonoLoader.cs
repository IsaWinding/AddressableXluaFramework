using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressMonoLoader : MonoBehaviour
{
    public string Path;
    public Transform root;
    private GameObject go_;
    
    void Start()
    {
        if (go_ == null){
            var go = AssetManager.Instance.Instantiate(Path);
            if(root != null)
                go.transform.parent = root.transform;
            else
                go.transform.parent = this.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go_ = go;
        }
    }
    private void OnDestroy()
    {
        AssetManager.Instance.FreeGameObject(go_);
    }
    
}
