using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_ReadUnitFileEvent : FB_Event
{
    public FB_ReadUnitFileEvent(string InModId, string InUnitFilePath)
    {
        BelongingModId = InModId;
        UnitFilePath = InUnitFilePath;
    }

    public string BelongingModId;
    public string UnitFilePath;
}
