using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPanel : UIBase
{
    public UIBaseLoaders loders;
    public override void OnForward() {
        OpenSubUI(1);
    }
    public void OpenSubUI(int pIndex)
    {
        loders.GetUIBaseAsync(pIndex, (oUIBase) => {
            var uiBase = oUIBase;
            uiBase.OnForward();
            loders.ShowGameObject(pIndex);
        });
    }
}
