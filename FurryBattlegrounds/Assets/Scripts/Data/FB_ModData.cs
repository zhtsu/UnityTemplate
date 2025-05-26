using System.Collections.Generic;
using UnityEngine;
using XLua;

public class FB_ModData : FB_IData
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Author { get; private set; }
    public string Email { get; private set; }
    public string[] TileList { get; private set; }
    public string[] UnitList { get; private set; }
    public string[] LocaleList { get; private set; }

    public string Serialize()
    {
        return "return {}";
    }

    public bool Deserialize(string LuaCode)
    {
        LuaTable ModDataLua = FB_ManagerHub.Instance.XLuaManager.GetLuaTable(LuaCode);
        if (ModDataLua == null)
        {
            Debug.Log($"Fail to deserialize lua code: ${LuaCode}");
            return false;
        }

        Id = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(ModDataLua, "id");
        Name = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(ModDataLua, "name");
        Description = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(ModDataLua, "description");
        Author = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(ModDataLua, "author");
        Email = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(ModDataLua, "email");

        TileList = FB_ManagerHub.Instance.XLuaManager.GetLuaArray<string>(ModDataLua, "tile_list");
        UnitList = FB_ManagerHub.Instance.XLuaManager.GetLuaArray<string>(ModDataLua, "unit_list");
        LocaleList = FB_ManagerHub.Instance.XLuaManager.GetLuaArray<string>(ModDataLua, "locale_list");

        return true;
    }
}
