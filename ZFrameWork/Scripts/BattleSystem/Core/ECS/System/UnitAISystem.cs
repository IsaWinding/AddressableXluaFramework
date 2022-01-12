using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UnitAISystem : ReactiveSystem<BattleEntity>
{
    public UnitAISystem(Contexts contexts) : base(contexts.battle){}

    protected override void Execute(List<BattleEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.aIState.aiStateType == AIStateType.Idle)
            {
                e.isMoveFinish = true;
                e.ReplaceModelAnimation(AniNameType.Idle, null);
                e.isUnitPartrolEnable = false;
            }
            else if (e.aIState.aiStateType == AIStateType.Partol)
            {
                e.isMoveFinish = false;
                e.isUnitPartrolEnable = true;
            }
            else if (e.aIState.aiStateType == AIStateType.Move)
            {
                e.isMoveFinish = false;
                e.ReplaceMoveTarget(e.aITarget.target.position.value);
                e.ReplaceModelAnimation(AniNameType.Move, null);
                e.isUnitPartrolEnable = false;
            }
            else if (e.aIState.aiStateType == AIStateType.Attack)
            {

                var dir = e.aITarget.target.position.value - e.position.value;
                if (dir.magnitude > 0.2f) {
                    var newRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
                    e.ReplaceDirection(newRot.eulerAngles.y);
                }
                e.isMoveFinish = true;
                e.ReplaceModelAnimation(AniNameType.Attack, () => {
                    var curHp = e.aITarget.target.unitHpAttribute.Value.Value;
                    var newHp = curHp - e.unitAtkAttribute.Value.Value;
                    if (newHp < 0) newHp = 0;
                    var newHpAtt = new AttributeValue(newHp, e.aITarget.target.unitHpAttribute.Value.Max,
                        e.aITarget.target.unitHpAttribute.Value.Recover);
                    e.aITarget.target.ReplaceUnitHpAttribute(newHpAtt);
                });
                e.isUnitPartrolEnable = false;
            }
            else if (e.aIState.aiStateType == AIStateType.Dead)
            {
                e.isMoveFinish = true;
                e.isUnitPartrolEnable = false;
                e.ReplaceModelAnimation(AniNameType.Dead, null);
                e.ReplacePosition(new Vector3(-9999, -999, -999));
            }
        }
    }

    protected override bool Filter(BattleEntity entity)
    {
        return entity.hasAIState;
    }

    protected override ICollector<BattleEntity> GetTrigger(IContext<BattleEntity> context)
    {
        return context.CreateCollector(BattleMatcher.AllOf(BattleMatcher.AIState, BattleMatcher.AITarget));
    }
}
