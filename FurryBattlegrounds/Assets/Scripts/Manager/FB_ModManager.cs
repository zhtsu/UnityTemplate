using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FB_ModManager : FB_IManager
{
    public string ManagerName
    {
        get { return "ModManager"; }
    }

    private List<FB_ModData> _ModDataList;

    public void Initialize()
    {
        string ModsPath = NormalizePath(Path.Combine(Application.dataPath, "Mods"));
        if (!Directory.Exists(ModsPath))
        {
            Directory.CreateDirectory(ModsPath);
        }

        foreach (string ModDir in Directory.GetDirectories(ModsPath))
        {
            string ModFilePath = NormalizePath(Path.Combine(ModDir, "mod.fbmod"));
            if (!File.Exists(ModFilePath))
            {
                Debug.Log(ModFilePath + " no exist!");
                continue;
            }

            try
            {
                string ModFileContent = File.ReadAllText(ModFilePath, System.Text.Encoding.UTF8);

                FB_ModData ModData = new FB_ModData();
                ModData.Deserialize(ModFileContent);
                _ModDataList.Add(ModData);
            }
            catch (System.Exception Err)
            {
                Debug.LogError($"Fail to deserialize {ModFilePath}\n Error: {Err.Message}");
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
