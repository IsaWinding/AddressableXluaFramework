using UnityEngine;
using Entitas;

//是否可以移动
[Battle]
public class CanMoveComponent : IComponent { }
//移动的目标位置
[Battle]
public class MoveTargetComponent : IComponent
{
    public Vector3 target;
}
//是否移动完成
[Battle]
public class MoveFinish : IComponent { }
