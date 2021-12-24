using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshProData : ComponentData
{
    private string text;
    public override ComponentType ComType { get { return ComponentType.TextMesh; } }
    public TextMeshProData(string pPath, string pText, SetType pSetType = SetType.MainSet,bool pEnable = true)
    {
        Path = pPath;
        text = pText;
        setType = pSetType;
        Enable = pEnable;
    }
    public override void OnLoadComponent(Component pComponent)
    {
        var com = pComponent as TMPro.TextMeshProUGUI;
        com.text = text;
    }
}
