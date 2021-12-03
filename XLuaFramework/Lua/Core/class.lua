print("class")
---@param object table
function clone(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end
        local new_table = {}
        lookup_table[object] = new_table
        for key, value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end
    return _copy(object)
end

---Create an class.
---@param classname string
---@param super string
function class(classname, super)
    local superType = type(super)
    local cls

    if superType ~= "function" and superType ~= "table" then
        superType = nil
        super = nil
    end

    if superType == "function" or (super and super.__ctype == 1) then
        -- inherited from native C++ Object
        cls = {}

        if superType == "table" then
            -- copy fields from super
            for k,v in pairs(super) do cls[k] = v end
            cls.__create = super.__create
            cls.super    = super
        else
            cls.__create = super
        end

        cls.ctor    = function() end
        cls.__cname = classname
        cls.__ctype = 1

        function cls.New(...)
            local instance = cls.__create(...)
            -- copy fields from class to native object
            for k,v in pairs(cls) do instance[k] = v end
            instance.class = cls
            instance:ctor(...)
            return instance
        end

    else
        -- inherited from Lua Object
        if super then
            cls = clone(super)
            cls.super = super
        else
            cls = {ctor = function() end}
        end

        cls.__cname = classname
        cls.__ctype = 2 -- lua
        cls.__index = cls

        function cls.New(...)
            local instance = setmetatable({}, cls)
            instance.class = cls
            instance:ctor(...)
            return instance
        end
    end

    return cls
end

---** 设置table只读 出现改写会抛出lua error
--- 用法 local cfg_proxy = read_only(cfg)  retur cfg_proxy
--- 增加了防重置设置read_only的机制
--- lua5.3支持 1）table库支持调用元方法，所以table.remove table.insert 也会抛出错误，
---               2）不用定义__ipairs 5.3 ipairs迭代器支持访问元方法__index，pairs迭代器next不支持故需要元方法__pairs
--- 低版本lua此函数不能完全按照预期工作
---@param t table
function read_only(t)
    --local temp= t or {}
    --local mt = {
    --    __index = function(t,k) return temp[k] end ;
    --    __newindex = function(t, k, v)
    --        Logger.LogError("attempt to update a read-only table!")
    --    end
    --}
    --setmetatable(temp, mt)
    --return temp
    return t
end
--[[
    将一个table 数据绑定一个事件key 当改table数据被外部修改后，将会发送一次该数据绑定好的eventkey 的事件
--]]
function bind_living_data(t,pBindEventKey)
    local newTable  = {}
    local mt = {
        __index = t;--function (table,key) return rawget(table,key) end;
        __newindex = function(t, k, v)
            Logger.LogError("table data is change __newindex"..pBindEventKey)
            EventManager:BroadEvent(pBindEventKey)
            rawset(t, k, v)
        end
    }
    setmetatable(newTable , mt)
    return newTable
end