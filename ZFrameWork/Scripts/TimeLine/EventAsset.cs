using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class EventAsset : BaseAsset
{
    public string EventKey;
    public string EventParams;

    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go){
        var baseBehaviour = new EventBehaviour();
        baseBehaviour.target_ = target_.Resolve(graph.GetResolver());
        baseBehaviour.EventKey = EventKey;
        baseBehaviour.EventParams = EventParams;
        return baseBehaviour;
    }
}
