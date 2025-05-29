using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_ReadTileFileEvent : FB_Event
{
    public FB_ReadTileFileEvent(string InModId, string InTileFilePath)
    {
        BelongingModId = InModId;
        TileFilePath = InTileFilePath;
    }

    public string BelongingModId;
    public string TileFilePath;
}
