using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NormalBtn : UIBase
{
    public string MessageKey;
    public int Index = 0;
    public GameObject ShowRoot;
    public GameObject HideRoot;
    public bool DefaultShow;
    private System.Action<int> OnClickCB;
    public void SetClickCB(System.Action<int> pOnClickCB)
    {
        OnClickCB = pOnClickCB;
    }
    private void Awake()
    {
        var btn = this.gameObject.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => {
                SendMessageToCtrl(MessageKey, Index);
                Show(true);
                if (OnClickCB != null)
                    OnClickCB.Invoke(Index);
            });
        }
        Show(DefaultShow);
    }
    public void ShowByCurIndex(int pCurIndex){
        Show(pCurIndex == Index);
    }
    public void Show(bool pShow){
        if (ShowRoot != null){
            ShowRoot.SetActive(pShow);
        }
        if (HideRoot != null){
            HideRoot.SetActive(!pShow);
        }
    }
}
