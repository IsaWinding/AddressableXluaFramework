using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonData : ComponentData
{
    private System.Action onClick;

    public override ComponentType ComType { get { return ComponentType.Button; } }
    public ButtonData(string pPath, System.Action pOnclick)
    {
        Path = pPath;
        onClick = pOnclick;
    }

    public override void OnLoadComponent(Component pComponent)
    {
        var com = pComponent as Button;
        com.onClick.RemoveAllListeners();
        com.onClick.AddListener(()=> {
            onClick.Invoke();
        });
    }
}
