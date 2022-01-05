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

            var x = Random.Range(-10f, 10f);
            var z = Random.Range(-10f, 10f);
            unit.AddPosition(new Vector3(x, 0, z));

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
            unit.AddModelAnimation( AniNameType.Idle,null);
            unit.AddDirection(Random.Range(0,360));
            var pathType = PathType.Loop;
            var pathVector3s = new List<Vector3> { new Vector3(-5,0,-5), new Vector3(-15, 0, -12) };
            var paths = AIPath.GetPathsByVector3s(pathVector3s);
            PathPoint firstPoint;
            AIPath.InitPath(paths, pathType, out firstPoint);

            unit.AddUnitPartrolPath(paths);
            unit.AddUnitPartrolPoint(true,firstPoint);
            unit.AddUnitPartrolType(pathType);
            unit.isUnitPartrolEnable = true;

            unit.AddUnitHpBarResName("Assets/_ABs/LocalDontChange/Prefabs/HpBar.prefab");
            unit.AddUnitHpHeight(3);

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
