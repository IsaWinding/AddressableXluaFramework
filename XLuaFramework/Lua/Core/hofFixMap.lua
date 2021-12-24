--require("xlua.util")
---@class hofFixMap
hofFixMap = {}

hofFixMap.StartLoadFix = function()
    --xlua.private_accessible(CS.LuaHelper)
    xlua.hotfix(CS.LuaHelper, 'Test', function(a)
        print("HotFix cs LuaHelper Test function2")
        a()
    end)
    xlua.hotfix(CS.LuaMono, 'OnForward', function(self)
        self.gameObject.transform.localPosition =  Vector3(0,2,0)
        print("HotFix cs LoginPanel  OnInit function")
    end)
end