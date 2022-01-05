using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitHpBarUpdateSystem : ReactiveSystem<BattleEntity>
{
    public UnitHpBarUpdateSystem(Contexts contexts) : base(contexts.battle){
        
    }

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            var hpBar = e.unitHpBar.gameObject.GetComponent<HpBar>();
            var hpAttribute = e.unitHpAttribute;
            hpBar.SetHp(hpAttribute.Value.Value, hpAttribute.Value.Max);
            hpBar.SetHeight(e.unitHpHeight.height);
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasUnitHpBar;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.UnitHpAttribute);
    }
}
