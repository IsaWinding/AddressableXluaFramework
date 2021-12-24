using UnityEngine;
using UnityEngine.Playables;

public class ScaleAsset : BaseAsset
{
	public Vector3 start;
	public Vector3 end;
	public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
	{
		ScaleBehaviour baseBehaviour = new ScaleBehaviour();
		baseBehaviour.target_ = target_.Resolve(graph.GetResolver());
		baseBehaviour.start = start;
		baseBehaviour.end = end;
		return baseBehaviour;
	}
}
