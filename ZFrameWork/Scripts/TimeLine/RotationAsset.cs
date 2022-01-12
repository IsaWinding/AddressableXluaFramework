using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class RotationAsset : BaseAsset
{
    public Vector3 start;
    public Vector3 end;
    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        var baseBehaviour = new RotationBehaviour();
        baseBehaviour.target_ = target_.GetGo(graph, go);
        baseBehaviour.start = start;
        baseBehaviour.end = end;
        return baseBehaviour;
    }
}
