using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_ReadTileFileEvent : FB_Event
{
    public FB_ReadTileFileEvent(string InTileFilePath)
    {
        TileFilePath = InTileFilePath;
    }

    public string TileFilePath;
}
