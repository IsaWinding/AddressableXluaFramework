using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitPositionSystem : ReactiveSystem<BattleEntity>
{
    //readonly BattleContext _battleContext;
    public UnitPositionSystem(Contexts contexts) : base(contexts.battle)
    {
        //_battleContext = contexts.battle;
    }

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            e.model.gameObject.transform.position = e.position.value;
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasModel && entity.hasPosition;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.Position);
    }
}
