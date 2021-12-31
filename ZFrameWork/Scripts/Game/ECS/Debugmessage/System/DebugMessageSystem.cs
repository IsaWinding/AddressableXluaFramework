using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

// �̳�ReactiveSystem��������ֻҪComponent��ֵһ�����仯�����е�Execute�ͻ�ִ��
public class DebugMessageSystem : ReactiveSystem<DebugEntity>
{
    // �������е�game�������룬GameEntity��Ȼ�Ƿ���game�����е�
    public DebugMessageSystem(Contexts contexts) : base(contexts.debug) { }

    // ���ռ�飬�жϳɹ�����ִ��
    protected override void Execute(List<DebugEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log(e.debugMessage.Message);
        }
    }

    // ���ռ�飬�жϳɹ�����ִ��
    protected override bool Filter(DebugEntity entity)
    {
        return entity.hasDebugMessage;
    }

    // �������е�game�������룬GameEntity��Ȼ�Ƿ���game�����е�
    protected override ICollector<DebugEntity> GetTrigger(IContext<DebugEntity> context)
    {
        // ���ع�����
        return context.CreateCollector(DebugMatcher.DebugMessage);
    }
}
