using UnityEngine;
using System.Collections.Generic;
using XLua;

public class UT_ScriptManager : UT_Manager
{
    override public string ManagerName => "Script Manager";

    private XLua.LuaEnv _LuaEnv { get; set; }

    override public void Initialize()
    {
        _LuaEnv = new XLua.LuaEnv();
    }

    override public void Destroy()
    {
        _LuaEnv.Dispose();
    }

    public LuaTable ReturnLuaTable(string LuaCode)
    {
        try
        {
            return _LuaEnv.DoString(LuaCode)[0] as LuaTable;
        }
        catch (System.Exception Err)
        {
            Debug.LogError($"Fail to get lua table! Error: {Err.Message}");
        }

        return null;
    }
}
