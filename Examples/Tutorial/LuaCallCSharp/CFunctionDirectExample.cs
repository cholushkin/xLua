using System;
using NaughtyAttributes;
using UnityEngine;
using XLua;
using XLua.LuaDLL;

public class CFunctionDirectExample : MonoBehaviour
{
    LuaEnv luaenv = null;

    // public delegate int lua_CSFunction(IntPtr L);
    static int MyHi(IntPtr L)
    {
        Debug.Log("Hi");
        return 0;
    }

    static int MySin(IntPtr L)
    {
        double d = Lua.lua_tonumber(L, 1);  /* get argument */
        Lua.lua_pushnumber(L, Mathf.Sin((float)d));  /* push result */
        return 1;  /* number of results */
    }

    [LuaCallCSharp()]
    static int MySum(int a, int b)
    {
        return a + b;
    }

    void OnEnable()
    {
        luaenv = new LuaEnv();
        Lua.lua_pushstdcallcfunction(luaenv.L, MyHi);
        Lua.xlua_setglobal(luaenv.L, "MyHi");

        Lua.lua_pushstdcallcfunction(luaenv.L, MySin);
        Lua.xlua_setglobal(luaenv.L, "MySin");

        luaenv.DoString("print('activated')");
        luaenv.DoString("MySum()");




    }

    [Button()]
    void CallMyHi()
    {
        luaenv.DoString("MyHi()");
    }

    [Button()]
    void CallMySin()
    {
        luaenv.DoString("print(MySin(1.57))");
    }

    [Button()]
    void CallPrintG()
    {
        luaenv.DoString("for n in pairs(_G) do print(n) end");
    }

    [Button()]
    void CallPrintVectorUp()
    {
        luaenv.DoString("print(CS.UnityEngine.Vector3.up)");
    }

    void OnDisable()
    {
        luaenv.Dispose();
    }
}
