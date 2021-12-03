using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMaker : MonoBehaviour
{
    public GameObject uiType;
    public GameObject parent;
    
    public List<GameObject> gos = new List<GameObject>();
    private List<GameObject> FreeGos = new List<GameObject>();

    private void Start(){
        uiType.SetActive(false);
    }
    public void RecoverGO(GameObject pGO) {
        pGO.SetActive(false);
        FreeGos.Add(pGO);
    }
    public GameObject GetFreeGO() {
        if (FreeGos.Count < 1)
        {
            GameObject _temp = GameObject.Instantiate(uiType) as GameObject;
            _temp.transform.SetParent(parent.transform);
            _temp.transform.localScale = Vector3.one;
            _temp.transform.localPosition = Vector3.zero;
            _temp.transform.localRotation = Quaternion.identity;
            FreeGos.Add(_temp);
        }
        var index = FreeGos.Count - 1;
        var go = FreeGos[index];
        FreeGos.RemoveAt(index);
        go.SetActive(true);
        return go;
    }
    public void MakerByCount(int pCount, System.Action<GameObject, int> pCB)
    {
        if (gos.Count < pCount)
        {
            for (var i = gos.Count; i < pCount; i++)
            {
                GameObject _temp = GameObject.Instantiate(uiType) as GameObject;
                _temp.transform.SetParent(parent.transform);
                _temp.transform.localScale = Vector3.one;
                _temp.transform.localPosition = Vector3.zero;
                _temp.transform.localRotation = Quaternion.identity;
                gos.Add(_temp);
            }
        }
        for (var i = 0; i < gos.Count; i++)
        {
            if (i < pCount)
            {
                gos[i].SetActive(true);
                pCB.Invoke(gos[i], i + 1);
            }
            else
            {
                gos[i].SetActive(false);
            }
        }

    }
    public void Maker(int pCount, List<object> pDatas,System.Action<GameObject,int> pCB)
    {
        if (gos.Count < pCount)
        {
            for (var i = gos.Count; i < pCount; i++)
            {
                GameObject _temp = GameObject.Instantiate(uiType) as GameObject;
                _temp.transform.SetParent(parent.transform);
                _temp.transform.localScale = Vector3.one;
                _temp.transform.localPosition = Vector3.zero;
                _temp.transform.localRotation = Quaternion.identity;
                gos.Add(_temp);
            }
        }
        for (var i = 0; i < gos.Count; i++)
        {
            if (i < pCount)
            {
                gos[i].SetActive(true);
                pCB.Invoke(gos[i],i);
            }
            else
            {
                gos[i].SetActive(false);
            }
        }
    }    
}
