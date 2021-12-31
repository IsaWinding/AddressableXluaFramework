using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class MouseInputsSystem : IInitializeSystem,IExecuteSystem
{
    readonly InputContext _context;
    private InputEntity _leftMouseEntity;
    private InputEntity _rightMouseEntity;
    public MouseInputsSystem(Contexts contexts) {
        _context = contexts.input;
    }

    public void Execute()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);
        ReplacePositionProgress(_leftMouseEntity,0,mousePos);
        ReplacePositionProgress(_rightMouseEntity, 1,mousePos);
    }
    void ReplacePositionProgress(InputEntity entity,int buttonIndex,Vector2 mousePosition) {
        if (Input.GetMouseButtonDown(buttonIndex))
            entity.ReplaceMouseDown(mousePosition);
        if (Input.GetMouseButtonUp(buttonIndex))
            entity.ReplaceMouseUp(mousePosition);
        if (Input.GetMouseButton(buttonIndex))
        {
            //Debug.Log(mousePosition);
            entity.ReplaceMousePosition(mousePosition);
        }
            
    }
    public void Initialize()
    {
        _context.isLeftMouse = true;
        _context.isRightMouse = true;
        _leftMouseEntity = _context.leftMouseEntity;
        _rightMouseEntity = _context.rightMouseEntity;
    }
}
