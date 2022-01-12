using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class EventAsset : BaseAsset
{
    public BattleEventType EventKey = BattleEventType.Damg;
    public DamgeEvent dmageEvent;

    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go){
        var baseBehaviour = new EventBehaviour();
        baseBehaviour.target_ = target_.GetGo(graph, go);
        baseBehaviour.EventKey = EventKey;
        baseBehaviour.dmageEvent = dmageEvent;
        return baseBehaviour;
    }
}
