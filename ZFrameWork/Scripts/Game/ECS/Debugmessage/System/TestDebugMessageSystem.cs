using Entitas;

public class TestDebugMessageSystem : IInitializeSystem
{
    readonly DebugContext _context;
    public TestDebugMessageSystem(Contexts pcontexts)
    {
        _context = pcontexts.debug;
    }
    public void Initialize()
    {
        _context.CreateEntity().AddDebugMessage("Hell world!!");
    }
}
