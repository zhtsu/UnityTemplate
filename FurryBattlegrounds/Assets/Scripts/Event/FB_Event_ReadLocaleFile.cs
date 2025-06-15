using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Event_ReadLocaleFile : FB_Event
{
    public FB_Event_ReadLocaleFile(string InNamespace, string InLocaleFilePath)
    {
        Namespace = InNamespace;
        LocaleFilePath = InLocaleFilePath;
    }

    public string Namespace { get; private set; }
    public string LocaleFilePath { get; private set; }
}
