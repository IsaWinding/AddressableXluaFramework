using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    public const string LuaTxtResources = "_ABs/LuaTxt";
    public const string LuaResources = "XLuaFramework/Lua";
    public const string ABRoot = "_ABs";
}
public static class GameSettings
{
    public static string GetABRootPath()
    {
        return Application.dataPath + "/" + GameDefine.ABRoot;
    }
    public static string GetLuaTxtPath()
    {
        return Application.dataPath + "/" + GameDefine.LuaTxtResources;
    }
    public static string GetLuaResourcesPath()
    {
        return Application.dataPath + "/" + GameDefine.LuaResources;
    }

}
