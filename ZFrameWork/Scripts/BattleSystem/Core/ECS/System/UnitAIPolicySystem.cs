using Entitas;
using UnityEngine;

public class UnitAIPolicySystem : IExecuteSystem { 
    
    readonly IGroup<BattleEntity> _aiUnits;
   
    public UnitAIPolicySystem(Contexts contexts) {

        _aiUnits = contexts.battle.GetGroup(BattleMatcher.AllOf(BattleMatcher.AIState));
    }

    public void Execute()
    {
        var entities = _aiUnits.GetEntities();
        foreach (var e in entities)
        {
            if (e.unitHpAttribute.Value.Value > 0)
            {
                var pos = e.position.value;
                if (e.hasAITarget && e.aITarget.target.unitHpAttribute.Value.Value >0)
                {
                    if (IsInRange(e.aITarget.target, e.unitAtkRangeAttribute.Value.Value, pos))
                    {
                        e.ReplaceAIState(AIStateType.Attack);
                    }
                    else if (IsInRange(e.aITarget.target, e.unitWarningRangeAttribute.Value.Value, pos))
                    {
                        e.ReplaceAIState(AIStateType.Move);
                    }
                    else
                    {
                        e.RemoveAITarget();
                        e.ReplaceAIState(AIStateType.Idle);
                    }
                }
                else
                {
                    var targetEntity = GetEntityInRange(entities, e.camp.value, e.unitWarningRangeAttribute.Value.Value, pos, false);
                    if (targetEntity != null)
                    {
                        e.ReplaceAITarget(targetEntity);
                    }
                    else
                    {
                        if (e.hasUnitPartrolPath)
                        {
                            e.ReplaceAIState(AIStateType.Partol);
                        }
                        else
                        {
                            e.ReplaceAIState(AIStateType.Idle);
                        }
                    }
                }
            }
            else
            {
                e.ReplaceAIState(AIStateType.Dead);
            }
        }
    }
    //protected override void Execute(List<BattleEntity> entities)
    //{
        
    //}
    private bool IsInRange(BattleEntity entitie,float pRange, Vector3 pSourcePos)
    { 
        return Vector3.Distance(entitie.position.value, pSourcePos) <= pRange;
    }
    private BattleEntity GetEntityInRange(BattleEntity[] entities, CampType pCampType, float pRange, Vector3 pSourcePos, bool pIsSameCamp)
    {
        foreach (var e in entities)
        {
            var isSameCamp = e.camp.value == pCampType;
            if (isSameCamp == pIsSameCamp && e.unitHpAttribute.Value.Value > 0)
            {
                var distance = Vector3.Distance(e.position.value, pSourcePos);
                if (distance <= pRange){
                    return e;
                }
            }
        }
        return null;
    }

    //protected override bool Filter(BattleEntity entity)
    //{
    //    return entity.hasAIState;
    //}

    //protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    //{
    //    return context.CreateCollector(BattleMatcher.AIState);
    //}
}
