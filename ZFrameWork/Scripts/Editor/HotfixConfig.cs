using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XLua;

namespace ZFrameWork
{
    public static class HotfixConfig
    {
        [Hotfix]
        public static List<Type> by_field = new List<Type>()
        {
            typeof(LuaHelper),
            typeof(LoginPanel),
            typeof(LuaMono),
        };

        [Hotfix]
        public static List<Type> hotfixList {
            get {
                string[] allowNamespaces = new string[] {
                    "ZFrameWork",
                };

                return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                        where allowNamespaces.Contains(type.Namespace)
                        select type).ToList();
            }

        }
    }
}

