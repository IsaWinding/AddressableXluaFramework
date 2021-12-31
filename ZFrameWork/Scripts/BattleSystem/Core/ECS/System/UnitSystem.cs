using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class UnitSystem : ReactiveSystem<BattleEntity>
{
    readonly Transform _modelGlobal = new GameObject("BattleField").transform;
    readonly BattleContext _battleContext;
    public UnitSystem(Contexts contexts) : base(contexts.battle)
    {
        _battleContext = contexts.battle;
    }

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            var go = new GameObject("Unit");
            go.transform.SetParent(_modelGlobal,false);
            e.AddModel(go);
            go.Link(e);
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return !entity.hasModel && entity.hasModelResName;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.ModelResName);
    }
}
