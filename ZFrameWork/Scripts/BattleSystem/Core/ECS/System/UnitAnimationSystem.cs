using System.Collections.Generic;
using UnityEngine;
using Entitas;


public class UnitAnimationSystem : ReactiveSystem<BattleEntity>
{
    public UnitAnimationSystem(Contexts contexts) : base(contexts.battle){
        
    }
    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            var animator = e.model.gameObject.GetComponentInChildren<Animator>();
            animator.CrossFade(e.modelAnimation.Name,0,0);
            //e.model.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasModel && entity.hasModelAnimation;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.ModelAnimation);
    }
}
