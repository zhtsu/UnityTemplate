using System;
using System.Collections.Generic;
using XLua;
using XLua.Cast;

public class FB_XLuaManager : FB_IManager
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

    public LuaTable GetLuaTable(string LuaCode)
    {
        return _LuaEnv.DoString(LuaCode)[0] as LuaTable;
    }

    public T GetLuaValue<T>(LuaTable Table, string Key)
    {
        return Table.Get<string, T>(Key);
    }

    public T[] GetLuaArray<T>(LuaTable Table, string Key)
    {
        object LuaObj = Table.Get<object>(Key);
        if (!(LuaObj is LuaTable LuaList))
            return new T[0];

        List<T> ValueList = new List<T>();
        foreach (int K in LuaList.GetKeys())
        {
            T Val = LuaList.Get<int, T>(K);
            ValueList.Add(Val);
        }

        return ValueList.ToArray();
    }
}
