using Entitas;

public class UnitBattleInitSystem : IInitializeSystem
{
    readonly BattleFieldContext _context;
    public UnitBattleInitSystem(Contexts pcontexts)
    {
        _context = pcontexts.battleField;
    }

    public void Initialize()
    {
        var e = _context.CreateEntity();
        e.AddUnitCreate(2,30);
    }

}
