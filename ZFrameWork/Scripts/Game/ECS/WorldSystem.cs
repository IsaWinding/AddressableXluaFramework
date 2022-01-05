using Entitas;
public class WorldSystem : Feature
{
    public WorldSystem(Contexts contexts):base("WorldSystem")
    {
        //debug
        Add(new TestDebugMessageSystem(contexts));
        Add(new CleanupDebugMessageSystem(contexts));
        //Add(new ClickTestDebugMessageSystem(contexts));
        Add(new DebugMessageSystem(contexts));
        //input
        Add(new MouseInputsSystem(contexts));
    }
}
public class BattleSystem : Feature
{
    public BattleSystem(Contexts contexts) : base("BattleSystem")
    {
        //Battle
        //Add(new ClickCreateUnitSystem(contexts));
        Add(new UnitSystem(contexts));
        Add(new UnitModelSystem(contexts));
        Add(new UnitPositionSystem(contexts));
        Add(new UnitDirectionSystem(contexts));
        //Add(new ClickMoveUnitSystem(contexts));
        Add(new UnitMoveSystem(contexts));
        Add(new UnitAnimationSystem(contexts));
        Add(new UnitHpBarSystem(contexts));
        Add(new UnitHpBarUpdateSystem(contexts));
       
        Add(new UnitAISystem(contexts));
        Add(new UnitAIPolicySystem(contexts));
        Add(new UnitPartolSystem(contexts));
        Add(new UnitDeadSystem(contexts));
        Add(new UnitCreateSystem(contexts));
        Add(new UnitBattleInitSystem(contexts));
    }
}
