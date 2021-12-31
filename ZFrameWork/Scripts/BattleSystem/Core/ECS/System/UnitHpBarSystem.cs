using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitHpBarSystem : ReactiveSystem<BattleEntity>
{
    public UnitHpBarSystem(Contexts contexts) : base(contexts.battle){}

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            var parentRoot = e.model.gameObject;
            var go = AssetManager.Instance.Instantiate(e.unitHpBarResName.resName);
            go.transform.SetParent(parentRoot.transform, false);
            go.transform.SetPositionAndRotation(Vector3.up*3, Quaternion.identity);
            e.ReplaceUnitHpBar(go);
        }
    }
    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasModel && entity.hasUnitHpBarResName;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.UnitHpBarResName);
    }
}
