using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DmgDrawAsset : BaseAsset
{
    public float Radius;
    public float Height;
    public float Degree;
    public Color Color = Color.red;
    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        var baseBehaviour = new DmgDrawBehaviour();
        baseBehaviour.target_ = target_.GetGo(graph, go);
        baseBehaviour.Radius = Radius;
        baseBehaviour.Height = Height;
        baseBehaviour.Degree = Degree;
        baseBehaviour.Color = Color;
        return baseBehaviour;
    }
}
