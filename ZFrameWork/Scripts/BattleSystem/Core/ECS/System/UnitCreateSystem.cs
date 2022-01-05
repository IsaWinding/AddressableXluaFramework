using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitCreateSystem : IExecuteSystem
{
    readonly IGroup<BattleFieldEntity> entitie;
    readonly BattleContext _battleContext;
    readonly List<Vector3> pathA = new List<Vector3>() { new Vector3(-25,0,-25), new Vector3(-30, 0, -30) ,
        new Vector3(-45, 0, -45),new Vector3(-50,0,-50),new Vector3(-65,0,-65),new Vector3(-80,0,-80)};
    readonly List<Vector3> pathB = new List<Vector3>() { new Vector3(-80,0,-80), new Vector3(-65,0,-65) ,
        new Vector3(-50,0,-50),new Vector3(-45, 0, -45),new Vector3(-30, 0, -30),new Vector3(-25,0,-25)};
    public UnitCreateSystem(Contexts contexts)
    {
        entitie = contexts.battleField.GetGroup(BattleFieldMatcher.UnitCreate);
        _battleContext = contexts.battle;
    }

    public void Execute()
    {
        foreach (var e in entitie.GetEntities())
        {
            var curTime = Time.realtimeSinceStartup;
            if (curTime  >= e.unitCreate.NextCreatTime)
            {
                var cdTime = e.unitCreate.CreatCDTime;
                e.ReplaceUnitCreate(curTime + cdTime, cdTime);
                CreatMonster(CampType.PlayerA);
                CreatMonster(CampType.PlayerA);
                CreatMonster(CampType.PlayerA);
                CreatMonster(CampType.PlayerA);

                CreatMonster(CampType.PlayerB);
                CreatMonster(CampType.PlayerB);
                CreatMonster(CampType.PlayerB);
                CreatMonster(CampType.PlayerB);
            }
        }
    }
    private void CreatMonster(CampType pCampType)
    {
        var unit = _battleContext.CreateEntity();
        unit.isCanMove = true;
        unit.AddUnitHpAttribute(new AttributeValue(100, 100, 1));
        unit.AddUnitMpAttribute(new AttributeValue(100, 100, 1));
        unit.AddUnitAtkAttribute(new AttributeValue(10));
        unit.AddUnitDefAttribute(new AttributeValue(10));
        unit.AddUnitMoveSpeedAttribute(new AttributeValue(2));
        unit.AddUnitAtkSpeedAttribute(new AttributeValue(1));
        unit.AddUnitAtkRangeAttribute(new AttributeValue(3));
        unit.AddUnitWarningRangeAttribute(new AttributeValue(5));
        
        unit.AddCamp(pCampType);
        unit.AddAIState(AIStateType.Idle);
        unit.AddModelAnimation(AniNameType.Idle, null);
        unit.AddDirection(0);
        var pathType = PathType.Loop;

        var pathVector3s = new List<Vector3> { new Vector3(-5, 0, -5), new Vector3(-15, 0, -12) };
        if (pCampType == CampType.PlayerA){
            pathVector3s = pathA;
        }
        else if(pCampType == CampType.PlayerB){
            pathVector3s = pathB;
        }
        var paths = AIPath.GetPathsByVector3s(pathVector3s);
        PathPoint firstPoint;
        AIPath.InitPath(paths, pathType, out firstPoint);

        unit.AddPosition(firstPoint.pos);

        unit.AddUnitPartrolPath(paths);
        unit.AddUnitPartrolPoint(true, firstPoint);
        unit.AddUnitPartrolType(pathType);
        unit.isUnitPartrolEnable = true;

        unit.AddUnitHpBarResName("Assets/_ABs/LocalDontChange/Prefabs/HpBar.prefab");
        unit.AddUnitHpHeight(3);

        unit.AddModelResName("Assets/_ABs/LocalDontChange/Prefabs/Player-URP.prefab");
    }

}
