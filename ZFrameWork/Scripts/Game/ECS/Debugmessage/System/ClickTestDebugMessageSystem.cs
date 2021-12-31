using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTestDebugMessageSystem : IExecuteSystem
{
    readonly DebugContext _context;
    public ClickTestDebugMessageSystem(Contexts contexts) {

        _context = contexts.debug;
    }
    public void Execute() {
        if (Input.GetMouseButtonDown(0))
        {
            _context.CreateEntity().AddDebugMessage("Left Mouse Button Clicked");
        }
        if (Input.GetMouseButtonDown(1))
        {
            _context.CreateEntity().AddDebugMessage("Right Mouse Button Clicked");
        }
    }
}
