using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_ReadLocaleFileEvent : FB_Event
{
    public FB_ReadLocaleFileEvent(string InNamespace, string InLocaleFilePath)
    {
        Namespace = InNamespace;
        LocaleFilePath = InLocaleFilePath;
    }

    public string Namespace { get; private set; }
    public string LocaleFilePath { get; private set; }
}
