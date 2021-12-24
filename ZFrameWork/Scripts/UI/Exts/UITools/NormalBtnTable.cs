using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBtnTable : MonoBehaviour
{
    public List<NormalBtn> Btns = new List<NormalBtn>();
    public void Awake()
    {
        for (var i = 0; i < Btns.Count; i++)
        {
            Btns[i].SetClickCB((oIndex)=> {
                OnClickOneBtn(oIndex);
            });
        }
    }
    private void OnClickOneBtn(int pIndex)
    {
        for (var i = 0; i < Btns.Count; i++)
        {
            Btns[i].ShowByCurIndex(pIndex);
        }
    }
}
