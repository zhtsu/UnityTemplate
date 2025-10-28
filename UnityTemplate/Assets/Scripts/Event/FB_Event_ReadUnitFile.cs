using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Event_ReadUnitFile : FB_Event
{
    public FB_Event_ReadUnitFile(string InModId, string InUnitFilePath)
    {
        BelongingModId = InModId;
        UnitFilePath = InUnitFilePath;
    }

    public string BelongingModId;
    public string UnitFilePath;
}
