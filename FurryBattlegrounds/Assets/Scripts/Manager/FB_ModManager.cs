using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FB_ModManager : FB_IManager
{
    public string ManagerName
    {
        get { return "ModManager"; }
    }

    private List<FB_ModData> _ModDataList = new List<FB_ModData>();

    public void Initialize()
    {
        foreach (string ModDir in Directory.GetDirectories(FB_PathManager.ModsPath))
        {
            string ModFilePath = FB_PathManager.GenerateModFilePath(Path.Combine(ModDir, "mod.fbmod"));
            if (!File.Exists(ModFilePath))
            {
                Debug.LogError(ModFilePath + " no exist!");
                continue;
            }

            try
            {
                string ModFileContent = File.ReadAllText(ModFilePath, System.Text.Encoding.UTF8);

                FB_ModData ModData = new FB_ModData();
                ModData.Deserialize(ModFileContent);
                _ModDataList.Add(ModData);

                // Load data by sending event
                foreach (string LocaleFile in ModData.LocaleList)
                {
                    FB_ReadLocaleFileEvent RLFE = new FB_ReadLocaleFileEvent(ModData.Id, FB_PathManager.GenerateModFilePath(LocaleFile));
                    FB_ManagerHub.Instance.EventManager.SendEvent<FB_ReadLocaleFileEvent>(RLFE);
                }

                foreach (string TileFile in ModData.TileList)
                {
                    FB_ReadTileFileEvent RTFE = new FB_ReadTileFileEvent(ModData.Id, FB_PathManager.GenerateModFilePath(TileFile));
                    FB_ManagerHub.Instance.EventManager.SendEvent<FB_ReadTileFileEvent>(RTFE);
                }
            }
            catch (System.Exception Err)
            {
                Debug.LogError($"Fail to read {ModFilePath}\n Error: {Err.Message}");
            }
        }
    }

    public void Destroy()
    {

    }

    public List<FB_ModData> GetModDataList()
    {
        return _ModDataList;
    }

    FB_ModData GetModData(string ModId)
    {
        foreach (FB_ModData ModData in _ModDataList)
        {
            if (ModData.Id == ModId)
            {
                return ModData;
            }
        }

        return null;
    }
}
