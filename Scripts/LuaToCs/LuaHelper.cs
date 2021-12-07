using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
[Hotfix]
public static class LuaHelper
{
    [Hotfix]
    public static void Test(System.Action pAc)
    {
       //Debug.LogError("LuaHelper Test");
        pAc.Invoke();
    }
}
