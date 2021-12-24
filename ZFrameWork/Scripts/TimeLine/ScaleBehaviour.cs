using UnityEngine;

public class ScaleBehaviour : BaseBehaviour
{
	public Vector3 start;
	public Vector3 end;
	protected override void OnProgress(float pProgress)
	{
		target_.transform.localScale = start + (end - start) * pProgress;
	}
}
