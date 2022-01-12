using UnityEngine;

public class PositionBehaviour : BaseBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public bool useInitPos;

    private Vector3 dir;
    protected override void OnStart()
    {
        if (!useInitPos)
        {
            dir = end - start;
            start = target_.transform.position;
            end = target_.transform.position + dir;
        }
        //Debug.LogError("On useInitPos >>" + useInitPos);
        //Debug.LogError("On Start >>"+ start);
        //Debug.LogError("On end >>" + end);
    }
    protected override void OnProgress(float pProgress)
    {
        target_.transform.position = start + (end - start) * pProgress;
    }
}
