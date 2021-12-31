using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ClickCreateUnitSystem : ReactiveSystem<InputEntity>
{
    readonly BattleContext _battleContext;

    public ClickCreateUnitSystem(Contexts contexts) : base(contexts.input){
        _battleContext = contexts.battle;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            var unit = _battleContext.CreateEntity();
            unit.isCanMove = true;
            unit.AddPosition(e.mouseDown.position);
            unit.AddUnitHpAttribute(new AttributeValue(100,100,1));
            unit.AddUnitMpAttribute(new AttributeValue(100, 100,1));
            unit.AddUnitAtkAttribute(new AttributeValue(10));
            unit.AddUnitDefAttribute(new AttributeValue(10));
            unit.AddUnitMoveSpeedAttribute(new AttributeValue(4));
            unit.AddUnitAtkSpeedAttribute(new AttributeValue(1));
            unit.AddUnitAtkRangeAttribute(new AttributeValue(3));
            unit.AddUnitWarningRangeAttribute(new AttributeValue(5));
            var campRandom = Random.Range(0,3);
            unit.AddCamp( (CampType)campRandom);
            unit.AddAIState(AIStateType.Idle);

            unit.AddModelAnimation("Attack");
            unit.AddDirection(Random.Range(0,360));
            unit.AddUnitHpBarResName("Assets/_ABs/LocalDontChange/Prefabs/HpBar.prefab");
            unit.AddModelResName("Assets/_ABs/LocalDontChange/Prefabs/Player-URP.prefab");
        }
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseDown;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.RightMouse,InputMatcher.MouseDown));
    }
}
