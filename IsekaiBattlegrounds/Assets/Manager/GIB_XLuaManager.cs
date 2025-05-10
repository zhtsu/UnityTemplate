using System;
using XLua;

public class GIB_XLuaManager : GIB_IManager
{
    public string ManagerName
    {
        get { return "XLuaManager"; }
    }

    private XLua.LuaEnv luaEnv { get; set; }

    public void Initialize()
    {
        luaEnv = new XLua.LuaEnv();
    }

    public void Destroy()
    {
        luaEnv.Dispose();
    }
}
