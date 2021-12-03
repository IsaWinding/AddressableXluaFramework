using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBtnData : ComponentData
{
    private int Index;
    public override ComponentType ComType { get { return ComponentType.NormalBtn; } }
    public NormalBtnData(string pPath, int pIndex, SetType pSetType = SetType.MainSet, bool pEnable = true)
    {
        Path = pPath;
        Index = pIndex;
        setType = pSetType;
        Enable = pEnable;
    }

    public override void OnLoadComponent(Component pComponent)
    {
        var com = pComponent as NormalBtn;
        com.Index = Index;
    }
}
