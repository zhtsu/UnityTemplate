using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class FB_ModManager : FB_IManager
{
    public string ManagerName
    {
        get { return "ModManager"; }
    }

    private string _ModsPath;
    private List<FB_ModData> _ModDataList = new List<FB_ModData>();

    public FB_ModManager()
    {
        _ModsPath = NormalizePath(Path.Combine(Application.dataPath, "Mods"));
    }

    public void Initialize()
    {
        if (!Directory.Exists(_ModsPath))
        {
            Directory.CreateDirectory(_ModsPath);
        }

        foreach (string ModDir in Directory.GetDirectories(_ModsPath))
        {
            string ModFilePath = NormalizePath(Path.Combine(ModDir, "mod.fbmod"));
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
                    FB_ReadLocaleFileEvent RLFE = new FB_ReadLocaleFileEvent(ModData.Id, NormalizePath(Path.Combine(_ModsPath, LocaleFile)));
                    FB_ManagerHub.Instance.EventManager.SendEvent<FB_ReadLocaleFileEvent>(RLFE);
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

    private string NormalizePath(string InPath)
    {
        return InPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
    }
}
