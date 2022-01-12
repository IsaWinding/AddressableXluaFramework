using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ImageColorAsset : BaseAsset
{
    public Color start;
    public Color end;
    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        ImageColorBehaviour baseBehaviour = new ImageColorBehaviour();
        baseBehaviour.target_ = target_.GetGo(graph, go);
        baseBehaviour.start = start;
        baseBehaviour.end = end;
        return baseBehaviour;
    }
}
