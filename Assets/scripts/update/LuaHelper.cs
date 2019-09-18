using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class LuaHelper
{
    public static LuaTable AddLuaComponent(GameObject go, string luaPath, params object[] args)
    {
        LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour>();
        luaBehaviour.InitLua(luaPath,args);
        return luaBehaviour.LuaTable;
    }

    public static LuaTable AddLuaComponent(Component comp, string luaPath, params object[] args)
    {
        return AddLuaComponent(comp.gameObject, luaPath, args);
    }
}
