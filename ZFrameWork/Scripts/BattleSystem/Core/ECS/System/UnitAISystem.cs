using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitAISystem : ReactiveSystem<BattleEntity>
{
    public UnitAISystem(Contexts contexts) : base(contexts.battle){}

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.aIState.aiStateType == AIStateType.Idle)
            {
                e.isMoveFinish = true;
                e.ReplaceModelAnimation("Idle");
            }
            else if (e.aIState.aiStateType == AIStateType.Move)
            {
                e.isMoveFinish = false;
                e.ReplaceMoveTarget(e.aITarget.target.position.value);
                e.ReplaceModelAnimation("Move");
            }
            else if (e.aIState.aiStateType == AIStateType.Move)
            {
                e.isMoveFinish = true;
                e.ReplaceModelAnimation("Attack");
            }
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasAIState;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.AllOf(BattleMatcher.AIState, BattleMatcher.AITarget));
    }
}
