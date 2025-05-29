using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using XLua;

//[CreateAssetMenu(fileName = "NewTileData", menuName = "FB Data/TileData")]
public class FB_TileData : FB_IData
{
    public struct FAnimation
    {
        public enum EType
        {
            None = 0,
            Sprite = 1,
            Sheet = 2,
            Spine = 3
        };

        public EType Type;
        // When Type == Sprite
        public string[] Keyframes;
    };

    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Dictionary<string, FAnimation> AnimationDict { get; private set; } = new Dictionary<string, FAnimation>();

    public string BelongingModId { get; private set; }

    public void Initialize(string ModId)
    {
        BelongingModId = ModId;
        AnimationDict.Clear();
    }

    public string Serialize()
    {
        return "return {}";
    }

    public bool Deserialize(string LuaCode)
    {
        LuaTable TileDataLua = FB_ManagerHub.Instance.XLuaManager.ReturnLuaTable(LuaCode);
        if (TileDataLua == null)
        {
            Debug.Log($"Fail to deserialize tile data: ${LuaCode}");
            return false;
        }

        Id = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(TileDataLua, "id");
        Name = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(TileDataLua, "name");
        Description = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(TileDataLua, "description");
        Width = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, int>(TileDataLua, "width");
        Height = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, int>(TileDataLua, "height");

        LuaTable Animations = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, LuaTable>(TileDataLua, "animations");

        return true;
    }
}
