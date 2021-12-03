using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextData : ComponentData
{
    private string text;
    public override ComponentType ComType { get { return ComponentType.Text; } }
    public TextData(string pPath,string pText,SetType pSetType = SetType.MainSet) {
        Path = pPath;
        text = pText;
        setType = pSetType;
    }
    public override void OnLoadComponent(Component pComponent)
    {
        var com = pComponent as Text;
        com.text = text;
    }
}
