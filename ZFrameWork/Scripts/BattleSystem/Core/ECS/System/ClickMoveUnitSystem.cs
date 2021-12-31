using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class ClickMoveUnitSystem : ReactiveSystem<InputEntity>
{
    readonly IGroup<BattleEntity> _Movers;

    public ClickMoveUnitSystem(Contexts contexts) : base(contexts.input)
    {
        _Movers = contexts.battle.GetGroup(BattleMatcher.AllOf(BattleMatcher.CanMove)) ;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            var movers = _Movers.GetEntities();
            foreach (var m in movers)
            {
                var x = Random.Range(-10f, 10f);
                var z = Random.Range(-10f, 10f);
                m.ReplaceMoveTarget(new Vector3(x,0,z));
            }
        }
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseDown;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.LeftMouse, InputMatcher.MouseDown));
    }
}
