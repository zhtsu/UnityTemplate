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
    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _LocaleStringDict
        = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

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
                LoadModLocaleStringData(ModData);

                _ModDataList.Add(ModData);

                Debug.Log(GetLocaleString(ModData.Id, "en", ModData.Description));
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

    public string GetLocaleString(string ModId, string LanguageCode, string StringId)
    {
        if (_LocaleStringDict.TryGetValue(ModId, out Dictionary<string, Dictionary<string, string>> LanguageDict))
        {
            if (LanguageDict.TryGetValue(LanguageCode, out Dictionary<string, string> StringDict))
            {
                if (StringDict.TryGetValue(StringId, out string LocaleString))
                {
                    return LocaleString;
                }
                else
                {
                    return $"Error string id: {StringId}";
                }
            }
            else
            {
                return $"Error language code: {LanguageCode}";
            }
        }
        else
        {
            return $"Error mod id: {ModId}";
        }
    }

    private string NormalizePath(string InPath)
    {
        return InPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
    }

    private void LoadModLocaleStringData(FB_ModData ModData)
    {
        if (_LocaleStringDict.ContainsKey(ModData.Id) == false)
        {
            _LocaleStringDict[ModData.Id] = new Dictionary<string, Dictionary<string, string>>();
        }

        foreach (string LocaleFile in ModData.LocaleList)
        {
            string LocaleFilePath = NormalizePath(Path.Combine(_ModsPath, LocaleFile));
            if (!File.Exists(LocaleFilePath))
            {
                Debug.LogError(LocaleFilePath + " no exist!");
                continue;
            }

            try
            {
                string LocaleFileContent = File.ReadAllText(LocaleFilePath, System.Text.Encoding.UTF8);
                LuaTable LanguageDictLua = FB_ManagerHub.Instance.XLuaManager.GetLuaTable(LocaleFileContent);

                foreach (string LanguageCode in LanguageDictLua.GetKeys<string>())
                {
                    if (_LocaleStringDict[ModData.Id].ContainsKey(LanguageCode) == false)
                    {
                        _LocaleStringDict[ModData.Id][LanguageCode] = new Dictionary<string, string>();
                    }

                    object StringDictObj = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, object>(LanguageDictLua, LanguageCode);
                    if (!(StringDictObj is LuaTable StringDictLua))
                        continue;

                    foreach (string StringId in StringDictLua.GetKeys<string>())
                    {
                        string LocaleString = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(StringDictLua, StringId);
                        _LocaleStringDict[ModData.Id][LanguageCode].Add(StringId, LocaleString);
                    }
                }
            }
            catch (System.Exception Err)
            {
                Debug.LogError($"Fail to read {LocaleFilePath}\n Error: {Err.Message}");
            }
        }
    }
}
