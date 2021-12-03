using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIBaseLoaders : MonoBehaviour
{
    public GameObject ParentGo;
    public List<string> ResKerys = new List<string>();
    private Dictionary<int,UIBase> UIBaseMap = new Dictionary<int, UIBase>();
    private Dictionary<int, UIBaseLoader> LoderMap = new Dictionary<int, UIBaseLoader>();
    private Dictionary<int, GameObject> LoderGOMap = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    private void Awake(){

    }
    private void OnDestroy()
    {
        foreach (var temp in LoderMap){
            temp.Value.Clear();
        }
        LoderMap = null;
        LoderGOMap = null;
        UIBaseMap = null;
    }

    public void ShowGameObject(int pIndex)
    {
        foreach (var temp in (LoderGOMap))
        {
            temp.Value.SetActive(temp.Key == pIndex);
        }
    }
    public void GetUIBaseAsync(int pIndex, System.Action<UIBase> pCallBack)
    {
        if (UIBaseMap.ContainsKey(pIndex))
        {
            pCallBack(UIBaseMap[pIndex]);
        }
        else
        {
            var loader = GetUIBaseLoder(pIndex);
            if (loader != null)
            {
                loader.AsyncLoad(() => {
                    var uibase = loader.GetUIBase();
                    if (uibase != null)
                    {
                        UIBaseMap.Add(pIndex, uibase);
                        LoderGOMap.Add(pIndex, loader.GetLoderGameObject());
                    }
                    pCallBack(uibase);
                });
            }
        }
    }
    private UIBaseLoader GetUIBaseLoder(int pIndex)
    {
        if (LoderMap.ContainsKey(pIndex))
        {
            return LoderMap[pIndex];
        }
        if (ResKerys.Count >= pIndex)
        {
            var loder = this.gameObject.AddComponent<UIBaseLoader>();
            loder.ResPath = ResKerys[pIndex - 1];
            loder.ParentGo = ParentGo;
            LoderMap.Add(pIndex, loder);
            return loder;
        }
        return null;
    }
}
