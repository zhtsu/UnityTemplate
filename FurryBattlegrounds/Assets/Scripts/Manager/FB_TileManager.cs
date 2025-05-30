using UnityEngine;
using System.Collections.Generic;
using System.IO;
using XLua;

public class FB_TileManager : FB_IManager
{
    public string ManagerName
    {
        get { return "TileManager"; }
    }

    private List<FB_TileData> _TileDataList = new List<FB_TileData>();

    public void Initialize()
    {
        FB_ManagerHub.Instance.EventManager.Subscribe<FB_ReadTileFileEvent>(LoadTileData);
    }

    public void Destroy()
    {
        FB_ManagerHub.Instance.EventManager.Unsubscribe<FB_ReadTileFileEvent>(LoadTileData);
    }

    public FB_TileData[] GetTileDataList(string ModId)
    {
        return _TileDataList.FindAll(TileData => TileData.BelongingModId == ModId).ToArray();
    }

    public FB_TileData GetTileData(string ModId, string TileId)
    {
        return _TileDataList.Find(
            TileData => TileData.BelongingModId == ModId && TileData.Id == TileId);
    }

    public bool HasTileData(string ModId, string TileId)
    {
        return GetTileData(ModId, TileId) != null;
    }

    private void LoadTileData(FB_ReadTileFileEvent Event)
    {
        if (!File.Exists(Event.TileFilePath))
        {
            Debug.LogError(Event.TileFilePath + " no exist!");
            return;
        }

        try
        {
            string TileFileContent = File.ReadAllText(Event.TileFilePath, System.Text.Encoding.UTF8);

            if (FB_Data.Deserialize<FB_TileData>(TileFileContent, out FB_TileData TileData))
            {
                if (HasTileData(TileData.BelongingModId, TileData.Id))
                {
                    Debug.LogWarning("Repeated tile id");
                    return;
                }

                TileData.Initialize(Event.BelongingModId);

                _TileDataList.Add(TileData);
            }
        }
        catch (System.Exception Err)
        {
            Debug.LogError($"Fail to read {Event.TileFilePath}\n Error: {Err.Message}");
        }
    }
}
