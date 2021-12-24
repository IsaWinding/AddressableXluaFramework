using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[System.Serializable]
public class BaseAsset : PlayableAsset
{
	public ExposedReference<GameObject> target_;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
	{
		var baseBehaviour = GetBehaviour(graph, go);
		return ScriptPlayable<BaseBehaviour>.Create(graph, baseBehaviour);
	}
	public virtual BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
	{
		return null;

	}
}
