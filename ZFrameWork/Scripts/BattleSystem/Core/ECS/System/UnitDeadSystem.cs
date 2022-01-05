using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class UnitDeadSystem : IExecuteSystem, ICleanupSystem//ReactiveSystem<BattleEntity>
{
    readonly IGroup<BattleEntity> _deadUnit;
    //public UnitDeadSystem(Contexts contexts):base(contexts.battle)
    //{

    //    //_deadUnit = contexts.battle.GetGroup(BattleMatcher.UnitHpAttribute);
    //}
    public UnitDeadSystem(Contexts contexts)
    {
        _deadUnit = contexts.battle.GetGroup(BattleMatcher.UnitHpAttribute);
    }

    public void Execute()
    {
        foreach (var e in _deadUnit.GetEntities())
        {
            if (e.unitHpAttribute.Value.Value <= 0)
            {
                AssetManager.Instance.FreeGameObject(e.unitHpBar.gameObject);
                AssetManager.Instance.FreeGameObject(e.modelRes.gameObject);
                e.model.gameObject.Unlink();
                GameObject.Destroy(e.model.gameObject);
                e.Destroy();
            }
        }
    }

    public void Cleanup()
    {
        
    }

    //protected override void Execute(List<BattleEntity> entities)
    //{
    //    foreach (var e in entities)
    //    {
    //        AssetManager.Instance.FreeGameObject(e.unitHpBar.gameObject);
    //        AssetManager.Instance.FreeGameObject(e.modelRes.gameObject);
    //        e.model.gameObject.Unlink();
    //        GameObject.Destroy(e.model.gameObject);
    //        e.Destroy();
    //    }
    //}

    //protected override bool Filter(BattleEntity entity)
    //{
    //    return entity.unitHpAttribute.Value.Value <= 0;
    //}

    //protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    //{
    //    return context.CreateCollector(BattleMatcher.UnitHpAttribute);
    //}
}
