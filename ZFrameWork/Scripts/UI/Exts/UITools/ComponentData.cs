using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ComponentDef
{
    public static Dictionary<ComponentType, string> ComponentBinder = new Dictionary<ComponentType, string>() {
        {ComponentType.Text,"Text"},{ComponentType.Image,"Image" },{ComponentType.Button,"Button" },{ComponentType.NormalBtn,"NormalBtn" },{ComponentType.TextMesh,"TMPro.TextMeshProUGUI" }
    };
    internal static bool FindRelatedPath(Transform child, Transform root, out string path)
    {
        var current = child;
        System.Collections.Generic.List<string> pathList = new System.Collections.Generic.List<string>();
        int counter = 0;
        while (current != null && counter <= 10)
        {
            pathList.Add(current.gameObject.name);
            if (current == root)
            {
                path = ConvertListToPath(pathList);
                return true;
            }
            current = current.parent;
            counter++;
        }
        path = null;
        return false;
    }
    private static string ConvertListToPath(System.Collections.Generic.List<string> pathList)
    {
        string result = "";
        for (int index = pathList.Count - 2; index >= 0; index--)
        {
            if (index > 0)
            {
                result += pathList[index] + "/" ;
            }
            else
            {
                result += pathList[index];
            }
        }
        return result;
    }
}
public enum ComponentType
{ 
    None = 0,
    Text = 1,
    Image = 2,
    Button = 3,
    NormalBtn = 4,
    TextMesh = 5,
}
public enum SetType
{ 
    MainSet = 1, //该组件的主要设置
    Enable = 2,//显示或者隐藏的设置
    FillAmount = 3,//图片的进度设置
}
public class ComponentData
{
    public string Path;
    public virtual ComponentType ComType {get{ return ComponentType.None; } }
    public  SetType setType = SetType.MainSet;
    public bool Enable = true;

    public virtual void OnLoadComponent(Component pComponent)
    {
       
    }
}
