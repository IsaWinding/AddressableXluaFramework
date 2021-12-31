using Entitas;


[Battle]
public class AIStateComponent:IComponent
{
    public AIStateType aiStateType;
}

[Battle]
public class AITargetComponent : IComponent
{
    public BattleEntity target;
}


