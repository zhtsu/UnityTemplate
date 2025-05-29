using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class FB_LocaleManager : FB_IManager
{
    public string ManagerName
    {
        get { return "LocaleManager"; }
    }

    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _LocaleStringDict
        = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

    public void Initialize()
    {
        FB_ManagerHub.Instance.EventManager.Subscribe<FB_ReadLocaleFileEvent>(LoadLocaleData);
    }

    public void Destroy()
    {
        FB_ManagerHub.Instance.EventManager.Unsubscribe<FB_ReadLocaleFileEvent>(LoadLocaleData);
    }

    public string GetString(string Namespace, string LanguageCode, string StringId)
    {
        if (_LocaleStringDict.TryGetValue(Namespace, out Dictionary<string, Dictionary<string, string>> LanguageDict))
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
            return $"Error namespace: {Namespace}";
        }
    }

    private void LoadLocaleData(FB_ReadLocaleFileEvent Event)
    {
        if (_LocaleStringDict.ContainsKey(Event.Namespace) == false)
        {
            _LocaleStringDict[Event.Namespace] = new Dictionary<string, Dictionary<string, string>>();
        }

        if (!File.Exists(Event.LocaleFilePath))
        {
            Debug.LogError(Event.LocaleFilePath + " no exist!");
            return;
        }

        try
        {
            string LocaleFileContent = File.ReadAllText(Event.LocaleFilePath, System.Text.Encoding.UTF8);
            LuaTable LanguageDictLua = FB_ManagerHub.Instance.XLuaManager.ReturnLuaTable(LocaleFileContent);

            foreach (string LanguageCode in LanguageDictLua.GetKeys<string>())
            {
                if (_LocaleStringDict[Event.Namespace].ContainsKey(LanguageCode) == false)
                {
                    _LocaleStringDict[Event.Namespace][LanguageCode] = new Dictionary<string, string>();
                }

                object StringDictObj = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, object>(LanguageDictLua, LanguageCode);
                if (!(StringDictObj is LuaTable StringDictLua))
                    continue;

                foreach (string StringId in StringDictLua.GetKeys<string>())
                {
                    string LocaleString = FB_ManagerHub.Instance.XLuaManager.GetLuaValue<string, string>(StringDictLua, StringId);
                    _LocaleStringDict[Event.Namespace][LanguageCode].Add(StringId, LocaleString);
                }
            }
        }
        catch (System.Exception Err)
        {
            Debug.LogError($"Fail to read {Event.LocaleFilePath}\n Error: {Err.Message}");
        }
    }
}
