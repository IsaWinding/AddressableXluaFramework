using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitMoveSystem : IExecuteSystem,ICleanupSystem
{
    readonly IGroup<BattleEntity> _moveIngUnits;
    readonly IGroup<BattleEntity> _moveFinishUnits;
    
    public UnitMoveSystem(Contexts contexts)
    {
        _moveIngUnits = contexts.battle.GetGroup(BattleMatcher.MoveTarget);
        _moveFinishUnits = contexts.battle.GetGroup(BattleMatcher.MoveFinish);
    }

    public void Execute()
    {
        foreach (var e in _moveIngUnits.GetEntities())
        {
            var dir = e.moveTarget.target - e.position.value;
            var newPos = e.position.value + dir.normalized * e.unitMoveSpeedAttribute.Value.Value * Time.deltaTime;
            e.ReplacePosition(newPos);

            var newRot = Quaternion.LookRotation(dir.normalized,Vector3.up);
            e.ReplaceDirection(newRot.eulerAngles.y);

            e.ReplaceModelAnimation(AniNameType.Move,null);
            if (dir.magnitude <= 0.5f)
            {
                e.ReplaceModelAnimation(AniNameType.Idle,null);
                e.RemoveMoveTarget();
                e.isMoveFinish = true;
            }
        }
    }

    public void Cleanup()
    {
        foreach (var e in _moveFinishUnits.GetEntities())
        {
            e.isMoveFinish = false;
        }
    }
}
