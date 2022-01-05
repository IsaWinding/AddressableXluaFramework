using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitPartolSystem : IExecuteSystem
{
    readonly IGroup<BattleEntity> _partolIngUnits;
    public UnitPartolSystem(Contexts contexts){
        _partolIngUnits = contexts.battle.GetGroup(BattleMatcher.UnitPartrolEnable);
    }
    public void Execute()
    {
        foreach (var e in _partolIngUnits.GetEntities()) {
            if (AIPath.IsReachNextPoint(e.unitPartrolPoint.nextPoint, e.position.value))
            {
                PathPoint oNextPoint;
                bool oIsForward;
                AIPath.GetNextPointEx(e.unitPartrolPoint.nextPoint,e.unitPartrolType.pathType,
                    e.unitPartrolPoint.isForward,out oNextPoint, out oIsForward);
                if (oNextPoint != null)
                    e.ReplaceUnitPartrolPoint(oIsForward, oNextPoint);
                else
                    e.isUnitPartrolEnable = false;
            }
            else
            {
                e.isMoveFinish = false;
                e.ReplaceMoveTarget(e.unitPartrolPoint.nextPoint.pos);
            }
        }
    }

   
}
