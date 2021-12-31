using Entitas;

public class CleanupDebugMessageSystem : ICleanupSystem
{
    readonly DebugContext _context;

    readonly IGroup<DebugEntity> _debugMessages;

    public CleanupDebugMessageSystem(Contexts contexts) {
        _context = contexts.debug;
        _debugMessages = _context.GetGroup(DebugMatcher.DebugMessage);
    }

    public void Cleanup() {
        foreach (var e in _debugMessages.GetEntities())
        {
            e.Destroy();
        }
    }
}
