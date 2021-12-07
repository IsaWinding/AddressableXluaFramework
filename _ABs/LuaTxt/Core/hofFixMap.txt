---@class hofFixMap
hofFixMap = {}

hofFixMap.StartLoadFix = function()
    xlua.hotfix(CS.LuaHelper, 'Test', function(a)
        print("HotFix cs LuaHelper Test function")
    end)
    xlua.hotfix(CS.LoginPanel, 'OnForward', function(self)
        self.gameObject.transform.localPosition =  Vector3(0,0,0)
        print("HotFix cs LoginPanel  OnInit function")
    end)
end