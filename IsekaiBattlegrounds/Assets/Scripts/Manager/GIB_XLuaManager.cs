using System;
using XLua;

public class GIB_XLuaManager : GIB_IManager
{
    public string ManagerName
    {
        get { return "XLuaManager"; }
    }

    private XLua.LuaEnv _LuaEnv { get; set; }

    public void Initialize()
    {
        _LuaEnv = new XLua.LuaEnv();
    }

    public void Destroy()
    {
        _LuaEnv.Dispose();
    }
}
