using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPicker : MonoBehaviour
{
    public List<GameObject> gos;
    public AIPathSetter aipathSetter;

    [ContextMenu("PickGOs")]
    public void Pick()
    {
        aipathSetter.SetByGOs(gos);
    }
}
