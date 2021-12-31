using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

// 继承ReactiveSystem，功能是只要Component的值一发生变化，其中的Execute就会执行
public class DebugMessageSystem : ReactiveSystem<DebugEntity>
{
    // 将环境中的game环境传入，GameEntity当然是放在game环境中的
    public DebugMessageSystem(Contexts contexts) : base(contexts.debug) { }

    // 最终检查，判断成功才能执行
    protected override void Execute(List<DebugEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log(e.debugMessage.Message);
        }
    }

    // 最终检查，判断成功才能执行
    protected override bool Filter(DebugEntity entity)
    {
        return entity.hasDebugMessage;
    }

    // 将环境中的game环境传入，GameEntity当然是放在game环境中的
    protected override ICollector<DebugEntity> GetTrigger(IContext<DebugEntity> context)
    {
        // 返回过滤器
        return context.CreateCollector(DebugMatcher.DebugMessage);
    }
}
