using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitModelSystem : ReactiveSystem<BattleEntity>
{
    //readonly BattleContext _battleContext;
    public UnitModelSystem(Contexts contexts) : base(contexts.battle)
    {
        //_battleContext = contexts.battle;
    }

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            var parentRoot = e.model.gameObject;
            var go = AssetManager.Instance.Instantiate(e.modelResName.Value);
            go.transform.SetParent(parentRoot.transform,false);
            go.transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
            e.ReplaceModelRes(go);
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasModel && entity.hasModelResName;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.ModelResName);
    }
}
