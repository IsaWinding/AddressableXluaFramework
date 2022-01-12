using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePicker : MonoBehaviour
{
    public GameObject go;

    public List<UnitSetter>  unitSetters;

    [ContextMenu("PickGOs")]
    public void Pick()
    {
        foreach (var temp in unitSetters) {
            temp.SetByGO(go);
        }
    }
}
