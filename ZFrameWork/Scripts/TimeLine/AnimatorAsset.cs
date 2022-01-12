using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class AnimatorAsset : BaseAsset
{
    public string aniName;
    public int layer;
   
    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        var baseBehaviour = new AnimatorBehaviour();
        baseBehaviour.target_ = target_.GetGo(graph, go);
        baseBehaviour.aniName = aniName;
        baseBehaviour.layer = layer;
        return baseBehaviour;
    }
}
