using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FB_UnitManager : FB_IManager
{
    public string ManagerName
    {
        get { return "UnitManager"; }
    }

    private List<FB_UnitData> _UnitDataList = new List<FB_UnitData>();

    public void Initialize()
    {
        FB_ManagerHub.Instance.EventManager.Subscribe<FB_ReadUnitFileEvent>(LoadUnitData);
    }

    public void Destroy()
    {
        FB_ManagerHub.Instance.EventManager.Unsubscribe<FB_ReadUnitFileEvent>(LoadUnitData);
    }

    private FB_UnitData GetUnitData(string ModId, string TileId)
    {
        return _UnitDataList.Find(
            UnitData => UnitData.BelongingModId == ModId && UnitData.Id == TileId);
    }

    private bool HasUnitData(string ModId, string TileId)
    {
        return GetUnitData(ModId, TileId) != null;
    }

    private void LoadUnitData(FB_ReadUnitFileEvent Event)
    {
        if (!File.Exists(Event.UnitFilePath))
        {
            Debug.LogError(Event.UnitFilePath + " no exist!");
            return;
        }

        try
        {
            string UnitFileContent = File.ReadAllText(Event.UnitFilePath, System.Text.Encoding.UTF8);

            if (FB_Data.Deserialize<FB_UnitData>(UnitFileContent, out FB_UnitData UnitData))
            {
                if (HasUnitData(UnitData.BelongingModId, UnitData.Id))
                {
                    Debug.LogWarning("Repeated unit id");
                    return;
                }

                UnitData.Initialize(Event.BelongingModId);

                _UnitDataList.Add(UnitData);
            }
        }
        catch (System.Exception Err)
        {
            Debug.LogError($"Fail to read {Event.UnitFilePath}\n Error: {Err.Message}");
        }
    }
}
