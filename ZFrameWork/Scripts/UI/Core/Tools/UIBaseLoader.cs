using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseLoader : MonoBehaviour
{
    public string ResPath;
    public GameObject ParentGo;
    public UIBase uiBase;

    public bool NeedAwakeLoder = false;
    private GameObject LoderGameObject;
    private int AssetId;

    public GameObject GetLoderGameObject()
    {
        return LoderGameObject;
    }

    public UIBase GetUIBase()
    {
        return uiBase;
    }
    private void Awake()
    {
        if (NeedAwakeLoder)
        {
            AsyncLoad(null);
        }
    }
    public void Clear()
    {
        if (LoderGameObject != null)
        {
            AddressLoadManager.UnLoadByAssetId(AssetId);
        }
        LoderGameObject = null;
        uiBase = null;
        AssetId = 0;
    }
    private void OnDestroy()
    {
        Clear();
    }
    public void AsyncLoad(System.Action pCallBack)
    {
        if (uiBase != null)
        {
            pCallBack();
        }
        else
        {
            if (AssetId == 0)
            {
                AssetId = AddressLoadManager.LoadAsync(ResPath, (oGO, oId) => {
                    if (oGO == null)
                    {
                        Debug.LogError(" Load GameObject is nil ResPath is: ");
                        pCallBack();
                        return;
                    }
                    GameObject temp = oGO as GameObject;
                    LoderGameObject = temp;
                    if (ParentGo != null)
                    {
                        LoderGameObject.transform.parent = ParentGo.transform;
                    }
                    else
                    {
                        LoderGameObject.transform.parent = this.transform.parent;
                    }
                    LoderGameObject.transform.localPosition = Vector3.zero;
                    LoderGameObject.transform.localScale = Vector3.one;
                    uiBase = LoderGameObject.GetComponent<UIBase>();
                    pCallBack();
                });
            }
        }
    }
}
