using Redmoon.Animator;
using UnityEngine;

public class PositionLine2Behaviour : BaseBehaviour
{
    public MoveDir moveDir;
    public float height;
    public float length;

    private X2LineVec3 x2LineV3;
    private Vector3 start;
    private Vector3 end;
    private Vector3 dir;
    protected override void OnStart()
    {
        start = target_.transform.position;
        if (moveDir == MoveDir.Forward){
            dir = target_.transform.forward * length;
        }
        else if (moveDir == MoveDir.Back){
            dir = -target_.transform.forward * length;
        }
        else if (moveDir == MoveDir.Left)
        {
            dir = -target_.transform.right * length;
        }
        else if (moveDir == MoveDir.Right)
        {
            dir = target_.transform.right * length;
        }
        end = start + dir;
        x2LineV3 = new X2LineVec3() {
            startPos = start, endPos = end, height = height
        };
        x2LineV3.Init();
    }
    protected override void OnProgress(float pProgress)
    {
        target_.transform.position = x2LineV3.GetPos(pProgress);
    }
}
