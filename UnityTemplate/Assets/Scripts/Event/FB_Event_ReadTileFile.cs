using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Event_ReadTileFile : FB_Event
{
    public FB_Event_ReadTileFile(string InModId, string InTileFilePath)
    {
        BelongingModId = InModId;
        TileFilePath = InTileFilePath;
    }

    public string BelongingModId;
    public string TileFilePath;
}
