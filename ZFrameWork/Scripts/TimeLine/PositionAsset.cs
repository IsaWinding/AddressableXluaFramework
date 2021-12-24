using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PositionAsset : BaseAsset
{
	public Vector3 start;
	public Vector3 end;
	public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
	{
		PositionBehaviour baseBehaviour = new PositionBehaviour();
		baseBehaviour.target_ = target_.Resolve(graph.GetResolver());
		baseBehaviour.start = start;
		baseBehaviour.end = end;
		return baseBehaviour;
	}
}
