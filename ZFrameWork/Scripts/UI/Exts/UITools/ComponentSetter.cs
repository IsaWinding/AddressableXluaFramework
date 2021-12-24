using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComponentPather
{
    public string Path;
    public ComponentType Type;
    public GameObject GO;

    private GameObject parentTransform;
    private Component component;
    public string SetComponenPathByGO(GameObject pParent)
    {
        if (pParent != null && GO != null)
        {
            parentTransform = pParent;
            ComponentDef.FindRelatedPath(GO.transform, pParent.transform,out Path);
        }
        return Path;
    }
    public Transform GetTransformByPath()
    {
        if (GO != null)
            return GO.transform;
        if (parentTransform != null)
            return parentTransform.transform.Find(Path);
        return null;
    }
    public Component GetComponent()
    {
        if (component != null){
            return component;
        }
        var transform = GetTransformByPath();
        if (transform)
        {
            var stringType = ComponentDef.ComponentBinder[Type];
            component = transform.gameObject.GetComponent(stringType);
            return component;
        }
        return null;
    }
    public void SetComponentData(ComponentData pComponentData)
    {
        var com = GetComponent();
        if (pComponentData.setType == SetType.Enable)
        {
            com.gameObject.SetActive(pComponentData.Enable);
        }
        else
        {
            com.gameObject.SetActive(true);
            pComponentData.OnLoadComponent(com);
        }
    }
}

public class ComponentSetter : MonoBehaviour
{
    public List<ComponentPather> ComponentPathers = new List<ComponentPather>();

    private Dictionary<string, List<ComponentPather>> ComponentMap = new Dictionary<string, List<ComponentPather>>();
    private void Awake()
    {
        ComponentMap.Clear();
        foreach (var temp in ComponentPathers)
        {
            var path = temp.SetComponenPathByGO(this.gameObject);
            if (!string.IsNullOrEmpty(path))
            {
                if (ComponentMap.ContainsKey(path))
                {
                    ComponentMap[path].Add(temp);
                }
                else
                {
                    ComponentMap.Add(path,new List<ComponentPather>() { temp });
                }
            }
        }
    }

    public void SetComponentDatas(List<ComponentData> pComponentDatas)
    {
        foreach (var temp in pComponentDatas)
        {
            SetOneComponentData(temp);
        }
    }
    public void SetOneComponentData(ComponentData pComponentData)
    {
        var pather = GetComponentPather(pComponentData.Path, pComponentData.ComType);
        if(pather != null)
        {
            pather.SetComponentData(pComponentData);
        }
    }
    private ComponentPather GetComponentPather(string pPath,ComponentType pType)
    {
        if (ComponentMap.ContainsKey(pPath))
        {
            foreach (var temp in ComponentMap[pPath])
            {
                if (temp.Type == pType)
                    return temp;
            }
        }
        return null;
    }
}
