using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitDirectionSystem : ReactiveSystem<BattleEntity>
{
    //readonly BattleContext _battleContext;
    public UnitDirectionSystem(Contexts contexts) : base(contexts.battle)
    {
        //_battleContext = contexts.battle;
    }

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            float angle = e.direction.value;
            e.model.gameObject.transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasModel && entity.hasDirection;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.Direction);
    }
}
