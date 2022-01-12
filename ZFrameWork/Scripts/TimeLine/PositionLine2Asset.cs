using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public enum MoveDir { 
	Forward = 1,
	Back = 2,
	Left = 3,
	Right = 4
}

[System.Serializable]
public class PositionLine2Asset : BaseAsset
{
	public MoveDir moveDir;
	public float height;
	public float length;
	
	public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
	{
		var baseBehaviour = new PositionLine2Behaviour();
		baseBehaviour.target_ = target_.GetGo(graph, go);
		baseBehaviour.moveDir = moveDir;
		baseBehaviour.height = height;
		baseBehaviour.length = length;
		return baseBehaviour;
	}
}
